using Microsoft.VisualStudio.TestTools.UnitTesting;
using Podcatcher.Models.Deserialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podcatcher.Models.Deserialization.Tests
{
    [TestClass()]
    public class PodcastDeserializerTests
    {
        private PodcastDeserializer Deserializer { get; set; }

        [TestInitialize()]
        public void Setup()
        {
            Deserializer = new PodcastDeserializer();
        }
    }
}