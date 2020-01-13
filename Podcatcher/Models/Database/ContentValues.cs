using System.Collections.Generic;

namespace Podcatcher.Models.Database
{
    public class ContentValues
    {

        private readonly Dictionary<string, object> valueDict;

        public ContentValues()
        {
            valueDict = new Dictionary<string, object>();
        }

        public void Add(string key, object value)
        {
            valueDict.Add(key, value);
        }
    }
}
