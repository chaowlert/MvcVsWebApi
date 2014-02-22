using System;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvcVsWebApi.Controllers;
using MvcVsWebApi.Models;

namespace MvcVsWebApi.UnitTest
{
	[TestClass]
	public class PersonTest
	{
		[TestMethod]
		public void GetTest_ById()
		{
			var mvcContext = new Mock<MvcContext>();
			var persons = new Mock<DbSet<Person>>();
			var person = new Person {Id = 1, Name = "Chaowlert", Age = 20};

			mvcContext.Setup(c => c.Persons).Returns(persons.Object);
			persons.Setup(s => s.Find(1)).Returns(person);

			var controller = new PersonController
			{
				Context = new Lazy<MvcContext>(() => mvcContext.Object)
			};

			var result = controller.Get(1);

			Assert.AreEqual(person, result);
		}
	}
}
