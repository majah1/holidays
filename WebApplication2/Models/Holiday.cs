using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{

    public class Holiday
    {
        string name;
        DateTime startsAt;
        DateTime endsAt;
        bool occursAnually;
        int numberOfDaysOff;
        int id;


       public Holiday (string name, bool occursAnually, DateTime startsAt, DateTime endsAt, int numberOfDaysOff, int id)
            
            
            {
            this.name = name;
            this.startsAt = startsAt;
            this.endsAt = endsAt;
            this.occursAnually = occursAnually;
            this.numberOfDaysOff = (endsAt - startsAt).Days;
            this.Id = id;
            }

        [Required(ErrorMessage = "Please enter a Holiday Name", AllowEmptyStrings = false)]
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }
        [Required(ErrorMessage = "Please enter start date", AllowEmptyStrings = false)]

        public DateTime StartsAt
        {
            get
            {
                return startsAt;
            }

            set
            {
                startsAt = value;
            }
        }

        [Required(ErrorMessage = "Please enter end date", AllowEmptyStrings = false)]

        public DateTime EndsAt
        {
            get
            {
                return endsAt;
            }

            set
            {
                endsAt = value;
            }
        }

        public bool OccursAnually
        {
            get
            {
                return occursAnually;
            }

            set
            {
                occursAnually = value;
            }
        }

        public int NumberOfDaysOff
        {
            get
            {
                return numberOfDaysOff;
            }

            set
            {
                numberOfDaysOff = value;
            }
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }
    }
}