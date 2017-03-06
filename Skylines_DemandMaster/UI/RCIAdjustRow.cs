using UnityEngine;
using ColossalFramework.UI;

namespace DemandMaster
{
    public class RCIAdjustRow : UIPanel
    {
        private RCISlider _RCIValueSlider;

        private RCICheckBox _RCIFixed;

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

            _RCIValueSlider = AddUIComponent<RCISlider>();

            _RCIFixed = AddUIComponent<RCICheckBox>();

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
            _RCIFixedText.text = ModLocaleManager.Instance.CurrentLocale.FixValueString;

            if (ConfigManager.Instance.CurrentConfig.storeDemand)
            {
                _RCIValueSlider.value = ConfigManager.Instance.GetStoreDemandValue(_type);
                currentSliderValue = ConfigManager.Instance.GetStoreDemandValue(_type);
            }
        }

        void _RCIValueSlider_eventValueChanged(UIComponent component, float value)
        {
            FeatureUtil.SetRCIValue(_type, (int)value);

#if DEBUG
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
            {
                FeatureUtil.SetRCIValue(_type, currentSliderValue);
            }
                

            _RCIValueSlider.value = FeatureUtil.GetCurrentRCIValue(_type);
            currentSliderValue = FeatureUtil.GetCurrentRCIValue(_type);

            if (ConfigManager.Instance.CurrentConfig.storeDemand)
                ConfigManager.Instance.SetStoreDemandValue(_type, currentSliderValue);
        }
    }
}
