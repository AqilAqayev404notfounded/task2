using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

public class XmlService
{
    private readonly string _filePath="";
    private readonly IWebHostEnvironment webHoste;


  

    public XmlService(IWebHostEnvironment webHoste)
    {
        this.webHoste = webHoste;
        _filePath = Path.Combine(webHoste.WebRootPath, "data.xml");

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

    public void WritePersonToXml(Person person)
    {
        XDocument doc;
        if (System.IO.File.Exists(_filePath))
        {
            doc = XDocument.Load(_filePath); 
        }
        else
        {
            doc = new XDocument(new XElement("Persons")); 
        }

       
        XElement newPerson = new XElement("person",

            new XElement("id", person.Id),
            new XElement("firstname", person.Firstname),
            new XElement("lastname" ,person.Lastname),
            new XElement("age", person.Age)
        );

    
        doc.Element("persons").Add(newPerson);

     
        doc.Save(_filePath);
    }

    public void DeletePersonToXml(int id)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(_filePath);

        XmlNode personNode = doc.SelectSingleNode($"/persons/person[id='{id}']");

        if (personNode != null)
        {
            personNode.ParentNode.RemoveChild(personNode);
            doc.Save(_filePath);
        }
    }

    public void UpdatePersonToXml(Person person)
    {
        XDocument doc;
        if (System.IO.File.Exists(_filePath))
        {
            doc = XDocument.Load(_filePath);
        }
        else
        {
            doc = new XDocument(new XElement("Persons"));
        }

        XElement existingPerson = doc.Descendants("person").FirstOrDefault(p => (int)p.Element("id") == person.Id);

        if (existingPerson != null)
        {
            existingPerson.Element("firstname").Value = person.Firstname;
            existingPerson.Element("lastname").Value = person.Lastname;
            existingPerson.Element("age").Value = person.Age.ToString();
        }
        else
        {

            XElement newPerson = new XElement("person",
                new XElement("id", person.Id),
                new XElement("firstname", person.Firstname),
                new XElement("lastname", person.Lastname),
                new XElement("age", person.Age)
            );

            doc.Element("persons").Add(newPerson);
        }

        doc.Save(_filePath);
    }
}
