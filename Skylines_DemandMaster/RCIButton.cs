using ICities;
using ColossalFramework.UI;
using UnityEngine;
using System.Collections;

namespace DemandMaster
{
    class RCIButton : UIButton
    {
        private UILabel ResidentialDemand;
        private UILabel CommercialDemand;
        private UILabel IndustrialDemand;

        private int RDemand;
        private int CDemand;
        private int IDemand;

        public override void Awake()
        {
            base.Awake();

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
            tooltip = "Click to Open DemandMaster !";

            hoveredBgSprite = "InfoPanelRCIOIndicator";
            hoveredColor = new Color32(100, 100, 100, 10);

            pressedBgSprite = "InfoPanelRCIOIndicator";
            pressedColor = new Color32(25, 25, 25, 255);

            relativePosition = new Vector3(-2.25f, 0f, 0f);

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

            RDemand = FeatureUtil.GetCurrentRCIValue(RCIType.Residential);
            CDemand = FeatureUtil.GetCurrentRCIValue(RCIType.Commercial);
            IDemand = FeatureUtil.GetCurrentRCIValue(RCIType.Industrial);

            ResidentialDemand.text = RDemand.ToString();
            CommercialDemand.text = CDemand.ToString();
            IndustrialDemand.text = IDemand.ToString();
        }
    }
}
