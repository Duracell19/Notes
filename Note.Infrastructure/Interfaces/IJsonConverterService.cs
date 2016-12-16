namespace Note.Infrastructure.Interfaces
{
    public interface IJsonConverterService
    {
        T Deserialize<T>(string str);
        string Serialize(object obj);
    }
}
