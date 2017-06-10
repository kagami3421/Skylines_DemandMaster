using UnityEngine;
using ICities;
using ColossalFramework.UI;

namespace DemandMaster
{

    #region Mod
    public class ModMain : IUserMod
    {
        public string Description
        {
            get { return "View or Change RCI Demand. Author:Kagami"; }
        }

        public string Name
        {
            get { return "Demand Master"; }
        }

        public void OnEnabled()
        {
            Albedo.Instance.EnableMod();
        }

        public void OnDisabled()
        {
            Albedo.Instance.DisableMod();
        }

        public void OnSettingsUI(UIHelperBase helper)
        {
            Albedo.Instance.SetUpSettingUI(helper);
        }
    }
    #endregion

    public class Albedo
    {
        #region Singleton
        public static Albedo Instance
        {
            get
            {
                if (instance == null)
                    instance = new Albedo();

                return instance;
            }
        }
        private static Albedo instance;
        #endregion

        public void EnableMod()
        {
            ConfigManager.Instance.LoadConfigXML();
            ModLocaleManager.Instance.Init();
        }

        public void DisableMod()
        {
            ModLocaleManager.Instance.CleanUp();
        }

        public void SetUpSettingUI(UIHelperBase helper)
        {
            helper.AddCheckbox(ModLocaleManager.Instance.CurrentLocale.OptionShowDemandBarString, ConfigManager.Instance.CurrentConfig.showDemandInBar, OnCheckShowDemandBar);
            helper.AddCheckbox(ModLocaleManager.Instance.CurrentLocale.OptionStoreDemandString, ConfigManager.Instance.CurrentConfig.storeDemand, OnCheckStoreFixedDemand);
        }

        private void OnCheckShowDemandBar(bool c)
        {
            ConfigManager.Instance.CurrentConfig.showDemandInBar = c;

            ConfigManager.Instance.SaveConfigXML();
        }

        private void OnCheckStoreFixedDemand(bool c)
        {
            ConfigManager.Instance.CurrentConfig.storeDemand = c;

            ConfigManager.Instance.SaveConfigXML();
        }
    }

    public class DemandMasterGame : LoadingExtensionBase
    {
        /// <summary>
        /// Flag for checking DemandMaster UI is open.
        /// </summary>
        private bool bIsUIOpen = false;

        /// <summary>
        /// The main UI of DemandMaster.
        /// </summary>
        private RCIMainUIPanel oDemandPanelComponent;

        /// <summary>
        /// The button UI for show/hide DemandMaster.
        /// </summary>
        private RCIButton oDemandTriggerButton;

        /// <summary>
        /// Store the mode of loading type. 
        /// </summary>
        private LoadMode eLoadMode;

        /// <summary>
        /// The Cities skylines main UI.
        /// </summary>
        private UIView oGameNativeUI;

        public override void OnCreated(ILoading loading)
        {
            ConfigManager.Instance.LoadConfigXML();

            base.OnCreated(loading);
        }

        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);

            if (mode != LoadMode.LoadGame && mode != LoadMode.NewGame)
                return;

            eLoadMode = mode;

            oGameNativeUI = UIView.GetAView();

            if (oGameNativeUI != null)
                RegisterDemandUI();
            else
                Debug.LogError("Can't Find UI View !");
        }

        public override void OnLevelUnloading()
        {
            base.OnLevelUnloading();

            if (eLoadMode != LoadMode.LoadGame && eLoadMode != LoadMode.NewGame)
                return;

            UnRegistorUI();

            ConfigManager.Instance.SaveConfigXML();
        }

        /********** Custom Methods **********/

        private void HookToMainToolBar()
        {
            UISprite _demandBack = (UISprite)oGameNativeUI.FindUIComponent("DemandBack");

            oDemandTriggerButton = _demandBack.AddUIComponent<RCIButton>();
            CreateButton(ref oDemandTriggerButton);
        }

        private void CreateButton(ref RCIButton btn)
        {
            btn.eventClick += button_eventClick;
        }

        void UnRegistorUI()
        {
            if (oDemandPanelComponent)
                GameObject.Destroy(oDemandPanelComponent.gameObject);
            if (oDemandTriggerButton)
                GameObject.Destroy(oDemandTriggerButton.gameObject);
        }

        void RegisterDemandUI()
        {
            GameObject oDemandPanel = new GameObject("DemandPanel");

            if (oDemandPanel == null)
                return;

            oDemandPanelComponent = oDemandPanel.AddComponent<RCIMainUIPanel>();

            oDemandPanelComponent.transform.parent = oGameNativeUI.transform;

            HookToMainToolBar();
        }

        void button_eventClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            bIsUIOpen = !bIsUIOpen;

#if DEBUG
            Debug.Log("R:" + ZoneManager.instance.m_residentialDemand);
            Debug.Log("C:" + ZoneManager.instance.m_commercialDemand);
            Debug.Log("I:" + ZoneManager.instance.m_workplaceDemand);

            Debug.Log("AR:" + ZoneManager.instance.m_actualResidentialDemand);
            Debug.Log("AC:" + ZoneManager.instance.m_actualCommercialDemand);
            Debug.Log("AI:" + ZoneManager.instance.m_actualWorkplaceDemand);
#endif

            oDemandPanelComponent.isVisible = bIsUIOpen;
        }
    }
}
