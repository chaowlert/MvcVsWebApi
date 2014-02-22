using System;
using System.Linq;
using System.Web.Http;
using Microsoft.Practices.Unity;
using MvcVsWebApi.Filters;
using MvcVsWebApi.Models;

namespace MvcVsWebApi.Controllers
{
    /// <summary>
    /// All functions about persons
    /// </summary>
    [ConcurrencyExceptionFilter]
    public class PersonController : ApiController
    {
        [Dependency]
        public Lazy<MvcContext> Context { get; set; }
        protected override void Dispose(bool disposing)
        {
            if (Context.IsValueCreated)
                Context.Value.Dispose();
        }

        /// <summary>
        /// Get all persons
        /// </summary>
        /// <returns>Array of person</returns>
        [Queryable(PageSize = 100)]
        public IQueryable<Person> Get()
        {
            return Context.Value.Persons;
        }

        /// <summary>
        /// Get person by id
        /// </summary>
        /// <param name="id">Person id</param>
        /// <returns>Person</returns>
        public Person Get(int id)
        {
            return Context.Value.Persons.Find(id);
        }

        /// <summary>
        /// Get person by name
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>Person</returns>
        [Route("api/person/{name:alpha}")]
        public Person Get(string name)
        {
            return Context.Value.Persons.FirstOrDefault(p => p.Name == name);
        }

        /// <summary>
        /// Add new person
        /// </summary>
        /// <param name="value">Person to insert</param>
        [ValidateModel]
        public void Post(Person value)
        {
            Context.Value.Persons.Add(value);
            Context.Value.SaveChanges();
        }

        /// <summary>
        /// Update person
        /// </summary>
        /// <param name="id">Person id</param>
        /// <param name="value">Person to update</param>
        [AcceptVerbs("PUT", "SEXY")]
        public void Put(int id, Person value)
        {
            var person = new Person
            {
                Id = id,
            };
            Context.Value.Persons.Attach(person);

            person.Name = value.Name;
            person.Age = value.Age;

            Context.Value.SaveChanges();
        }

        /// <summary>
        /// Delete person
        /// </summary>
        /// <param name="id">Person id</param>
        public void Delete(int id)
        {
            var person = new Person
            {
                Id = id,
            };
            Context.Value.Persons.Attach(person);
            Context.Value.Persons.Remove(person);
            Context.Value.SaveChanges();
        }
    }
}