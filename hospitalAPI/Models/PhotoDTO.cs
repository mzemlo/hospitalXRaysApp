using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hospitalAPI.Models
{
    public class PhotoDTO
    {
        public int Id { get; set; }
        public string XrayPhotoBlobSource { get; set; }
        public string ColoredPhotoBlobSource { get; set; }
        public bool IsColored { get; set; }
        public string DiseaseName { get; set; }
        public string DiseaseDescription { get; set; }
    }
}