namespace Podcatcher.Models.Deserialization
{
    public interface IConverter<T>
    {

        T Convert(string contentBody);
    }
}
