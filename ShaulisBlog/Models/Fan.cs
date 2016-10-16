using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShaulisBlog.Models
{
    public enum Gender
    {
        Male, Female
    }
    public class Fan
    {
        static int counter = 0;
        [Key]
        public int ID { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string _firstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string _lastName { get; set; }

        [Required]
        [DisplayName("Gender")]
        public Gender _gender { get; set; }

        [Required]
        [DisplayName("Birth Date")]
        public DateTime _birthDate { get; set; }

        [Required]
        [DisplayName("Years in club")]
        public double _seniority { get; set; }

        public Fan()
        {
            counter++;
            ID = counter;
        }


        public void setFan(Fan copy)
        {
            this._firstName = copy._firstName;
            this._lastName = copy._lastName;
            this._gender = copy._gender;
            this._birthDate = copy._birthDate;
            this._seniority = copy._seniority;
        }
    }
   /* public class FanDBContext : DbContext
    {
        public DbSet<Fan> Movies { get; set; }
    }*/

}