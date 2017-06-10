using ColossalFramework.Plugins;
using System.IO;

namespace DemandMaster
{
    public class IOHelper
    {
        #region Singleton
        public static IOHelper Instance
        {
            get
            {
                if (instance == null)
                    instance = new IOHelper();

                return instance;
            }
        }
        private static IOHelper instance;
        #endregion

        private readonly string mainFolderName = "DemandMaster";
        private readonly string block = "/";

        private readonly string configFileName = "Config.xml";

        private readonly string localeFolderName = "Locale";
        //private readonly string localeFileExtension = ".xml";

        public string FullConfigPath
        {
            get
            {
                return WorkshopFilePath + block + mainFolderName + block + configFileName;
            }
        }
        public string FullLocaleFolderPath
        {
            get
            {
                return WorkshopFilePath + block + mainFolderName + block + localeFolderName;
            }
        }
        public string FullMainFolderPath
        {
            get
            {
                return WorkshopFilePath + block + mainFolderName;
            }
        }

        public IOHelper()
        {
            GetModWorkshopPath();
        }

        private string WorkshopFilePath;

        private void InitMainFolder()
        {
            if(!Directory.Exists(FullMainFolderPath))
                Directory.CreateDirectory(FullMainFolderPath);
        }

        public void InitLocaleFolder()
        {
            InitMainFolder();

            if (!Directory.Exists(FullLocaleFolderPath))
                Directory.CreateDirectory(FullLocaleFolderPath);
        }

        public bool CheckConfigExist()
        {
            InitMainFolder();

            if (File.Exists(FullConfigPath))
                return true;
            else
                return false; 
        }

        private void GetModWorkshopPath()
        {
            if (string.IsNullOrEmpty(WorkshopFilePath))
            {
                PluginManager pluginManager = ColossalFramework.Singleton<PluginManager>.instance;

                foreach (PluginManager.PluginInfo pluginInfo in pluginManager.GetPluginsInfo())
                {
                    if (pluginInfo.modPath.Contains(409776678.ToString()))
                    {
                        WorkshopFilePath = pluginInfo.modPath;
                    }
                }
            }
        }
    }
}
