using System;
using System.ComponentModel.DataAnnotations;

namespace Adventures.Data.Models
{
    public class ParkModel
    {
        public int Id { get; set; }

        [Required, StringLength(50, ErrorMessage = "The {0} must be atleast {2} and at most {1} characters.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required, StringLength(2, ErrorMessage = "The {0} must be {1} characters.", MinimumLength = 2)]
        public string State { get; set; }

        public DateTime Created { get; set; }
    }
}
