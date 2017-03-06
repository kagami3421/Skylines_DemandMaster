using ICities;
using ColossalFramework.UI;
using UnityEngine;
using System.Collections;

namespace DemandMaster
{
    public class RCIButton : UIButton
    {
        private UILabel ResidentialDemand;
        private UILabel CommercialDemand;
        private UILabel IndustrialDemand;

        public override void Awake()
        {
            base.Awake();

            if (!ConfigManager.Instance.CurrentConfig.showDemandInBar)
                return;

            ResidentialDemand = AddUIComponent<UILabel>();
            CommercialDemand = AddUIComponent<UILabel>();
            IndustrialDemand = AddUIComponent<UILabel>();
        }

        public override void Start()
        {
            base.Start();

            width = 70;
            height = 43;
            name = "DemandToggleButton";
            tooltip = ModLocaleManager.Instance.CurrentLocale.DemandMasterOpenButtonTooltip;

            hoveredBgSprite = "InfoPanelRCIOIndicator";
            hoveredColor = new Color32(100, 100, 100, 10);

            pressedBgSprite = "InfoPanelRCIOIndicator";
            pressedColor = new Color32(25, 25, 25, 255);

            relativePosition = new Vector3(-2.25f, 0f, 0f);

            if (!ConfigManager.Instance.CurrentConfig.showDemandInBar)
                return;

            //Setup demand display text
            AttachUIComponent(ResidentialDemand.gameObject);
            ResidentialDemand.relativePosition = new Vector3(6, 15);

            AttachUIComponent(CommercialDemand.gameObject);
            CommercialDemand.relativePosition = new Vector3(28, 15);

            AttachUIComponent(IndustrialDemand.gameObject);
            IndustrialDemand.relativePosition = new Vector3(52, 15);
        }

        public override void Update()
        {
            base.Update();

            if (!ConfigManager.Instance.CurrentConfig.showDemandInBar)
                return;

            ResidentialDemand.text = FeatureUtil.GetCurrentRCIValue(RCIType.Residential).ToString();
            CommercialDemand.text = FeatureUtil.GetCurrentRCIValue(RCIType.Commercial).ToString();
            IndustrialDemand.text = FeatureUtil.GetCurrentRCIValue(RCIType.Industrial).ToString();
        }
    }
}
