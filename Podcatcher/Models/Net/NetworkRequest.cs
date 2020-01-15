using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Podcatcher.Models.Net
{
    /// <summary>
    /// Sends an HTTP GET request to the specifield url.
    /// </summary>
    public class NetworkRequest
    {

        private readonly string Url;

        public NetworkRequest(string url)
        {
            Url = url;
        }

        /// <summary>
        /// Sends an HTTP GET request to the url passed to the constructor and then returns the response content as a string.
        /// </summary>
        public string SendRequest()
        {
            var req = (HttpWebRequest)WebRequest.Create(Url);
            var resp = (HttpWebResponse)req.GetResponse();
            if ( ((int)resp.StatusCode >= 200) && ((int)resp.StatusCode <= 299) )
            {
                Stream stream = resp.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                return reader.ReadToEnd();
            }
            else
            {
                throw new WebException("Got bad statuc code: " + resp.StatusCode);
            }
        }


    }
}
