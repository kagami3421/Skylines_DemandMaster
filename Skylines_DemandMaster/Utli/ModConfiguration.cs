using System.IO;
using System.Xml.Serialization;

namespace DemandMaster
{
    public class Configuration
    {
        public bool showDemandInBar;
        public bool storeDemand;

        public int storedResidentialDemand;
        public int storedCommercialDemand;
        public int storedIndustrialDemand;

        public int LCIDCode;
    }

    public class ConfigManager
    {

        #region Singleton
        public static ConfigManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ConfigManager();

                return instance;
            }
        }
        private static ConfigManager instance;
        #endregion

        public ConfigManager()
        {
            Serializer = new XmlSerializer(typeof(Configuration));

            if (currentConfig == null)
                currentConfig = new Configuration();
        }

        public Configuration CurrentConfig
        {
            get
            {
                return currentConfig;
            }
            set
            {
                currentConfig = value;
            }
        }
        private Configuration currentConfig;

        private XmlSerializer Serializer;

        public void LoadConfigXML()
        {
            if (IOHelper.Instance.CheckConfigExist())
            {
                try
                {
                    using (var reader = new StreamReader(IOHelper.Instance.FullConfigPath))
                    {
                        currentConfig = (Configuration)Serializer.Deserialize(reader);
                    }
                }
                catch
                {
                    //Preventing protection
                    //Creating new file if the manager doesn't find file.
                    SaveConfigXML();
                }
            }
            else
            {
                SaveConfigXML();
            }
        }

        public void SaveConfigXML()
        {
            using (var writer = new StreamWriter(IOHelper.Instance.FullConfigPath))
            {
                Serializer.Serialize(writer, currentConfig);
            }
        }

        public void SetStoreDemandValue(RCIType type , int value)
        {
            switch (type)
            {
                case RCIType.Residential:
                    {
                        currentConfig.storedResidentialDemand = value;
                    }
                    break;
                case RCIType.Commercial:
                    {
                        currentConfig.storedCommercialDemand = value;
                    }
                    break;
                case RCIType.Industrial:
                    {
                        currentConfig.storedIndustrialDemand = value;
                    }
                    break;
            }
        }

        public int GetStoreDemandValue(RCIType type)
        {
            int tempValue = 0;
            switch (type)
            {
                case RCIType.Residential:
                    tempValue = currentConfig.storedResidentialDemand;
                    break;
                case RCIType.Commercial:
                    tempValue = currentConfig.storedCommercialDemand;
                    break;
                case RCIType.Industrial:
                    tempValue = currentConfig.storedIndustrialDemand;
                    break;
            }

            return tempValue;
        }
    }
}
