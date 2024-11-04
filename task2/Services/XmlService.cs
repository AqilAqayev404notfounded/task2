using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public class XmlService
{
    private readonly string _filePath;

    public XmlService(string filePath)
    {
        _filePath = filePath;
    }

    public List<Person> GetAll()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Person>();
        }

        var serializer = new XmlSerializer(typeof(List<Person>));
        using (var reader = new StreamReader(_filePath))
        {
            return (List<Person>)serializer.Deserialize(reader);
        }
    }

    public void SaveToXml(List<Person> persons)
    {
        var serializer = new XmlSerializer(typeof(List<Person>));
        using (var writer = new FileStream(_filePath, FileMode.Create))
        {
            serializer.Serialize(writer, persons);
        }
    }
}
