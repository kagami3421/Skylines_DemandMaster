using System;
using System.Collections.Generic;
using UnityEngine;
using ColossalFramework.UI;

namespace DemandMaster
{
    public class RCIMainUIPanel : UIPanel
    {
        private RCITitlePanel TitlePanel;

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

            TitlePanel = AddUIComponent<RCITitlePanel>();
            TitlePanel.ParentUI = this;

            foreach (RCIType _type in Enum.GetValues(typeof(RCIType)))
            {
                GameObject _singleRow = new GameObject("ViewRow" + _type.ToString());
                RCIViewRow _rowUI = _singleRow.AddComponent<RCIViewRow>();

                _rowUI.ViewRCIType = _type;

                AttachUIComponent(_singleRow);

                _ViewRowArray.Add(_singleRow);
            }

            foreach (RCIType _type in Enum.GetValues(typeof(RCIType)))
            {
                GameObject _singleRow = new GameObject("AdjustRow" + _type.ToString());
                RCIAdjustRow _rowUI = _singleRow.AddComponent<RCIAdjustRow>();

                _rowUI.AdjustRCIType = _type;

                UILabel _Description = AddUIComponent<UILabel>();

                switch (_type)
                {
                    case RCIType.Residential:
                        _Description.text = ModLocaleManager.Instance.CurrentLocale.ResdentialAdjustString;
                        break;
                    case RCIType.Commercial:
                        _Description.text = ModLocaleManager.Instance.CurrentLocale.CommerialAdjustString;
                        break;
                    case RCIType.Industrial:
                        _Description.text = ModLocaleManager.Instance.CurrentLocale.IndustrialAdjustString;
                        break;
                }

                AttachUIComponent(_singleRow);

                _AdjustRowArray.Add(_singleRow);
            }

#if DEBUG
            ModDebug.Log(_ViewRowArray.Count);
            ModDebug.Log(_AdjustRowArray.Count);
#endif
        }
    }
}
