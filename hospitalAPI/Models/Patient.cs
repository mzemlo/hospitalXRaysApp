using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hospitalAPI.Models
{
    public class Patient
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Pesel { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string NoHouse { get; set; }
        public string NoFlat { get; set; }
        public string UserId { get; set; }
        // Navigation property
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<Photo> Photos { get; set; }



    }
}