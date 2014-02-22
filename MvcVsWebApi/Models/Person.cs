using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcVsWebApi.Models
{
    /// <summary>
    /// Represent a person
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Person id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Person name (Required)
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Person age
        /// </summary>
        [Range(0, 99)]
        public int Age { get; set; }
    }
}