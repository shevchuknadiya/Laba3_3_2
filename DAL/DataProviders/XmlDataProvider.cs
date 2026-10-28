using Lab3_3_2.Core;
using Lab3_3_2.DAL.Entities;
using System.Text;
using System.Xml.Serialization;

namespace Lab3_3_2.DAL.DataProviders;

public class XmlDataProvider<T> : IDataProvider<T> where T : PersonEntity
{
    public string DataFormat => "xml";

    public byte[] Serialize(List<T> data)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));
        using var strWriter = new StringWriter();
        xmlSerializer.Serialize(strWriter, data.ToList());
        return Encoding.UTF8.GetBytes(strWriter.ToString());
    }

    public List<T> Deserialize(byte[] buffer)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));
        var str = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
        using var strReader = new StringReader(str);
        return xmlSerializer.Deserialize(strReader) as List<T> ?? [];
    }
}
