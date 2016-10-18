using System.Xml.Serialization;
using UnityEngine;
using System.IO;

namespace DemandMaster
{
    public class ModConfiguration
    {
        public bool showDemandInBar;
        public bool storeDemand;

        public int storedResidentialDemand;
        public int storedCommercialDemand;
        public int storedIndustrialDemand;
    }

    public static class ModeConfigProcessor
    {
        public static void Serialize(string filename, ModConfiguration config)
        {
            var serializer = new XmlSerializer(typeof(ModConfiguration));

            using (var writer = new StreamWriter(filename))
            {
                serializer.Serialize(writer, config);
            }
        }

        public static ModConfiguration Deserialize(string filename)
        {
            var serializer = new XmlSerializer(typeof(ModConfiguration));

            using (var reader = new StreamReader(filename))
            {
                var config = (ModConfiguration)serializer.Deserialize(reader);
                return config;
            }
        }
    }
}
