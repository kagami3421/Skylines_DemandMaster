using System;
using System.Collections.Generic;
using UnityEngine;

namespace DemandMaster
{
    public class ZoneVariable
    {
        public int SliderValue { get; set; }
        public bool FixedValue { get; set; }

        public ZoneVariable()
        {
            SliderValue = 0;
            FixedValue = false;
        }
    }

    public class ToolUI : MonoBehaviour
    {

        public bool EnableUI
        {
            set
            {
                bUiVisible = value;
            }
        }
        private bool bUiVisible = false;

        private List<ZoneVariable> ZoneVars = new List<ZoneVariable>();

        private Rect winSize = new Rect(15, 15, 500, 250);

        private ZoneManager _manager;

        void Start()
        {
            for (int i = 0; i < 3; i++)
            {
                ZoneVariable tmp = new ZoneVariable();
                ZoneVars.Add(tmp);
            }

            _manager = ZoneManager.instance;
        }

        void OnGUI()
        {
            if(bUiVisible)
                winSize = GUI.Window(1, winSize, DemandMasterWindow, "RCI Demand Master by Kagami");
        }

        void DemandMasterWindow(int windID)
        {
            

            GUILayout.BeginVertical();

            GUILayout.Label("Residential Demand (Max:100):" + _manager.m_residentialDemand);
            GUILayout.Label("Commercial Demand (Max:100):" + _manager.m_commercialDemand);
            GUILayout.Label("Industrial Demand (Max:100):" + _manager.m_workplaceDemand);

            GUILayout.Space(15);

            GUILayout.Label("Residential Demand Adjust(0~100):");

            #region Residential

            GUILayout.BeginHorizontal();
            GUI.enabled = !ZoneVars[0].FixedValue;

            if (ZoneVars[0].FixedValue)
                _manager.m_residentialDemand = _manager.m_actualResidentialDemand = ZoneVars[0].SliderValue;

            ZoneVars[0].SliderValue = _manager.m_actualResidentialDemand = _manager.m_residentialDemand = (int)GUILayout.HorizontalSlider(_manager.m_residentialDemand, 0f, 100f);
            
            GUI.enabled = true;
            ZoneVars[0].FixedValue = GUILayout.Toggle(ZoneVars[0].FixedValue, "Fix Value");
            GUILayout.EndHorizontal();

            #endregion

            GUILayout.Label("Commercial Demand Adjust(0~100):");

            #region Commercial

            GUILayout.BeginHorizontal();
            GUI.enabled = !ZoneVars[1].FixedValue;

            if (ZoneVars[1].FixedValue)
                _manager.m_commercialDemand = _manager.m_actualCommercialDemand = ZoneVars[1].SliderValue;

            ZoneVars[1].SliderValue = _manager.m_commercialDemand = _manager.m_actualCommercialDemand = (int)GUILayout.HorizontalSlider(_manager.m_commercialDemand, 0f, 100f);

            GUI.enabled = true;
            ZoneVars[1].FixedValue = GUILayout.Toggle(ZoneVars[1].FixedValue, "Fix Value");
            GUILayout.EndHorizontal();

            #endregion

            GUILayout.Label("Industrial Demand Adjust(0~100):");

            #region Industrial

            GUILayout.BeginHorizontal();
            GUI.enabled = !ZoneVars[2].FixedValue;

            if (ZoneVars[2].FixedValue)
                _manager.m_workplaceDemand = _manager.m_actualWorkplaceDemand = ZoneVars[2].SliderValue;

            ZoneVars[2].SliderValue = _manager.m_workplaceDemand = _manager.m_actualWorkplaceDemand = (int)GUILayout.HorizontalSlider(_manager.m_workplaceDemand, 0f, 100f);

            GUI.enabled = true;
            ZoneVars[2].FixedValue = GUILayout.Toggle(ZoneVars[2].FixedValue, "Fix Value");
            GUILayout.EndHorizontal();

            #endregion

            GUILayout.EndVertical();

            GUI.DragWindow();
        }
    }
}
