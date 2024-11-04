using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace task2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PersonController : ControllerBase
    {
        private string _filePath = "";
        private readonly IWebHostEnvironment webHoste;
        private readonly XmlService xmlService; 
        private static List<Person> Persons = new List<Person>
    {
        new Person{Id = 2, Firstname="Aqil",Lastname="Agayev",Age=21}
    };


        public PersonController(IWebHostEnvironment webHost, XmlService xmlServices)
        {
            webHoste = webHost;
            _filePath = Path.Combine(webHoste.WebRootPath, "data.xml");
            xmlService = xmlServices;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var personList = xmlService.GetAll();
            return Ok(personList);
            //if (!System.IO.File.Exists(_filePath))
            //{
            //    return NotFound("XML yoxdur.");
            //}

            //var serializer = new XmlSerializer(typeof(List<Person>));
            //using (var reader = new StreamReader(_filePath))
            //{
            //    var root = (List<Person>)serializer.Deserialize(reader);
            //    return Ok(root);
            //}


        }
        public IActionResult CreatePerson(Person person)
        {
            Persons.Add(person);
            xmlService.SaveToXml(Persons);
            return Ok(Persons);
        }

      


    }
}
