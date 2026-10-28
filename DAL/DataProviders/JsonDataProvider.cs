using Lab3_3_2.Core;
using Lab3_3_2.DAL.Entities;
using System.Text;
using System.Text.Json;

namespace Lab3_3_2.DAL.DataProviders;

public class JsonDataProvider<T> : IDataProvider<T> where T : PersonEntity
{
    public string DataFormat => "json";

    public byte[] Serialize(List<T> data)
    {
        var str = JsonSerializer.Serialize(data);
        return Encoding.UTF8.GetBytes(str);
    }

    public List<T> Deserialize(byte[] buffer)
    {
        var str = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
        return JsonSerializer.Deserialize<List<T>>(str) ?? [];
    }
}
