using System;
using System.IO;
using System.Net;

namespace Podcatcher.Models.Deserialization
{
    public class PodcastDeserializer : IPodcastDeserializer
    {

        public XmlConverter XmlConverter {get; set;} = new XmlConverter();

        public Podcast Deserialize(string url)
        {
            string contents;
            if (IsLocalFile(url))
            {
                contents = ReadFileContents(url);
            }
            else
            {
                contents = GetBodyFromUrl(url);
            }

            if (contents == null)
            {
                throw new Exception("Could not read contents");
            }

            return XmlConverter.Convert(contents);
        }

        private bool IsLocalFile(string path)
        {
            return new Uri(path).IsFile;
        }

        private string GetBodyFromUrl(string url)
        {
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.AutomaticDecompression = DecompressionMethods.GZip;

            using (var resp = req.GetResponse())
            using (var stream = resp.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }

        }

        private string ReadFileContents(string filePath)
        {
            return File.ReadAllText(filePath);
        }
    }
}
