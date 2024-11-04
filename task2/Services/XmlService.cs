using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

public class XmlService
{
    private string _filePath = "";
    private readonly IWebHostEnvironment _environment;
   

    public XmlService(IWebHostEnvironment environment)
    {
        _environment = environment;
        _filePath = Path.Combine(_environment.WebRootPath, "data.xml");

    }

    public List<Person> GetAll()
    {
        var people = new List<Person>();
        XmlDocument doc = new XmlDocument();
        doc.Load(_filePath);

        foreach (XmlNode node in doc.SelectNodes("/persons/person"))
        {
            people.Add(new Person
            {
                Id = int.Parse(node["id"].InnerText),
                Firstname = node["firstname"].InnerText,
                Lastname = node["lastname"].InnerText,
                Age = int.Parse(node["age"].InnerText)
            });
        }

        return people;
    }
    public IActionResult <Person> CreatePerson(Person person)
    {
        Persons.Add(person);
        savetoxml();
    }

    private void savetoxml()
    {
        var serialazer = new XmlSerializer(typeof(List<Person>));
        using (FileStream writer = new FileStream(_filePath,FileMode.OpenOrCreate))
        {
            serialazer.Serialize(writer , Persons);
        }
    }
}
