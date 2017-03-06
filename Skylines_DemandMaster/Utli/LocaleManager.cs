using ColossalFramework.Globalization;
using ColossalFramework.UI;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System;

namespace DemandMaster
{
    public class LocaleLabel
    {
        public string LanguageCode;

        public string OptionShowDemandBarString;
        public string OptionStoreDemandString;

        public string DemandMasterOpenButtonTooltip;

        public string FixValueString;

        public string ResdentialDemandString;
        public string CommerialDemandString;
        public string IndustrialDemandString;

        public string ResdentialAdjustString;
        public string CommerialAdjustString;
        public string IndustrialAdjustString;
    }

    public class ModLocaleManager
    {
        #region Singleton
        public static ModLocaleManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ModLocaleManager();

                return instance;
            }
        }
        private static ModLocaleManager instance;
        #endregion

        public LocaleLabel CurrentLocale
        {
            get
            {
                string wantedToGetLangCode = LocaleManager.instance.language;
                if (locales.ContainsKey(wantedToGetLangCode))
                    return locales[LocaleManager.instance.language];
                else
                    return locales["en"];
            }
        }

        private Dictionary<string, LocaleLabel> locales = new Dictionary<string, LocaleLabel>();

        private XmlSerializer Serializer;

        public void Init()
        {
            Serializer = new XmlSerializer(typeof(LocaleLabel));

            IOHelper.Instance.InitLocaleFolder();
            LoadLanguageFiles();
        }

        public void CleanUp()
        {
            locales.Clear();
        }

        private void LoadLanguageFiles()
        {
            string[] fileNames = Directory.GetFiles(IOHelper.Instance.FullLocaleFolderPath, "*.xml");

            if (fileNames.Length == 0)
                CreateDeafultLanguageFile();
            else
            {
                foreach (string file in fileNames)
                {
                    string contents = File.ReadAllText(file);
                    LocaleLabel result;

                    using (TextReader reader = new StringReader(contents))
                    {
                        result = (LocaleLabel)Serializer.Deserialize(reader);
                    }

                    if (result != null)
                    {
                        if (locales.ContainsKey(result.LanguageCode))
                            ModDebug.Error("There already exists the same language file !");
                        else
                            locales.Add(result.LanguageCode, result);
                    }
                        
                }
            }
        }

        private void CreateDeafultLanguageFile()
        {
            LocaleLabel defaultLocale = new LocaleLabel();

            defaultLocale.LanguageCode = "en";

            defaultLocale.OptionShowDemandBarString = "Show RCI demand value on status bar";
            defaultLocale.OptionStoreDemandString = "Store RCI demand value";

            defaultLocale.DemandMasterOpenButtonTooltip = "Click to Open DemandMaster !";

            defaultLocale.FixValueString = "FixValue";

            defaultLocale.ResdentialDemandString = "Residential Demand (Max:100):";
            defaultLocale.CommerialDemandString = "Commercial Demand (Max:100):";
            defaultLocale.IndustrialDemandString = "Industrial Demand (Max:100):";

            defaultLocale.ResdentialAdjustString = "Residential Demand Adjust(0~100):";
            defaultLocale.CommerialAdjustString = "Commercial Demand Adjust(0~100):";
            defaultLocale.IndustrialAdjustString = "Industrial Demand Adjust(0~100):";

            using (var writer = new StreamWriter(IOHelper.Instance.FullLocaleFolderPath + "/" + "en.xml"))
            {
                Serializer.Serialize(writer, defaultLocale);
            }

            locales.Add(defaultLocale.LanguageCode, defaultLocale);
        }
    }
}
