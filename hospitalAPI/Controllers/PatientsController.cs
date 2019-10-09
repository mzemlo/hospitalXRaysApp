using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using hospitalAPI.Models;
using System.Web.Http.Cors;
using Microsoft.AspNet.Identity;
using System.Text.RegularExpressions;

namespace hospitalAPI.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    //[Authorize]
    public class PatientsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Patients
        [Authorize(Roles = "Ordynator, Lekarz")]
        public IQueryable<PatientDTO> GetPatients()
        {
            var patients = from b in db.Patients
                           select new PatientDTO()
                           {
                               Id = b.Id,
                               Name = b.Name,
                               Surname = b.Surname,
                               Pesel = b.Pesel,
                               PostCode = b.PostCode,
                               City = b.City,
                               Street = b.Street,
                               NoHouse = b.NoHouse,
                               NoFlat = b.NoFlat,
                               DoctorName = b.ApplicationUser.Name,
                               Photos = b.Photos
                           };
                           

            return patients;
        }
        [Route("api/{doctorId}/patients")]
        [Authorize(Roles = "Ordynator, Lekarz")]
        public IQueryable<PatientDTO> GetPatient(string doctorId)
        {
            var patients = from b in db.Patients.Where(b => b.ApplicationUser.Id == doctorId)
                           select new PatientDTO()
                           {
                               Id = b.Id,
                               Name = b.Name,
                               Surname = b.Surname,
                               Pesel = b.Pesel,
                               PostCode = b.PostCode,
                               City = b.City,
                               Street = b.Street,
                               NoHouse = b.NoHouse,
                               NoFlat = b.NoFlat,
                               DoctorName = b.ApplicationUser.Name,
                               Photos = b.Photos
                           };


            return patients;
        }

        // GET: api/Patients/5
        [Authorize(Roles = "Admin, Ordynator, Lekarz")]
        [ResponseType(typeof(PatientDTO))]
        public async Task<IHttpActionResult> GetPatient(int id)
        {
            Patient patient = await db.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        // PUT: api/Patients/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPatient(int id, Patient patient)
        {
            ApplicationDbContext x = new ApplicationDbContext();
            Patient dbPatient = x.Patients.Find(id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != patient.Id)
            {
                return BadRequest();
            }
            if (dbPatient.UserId != patient.UserId)
            {
                return BadRequest("Nie masz uprawnień do edycji tego pacjenta");
            }
            db.Entry(patient).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
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

        // POST: api/Patients
        [ResponseType(typeof(Patient))]
        public async Task<IHttpActionResult> PostPatient(Patient patient)
        {
            Regex regex =  new Regex(@"\d+");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Walidacja
            if(patient.Name == "" || regex.IsMatch(patient.Name))
                return BadRequest("Pacjent nie ma podanego imienia!");

            if (patient.Surname == "" || regex.IsMatch(patient.Surname))
                return BadRequest("Pacjent nie ma podanego nazwiska!");

            if (patient.Pesel == "" || !Regex.IsMatch(patient.Pesel, @"^[0-9]{11}$"))
                return BadRequest("Pacjent nie ma podanego numeru PESEL!");

            if (patient.PostCode == "" || !Regex.IsMatch(patient.PostCode, @"^[0-9]{2}\-[0-9]{3}$"))
                return BadRequest("Pacjent nie ma podanego numeru pocztowego!");

            if (patient.City == "" || regex.IsMatch(patient.City))
                return BadRequest("Pacjent nie ma podanej miejscowości!");

            if (patient.Street == "")
                return BadRequest("Pacjent nie ma podanej ulicy");
            bool check = Regex.IsMatch(patient.NoHouse, @"^[0-9]+[a-zA-Z]?$");
            if (patient.NoHouse == "" || !Regex.IsMatch(patient.NoHouse, @"^[0-9]+[a-zA-Z]?$"))
                return BadRequest("Pacjent nie ma podanego numeru domu!");

            if(!Regex.IsMatch(patient.NoFlat, @"^\D") && patient.NoFlat != "")
                return BadRequest("Pacjent ma błędny numeru mieszkania!");
            // Koniec Walidacji

            db.Patients.Add(patient);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = patient.Id }, patient);
        }

        // DELETE: api/Patients/5
        [ResponseType(typeof(Patient))]
        public async Task<IHttpActionResult> DeletePatient(int id)
        {
            Patient patient = await db.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            db.Patients.Remove(patient);
            await db.SaveChangesAsync();

            return Ok(patient);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PatientExists(int id)
        {
            return db.Patients.Count(e => e.Id == id) > 0;
        }
    }
}