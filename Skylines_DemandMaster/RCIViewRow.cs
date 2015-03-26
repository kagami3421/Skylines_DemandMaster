using System;
using System.Collections.Generic;
using UnityEngine;

using ColossalFramework;
using ColossalFramework.UI;

namespace DemandMaster
{
    public enum RCIType
    {
        Residential,
        Commercial,
        Industrial
    }

    public class RCIViewRow : UIPanel
    {
        private UILabel _ViewDescription;

        private UILabel _ViewNumber;

        private string description = "";

        private RCIType _type;

        public RCIType ViewRCIType
        {
            set
            {
                switch (value)
                {
                    case RCIType.Residential:
                        description = "Residential Demand (Max:100):";
                        break;
                    case RCIType.Commercial:
                        description = "Commercial Demand (Max:100):";
                        break;
                    case RCIType.Industrial:
                        description = "Industrial Demand (Max:100):";
                        break;
                }

                _type = value;
            }
        }

        public override void Awake()
        {
            base.Awake();

            _ViewDescription = AddUIComponent<UILabel>();

            _ViewNumber = AddUIComponent<UILabel>();

            height = 20;
            width = 550;
        }

        public override void Start()
        {
            base.Start();

            _ViewDescription.relativePosition = new Vector3(5, 0);
            _ViewNumber.relativePosition = new Vector3(300, 0);

            switch (_type)
            {
                case RCIType.Residential:
                    {
                        _ViewDescription.textColor = new Color32(85, 255, 0, 255);
                        _ViewNumber.textColor = new Color32(85, 255, 0, 255);
                    }
                    break;
                case RCIType.Commercial:
                    {
                        _ViewDescription.textColor = new Color32(0, 100, 255, 255);
                        _ViewNumber.textColor = new Color32(0, 100, 255, 255);
                    }
                    break;
                case RCIType.Industrial:
                    {
                        _ViewDescription.textColor = new Color32(255, 110, 0, 255);
                        _ViewNumber.textColor = new Color32(255, 110, 0, 255);
                    }
                    break;
            }

            _ViewDescription.text = description;
        }

        public override void Update()
        {
            base.Update();

            _ViewNumber.text = FeatureUtil.GetCurrentRCIValue(_type).ToString();
        }
    }
}
