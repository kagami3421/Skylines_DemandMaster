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
    }
    #endregion

    public class DemandMaster : LoadingExtensionBase
    {
        /// <summary>
        /// Flag for checking DemandMaster UI is open.
        /// </summary>
        private bool bIsUIOpen = false;

        /// <summary>
        /// The gameobject of DemandMaster.
        /// </summary>
        private GameObject oDemandPanel;

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

        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);

            if (mode != LoadMode.LoadGame && mode != LoadMode.NewGame)
                return;

            eLoadMode = mode;

            oGameNativeUI = UIView.GetAView();

            if (oGameNativeUI != null)
                RegisterUI();
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

        /// <summary>
        /// Hook DemandMaster to native UI.
        /// </summary>
        void RegisterUI()
        {
            oDemandPanel = new GameObject("DemandPanel");

            oDemandPanelComponent = oDemandPanel.AddComponent<RCIMainUIPanel>();

            oDemandPanelComponent.transform.parent = oGameNativeUI.transform;

            HookToMainToolBar();
        }

        void UnRegistorUI()
        {
            if (oDemandPanelComponent)
                GameObject.Destroy(oDemandPanelComponent.gameObject);
            if (oDemandTriggerButton)
                GameObject.Destroy(oDemandTriggerButton.gameObject);
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
