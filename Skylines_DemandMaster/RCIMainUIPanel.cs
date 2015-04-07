//#define DEBUG

using System;
using System.Collections.Generic;
using UnityEngine;

using ColossalFramework;
using ColossalFramework.UI;

namespace DemandMaster
{
    public class RCIMainUIPanel5 : UIPanel
    {
        private RCITitlePanel5 TitlePanel;

        private List<GameObject> _ViewRowArray;
        private List<GameObject> _AdjustRowArray;

        public override void Start()
        {
            base.Start();

            // Set UI
            
            backgroundSprite = "MenuPanel";
            width = 550;
            height = 250;
            isVisible = false;
            canFocus = true;
            isInteractive = true;

            autoLayout = true;
            autoLayoutDirection = LayoutDirection.Vertical;
            autoLayoutPadding = new RectOffset(0, 0, 1, 1);
            autoLayoutStart = LayoutStart.TopLeft;

            SetUIRows();
        }

        private void SetUIRows()
        {
            _ViewRowArray = new List<GameObject>();
            _AdjustRowArray = new List<GameObject>();

            TitlePanel = AddUIComponent<RCITitlePanel5>();
            TitlePanel.ParentUI = this;

            foreach (RCIType _type in Enum.GetValues(typeof(RCIType)))
            {
                GameObject _singleRow = new GameObject("ViewRow" + _type.ToString());
                RCIViewRow5 _rowUI = _singleRow.AddComponent<RCIViewRow5>();

                _rowUI.ViewRCIType = _type;

                this.AttachUIComponent(_singleRow);

                _ViewRowArray.Add(_singleRow);
            }

            foreach (RCIType _type in Enum.GetValues(typeof(RCIType)))
            {
                GameObject _singleRow = new GameObject("AdjustRow" + _type.ToString());
                RCIAdjustRow5 _rowUI = _singleRow.AddComponent<RCIAdjustRow5>();

                _rowUI.AdjustRCIType = _type;

                UILabel _Description = AddUIComponent<UILabel>();

                switch (_type)
                {
                    case RCIType.Residential:
                        _Description.text = "Residential Demand Adjust(0~100):";
                        break;
                    case RCIType.Commercial:
                        _Description.text = "Commercial Demand Adjust(0~100):";
                        break;
                    case RCIType.Industrial:
                        _Description.text = "Industrial Demand Adjust(0~100):";
                        break;
                }

                this.AttachUIComponent(_singleRow);

                _AdjustRowArray.Add(_singleRow);
            }

#if !DEBUG
            ModDebug.Log(_ViewRowArray.Count);
            ModDebug.Log(_AdjustRowArray.Count);
#endif
        }
    }
}
