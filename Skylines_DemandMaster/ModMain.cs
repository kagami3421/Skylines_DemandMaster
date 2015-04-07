//#define DEBUG

using System;
using System.Collections.Generic;
using UnityEngine;

using ICities;
using ColossalFramework;
using ColossalFramework.UI;

namespace DemandMaster
{

    #region Mod
    public class ModMain : IUserMod
    {
        public string Description
        {
            get { return "View or Change RCI Demand"; }
        }

        public string Name
        {
            get { return "Demand Master"; }
        }
    }
    #endregion

    public class DemandMaster : LoadingExtensionBase
    {

        private bool bIsUIOpen = false;

        private GameObject _DemandPanelGO;

        private RCIMainUIPanel5 _DemandPanel;

        private UIButton _DemandBtn;

        private LoadMode _mode;

        private UIView _view;

        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);

            if (mode != LoadMode.LoadGame && mode != LoadMode.NewGame)
                return;

            _mode = mode;

            _view = UIView.GetAView();

            RegisterUI();
        }

        public override void OnLevelUnloading()
        {
            base.OnLevelUnloading();

            if (_mode != LoadMode.LoadGame && _mode != LoadMode.NewGame)
                return;

            UnRegistorUI();
        }

        private void HookToMainToolBar()
        {
            if (_view == null)
            {
                ModDebug.Error("Can't Find UI View !");
                return;
            }

            UISprite _demandBack = (UISprite)_view.FindUIComponent("DemandBack");

            _DemandBtn = _demandBack.AddUIComponent<UIButton>();
            CreateButton(ref _DemandBtn);
        }

        private void CreateButton(ref UIButton _btn)
        {
            _btn.width = 70;
            _btn.height = 43;
            _btn.name = "DemandToggleButton";

            _btn.pressedBgSprite = "InfoPanelRCIOIndicator";

            _btn.pressedColor = new Color32(25, 25, 25, 255);

            _btn.relativePosition = new Vector3(-2.25f, 0f , 0f);

            _btn.eventClick += button_eventClick;
        }

        void RegisterUI()
        {
            if (_view == null)
            {
                ModDebug.Error("Can't Find UI View !");
                return;
            }

            _DemandPanelGO = new GameObject("DemandPanel");

            _DemandPanel = _DemandPanelGO.AddComponent<RCIMainUIPanel5>();

            _DemandPanel.transform.parent = _view.transform;


            HookToMainToolBar();
        }

        void UnRegistorUI()
        {
            if (_DemandPanel)
                GameObject.Destroy(_DemandPanel.gameObject);
            if (_DemandBtn)
                GameObject.Destroy(_DemandBtn.gameObject);
        }

        void button_eventClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            bIsUIOpen = !bIsUIOpen;

#if !DEBUG
            ModDebug.Log("R:" + ZoneManager.instance.m_residentialDemand);
            ModDebug.Log("C:" + ZoneManager.instance.m_commercialDemand);
            ModDebug.Log("I:" + ZoneManager.instance.m_workplaceDemand);

            ModDebug.Log("AR:" + ZoneManager.instance.m_actualResidentialDemand);
            ModDebug.Log("AC:" + ZoneManager.instance.m_actualCommercialDemand);
            ModDebug.Log("AI:" + ZoneManager.instance.m_actualWorkplaceDemand);
#endif

            //if (_DemandPanel)
                _DemandPanel.isVisible = bIsUIOpen;
            //else
                //ModDebug.Error("Demand Panel Vairable is NULL !");

        }
    }
}
