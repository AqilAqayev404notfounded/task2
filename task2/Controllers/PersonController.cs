using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
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
        }
        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {
            if (person == null)
            {
                return BadRequest("Person yoxdu ");
            }

            xmlService.WritePersonToXml(person);

            var updatedList = xmlService.GetAll();
            return Ok(updatedList);
        }

        [HttpDelete("{id?}")]
        public IActionResult Delete(int id)
        {
            xmlService.DeletePersonToXml(id);
            var updatedList = xmlService.GetAll();
            return Ok(updatedList);
        }
       
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Person person)
        {
            if (person == null || person.Id != id)
            {
                return BadRequest("Person yoxdu");
            }

            xmlService.UpdatePersonToXml(person);

            var updatedList = xmlService.GetAll();
            return Ok(updatedList);
        }


    }
}
