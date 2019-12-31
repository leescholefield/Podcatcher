namespace Podcatcher.Models.Deserialization
{
    public interface IPodcastDeserializer
    {

        Podcast Deserialize(string url);
    }
}
