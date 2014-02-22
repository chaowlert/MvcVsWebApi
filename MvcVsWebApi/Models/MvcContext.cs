using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcVsWebApi.Models
{
    public class MvcContext : DbContext
    {
        public virtual DbSet<Person> Persons { get; set; }
    }
}