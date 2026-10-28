using Lab3_3_2.Core;
using Lab3_3_2.DAL.DataProviders;
using Lab3_3_2.DAL.Entities;

namespace Lab3_3_2.DAL;

public class EntityContext<T>(IDataProvider<T> dataProvider, string dataFolderPath) where T : PersonEntity
{
    private string FileName => $"{typeof(T).Name[..^1]}ies.{dataProvider.DataFormat}";
    private string FilePath => Path.Combine(dataFolderPath, FileName);

    public List<T> Load()
    {
        if (!File.Exists(FilePath))
            return [];

        var buffer = File.ReadAllBytes(FilePath);
        return dataProvider.Deserialize(buffer);
    }

    public void Save(List<T> entities)
    {
        var buffer = dataProvider.Serialize(entities);
        File.WriteAllBytes(FilePath, buffer);
    }
}

public static class EntityContext
{
    public static EntityContext<T> Create<T>(string dataFolderPath, string fileType) where T : PersonEntity
    {
        IDataProvider<T> dataProvider = fileType.ToLower() switch
        {
            "json" => new JsonDataProvider<T>(),
            "xml" => new XmlDataProvider<T>(),
            _ => throw new UnknownFileTypeException(),
        };

        return new EntityContext<T>(dataProvider, dataFolderPath);
    }
}
