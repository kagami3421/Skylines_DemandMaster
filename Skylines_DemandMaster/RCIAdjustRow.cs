//#define DEBUG

using System;
using System.Collections.Generic;
using UnityEngine;

using ColossalFramework;
using ColossalFramework.UI;

namespace DemandMaster
{
    public class Kagami_RCIAdjustRow : UIPanel
    {
        private Kagami_RCISlider _RCIValueSlider;

        private Kagami_RCICheckBox _RCIFixed;

        private UILabel _RCIFixedText;

        private RCIType _type;

        public RCIType AdjustRCIType
        {
            set
            {
                _type = value;
            }
        }

        private int currentSliderValue;

        public override void Awake()
        {
            base.Awake();

            _RCIValueSlider = AddUIComponent<Kagami_RCISlider>();

            _RCIFixed = AddUIComponent<Kagami_RCICheckBox>();

            _RCIFixedText = AddUIComponent<UILabel>();

            height = 20;
            width = 550;
        }

        public override void Start()
        {
            base.Start();

            _RCIValueSlider.relativePosition = new Vector3(5, 0);
            _RCIValueSlider.eventValueChanged += _RCIValueSlider_eventValueChanged;

            _RCIFixed.relativePosition = new Vector3(250, 0);
            _RCIFixed.eventClick += _RCIFixed_eventClick;

            _RCIFixedText.relativePosition = new Vector3(270, 0);
            _RCIFixedText.text = "Fix Value";

        }

        void _RCIValueSlider_eventValueChanged(UIComponent component, float value)
        {
            FeatureUtil.SetRCIValue(_type, (int)value);

#if !DEBUG
            ModDebug.Log(_type.ToString() + "------" +value.ToString());
#endif
        }

        void _RCIFixed_eventClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            _RCIFixed.IsChecked = !_RCIFixed.IsChecked;

            _RCIValueSlider.isEnabled = !_RCIFixed.IsChecked;
        }

        public override void Update()
        {
            base.Update();

            if (_RCIFixed.IsChecked)
                FeatureUtil.SetRCIValue(_type, currentSliderValue);

            _RCIValueSlider.value = FeatureUtil.GetCurrentRCIValue(_type);
            currentSliderValue = FeatureUtil.GetCurrentRCIValue(_type);
        }
    }
}
