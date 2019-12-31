using System.Xml.Linq;

namespace Podcatcher.Models.Deserialization
{
    public static class XmlExtensionMethods
    {


        public static string GetAttributeValueOrDefault(this XAttribute attribute, string nullValue)
        {
            if (attribute != null)
                return attribute.Value;

            return nullValue;
        }

        public static string GetAttributeOfElement(this XElement element, string elemName, string attrName, string nullValue)
        {
            var e = element.Element(elemName);
            if (e != null)
            {
                var a = e.Attribute(attrName);
                if (e != null)
                {
                    return a.Value;
                }
            }

            return nullValue;
        }
    }
}
