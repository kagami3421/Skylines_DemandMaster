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

        public void OnSettingsUI(UIHelperBase helper)
        {
            RCIButton.config = ModeConfigProcessor.Deserialize(RCIButton.configPath);
            if (RCIButton.config == null)
            {
                RCIButton.config = new ModConfiguration();
            }
            RCIButton.SaveConfig();

            UIHelperBase group = helper.AddGroup("Demand Master Settings");

            group.AddCheckbox("Show RCI demand value on status bar", RCIButton.config.showDemandInBar, OnCheckShowDemandBar);
            //group.AddCheckbox("Store RCI fixed demand value", RCIButton.config.storeDemand, OnCheckStoreFixedDemand);
        }

        private void OnCheckShowDemandBar(bool c)
        {
            RCIButton.config.showDemandInBar = c;
            RCIButton.SaveConfig();
        }

        private void OnCheckStoreFixedDemand(bool c)
        {
            RCIButton.config.storeDemand = c;
            RCIButton.SaveConfig();
        }
    }
    #endregion

    public class DemandMaster : LoadingExtensionBase
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
            base.OnCreated(loading);

            RCIButton.config = ModeConfigProcessor.Deserialize(RCIButton.configPath);
            if (RCIButton.config == null)
            {
                RCIButton.config = new ModConfiguration();
            }
            RCIButton.SaveConfig();
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
                ModDebug.Error("Can't Find UI View !");
        }

        public override void OnLevelUnloading()
        {
            base.OnLevelUnloading();

            if (eLoadMode != LoadMode.LoadGame && eLoadMode != LoadMode.NewGame)
                return;

            UnRegistorUI();
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
            ModDebug.Log("R:" + ZoneManager.instance.m_residentialDemand);
            ModDebug.Log("C:" + ZoneManager.instance.m_commercialDemand);
            ModDebug.Log("I:" + ZoneManager.instance.m_workplaceDemand);

            ModDebug.Log("AR:" + ZoneManager.instance.m_actualResidentialDemand);
            ModDebug.Log("AC:" + ZoneManager.instance.m_actualCommercialDemand);
            ModDebug.Log("AI:" + ZoneManager.instance.m_actualWorkplaceDemand);
#endif

            oDemandPanelComponent.isVisible = bIsUIOpen;
        }
    }
}
