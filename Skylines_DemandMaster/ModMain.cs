#define DEBUG

using System;
using System.Collections.Generic;
using UnityEngine;

using ICities;
using ColossalFramework;
using ColossalFramework.UI;

namespace DemandMaster
{
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

    public class DemandMaster : LoadingExtensionBase
    {

        private bool bIsUIOpen = false;

        private ToolUI toolUI;

        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);

            if (mode == LoadMode.LoadGame || mode == LoadMode.NewGame)
            {
                CreateButton();
                RegisterUI();
            }
        }

        public override void OnLevelUnloading()
        {
            base.OnLevelUnloading();

            UnRegistorUI();
        }

        private UIButton CreateButton()
        {
            UIView uiView = UIView.GetAView();

            UIButton button = (UIButton)uiView.AddUIComponent(typeof(UIButton));

            button.text = "Demand Master";

            button.width = 120;
            button.height = 40;

            button.normalBgSprite = "ButtonMenu";
            button.disabledBgSprite = "ButtonMenuDisabled";
            button.hoveredBgSprite = "ButtonMenuHovered";
            button.focusedBgSprite = "ButtonMenuFocused";
            button.pressedBgSprite = "ButtonMenuPressed";
            button.textColor = new Color32(255, 255, 255, 255);
            button.disabledTextColor = new Color32(7, 7, 7, 255);
            button.hoveredTextColor = new Color32(7, 132, 255, 255);
            button.focusedTextColor = new Color32(255, 255, 255, 255);
            button.pressedTextColor = new Color32(30, 30, 44, 255);

            button.playAudioEvents = true;

            button.transformPosition = new Vector3(-1f, 0.97f);

            button.eventClick += button_eventClick;

            return button;
        }

        void RegisterUI()
        {
            GameObject gameController = GameObject.FindWithTag("GameController");
            if (gameController)
                toolUI = gameController.AddComponent<ToolUI>();
        }

        void UnRegistorUI()
        {
            GameObject gameController = GameObject.FindWithTag("GameController");
            if (gameController)
            {
                ToolUI _ui = gameController.GetComponent<ToolUI>();
                if (_ui)
                    GameObject.Destroy(_ui);
            }
        }

        void button_eventClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            if (bIsUIOpen)
                bIsUIOpen = false;
            else
                bIsUIOpen = true;

#if DEBUG
            ModDebug.Log("R:" + ZoneManager.instance.m_residentialDemand);
            ModDebug.Log("C:" + ZoneManager.instance.m_commercialDemand);
            ModDebug.Log("I:" + ZoneManager.instance.m_workplaceDemand);

            ModDebug.Log("AR:" + ZoneManager.instance.m_actualResidentialDemand);
            ModDebug.Log("AC:" + ZoneManager.instance.m_actualCommercialDemand);
            ModDebug.Log("AI:" + ZoneManager.instance.m_actualWorkplaceDemand);

            ModDebug.Log(bIsUIOpen);
#endif

            toolUI.EnableUI = bIsUIOpen;
        }
    }
}
