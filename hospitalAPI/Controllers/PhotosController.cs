using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using hospitalAPI.ColorTransformation;
using hospitalAPI.Models;

namespace hospitalAPI.Controllers
{
    public class PhotosController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Photos
        [Authorize(Roles = "Ordynator, Lekarz")]
        public IQueryable<Photo> GetPhotos()
        {
            return db.Photos;
        }
        [Authorize(Roles = "Ordynator, Lekarz")]
        [Route("api/{PatientId}/photos")]
        public IQueryable<Photo> GetPhotos(int PatientId)
        {
            return db.Photos.Where(b => b.Patient.Id == PatientId);
        }

        // GET: api/Photos/5
        [Authorize(Roles = "Ordynator, Lekarz")]
        [ResponseType(typeof(Photo))]
        public IHttpActionResult GetPhoto(int id)
        {
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return NotFound();
            }

            return Ok(photo);
        }

        // PUT: api/Photos/5
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPhoto(int id, Photo photo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != photo.Id)
            {
                return BadRequest();
            }

            db.Entry(photo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhotoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Photos
        [Authorize(Roles = "Ordynator, Lekarz")]
        [ResponseType(typeof(Photo))]
        public IHttpActionResult PostPhoto()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int iUploadedCnt = 0;

            // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
            string sPath = "";
            string sColoredPath = "";
            string unicalCode = "";
            string fileName = "";
            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/photos/xrayoriginal/");
            sColoredPath = System.Web.Hosting.HostingEnvironment.MapPath("~/photos/xraycolored/");
            unicalCode = Guid.NewGuid().ToString();
            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            // CHECK THE FILE COUNT.
            if (hfc.Count == 1)
            {
                System.Web.HttpPostedFile hpf = hfc[0];
                string description = HttpContext.Current.Request.Params.Get("desc");
                string name = HttpContext.Current.Request.Params.Get("name");
                string x = HttpContext.Current.Request.Params.Get("patientId");
                int patientId = Int32.Parse(x);


                if (hpf.ContentLength > 0)
                {
                    fileName = unicalCode + hpf.FileName;
                    // CHECK IF THE SELECTED FILE(S) ALREADY EXISTS IN FOLDER. (AVOID DUPLICATE)
                    if (!File.Exists(sPath + Path.GetFileName(fileName)))
                    {
                        // SAVE THE FILES IN THE FOLDER.
                        hpf.SaveAs(sPath + Path.GetFileName(fileName));
                        iUploadedCnt = iUploadedCnt + 1;
                    }

                    string filePath = sPath + Path.GetFileName(fileName);
                    ColorChanger algorithm = new ColorChanger(filePath);
                    algorithm.ColorImage();
                    algorithm.SaveColoredImage(sColoredPath+"colored"+ fileName);

                    var image = new Photo
                    {
                        DiseaseName = name,
                        DiseaseDescription = description,
                        XrayPhotoBlobSource = "/photos/xrayoriginal/" + Path.GetFileName(fileName),
                        ColoredPhotoBlobSource = "/photos/xraycolored/" + "colored" + Path.GetFileName(fileName),
                        IsColored = true,                        
                        Patient = db.Patients.Single(p => p.Id == patientId)
                    };

                    db.Photos.Add(image);
                    db.SaveChanges();
                    var patients = db.Patients.Single(p => p.Id == patientId);
                    if (patients != null)
                    {
                        patients.Photos.Add(image);
                        db.SaveChanges();
                    }
                    return Ok($@"http:\\{Request.RequestUri.Host}\photos\xrayoriginal\{fileName}");
                }
            }
            return BadRequest("Upload Failed");

        }


        // DELETE: api/Photos/5
        [Authorize(Roles = "Ordynator, Lekarz")]
        [ResponseType(typeof(Photo))]
        public IHttpActionResult DeletePhoto(int id)
        {
            string dPath = "";
            string dColoredPath = "";
            string folder = "";
            folder = System.Web.Hosting.HostingEnvironment.MapPath("~");
            
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return NotFound();
            }
            dPath = folder + photo.XrayPhotoBlobSource;
            dColoredPath = folder + photo.ColoredPhotoBlobSource;
            db.Photos.Remove(photo);
            db.SaveChanges();
            File.Delete(dPath);
            File.Delete(dColoredPath);
            return Ok(photo);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PhotoExists(int id)
        {
            return db.Photos.Count(e => e.Id == id) > 0;
        }

 
    }
}