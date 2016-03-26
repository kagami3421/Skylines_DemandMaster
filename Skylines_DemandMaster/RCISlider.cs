using System;
using System.Collections.Generic;
using UnityEngine;

using ColossalFramework;
using ColossalFramework.UI;

namespace DemandMaster
{
    public class Kagami_RCISlider : UISlider
    {
        private UISprite _ThumbObj;

        public override void Start()
        {
            base.Start();

            _ThumbObj = AddUIComponent<UISprite>();
            _ThumbObj.spriteName = "SliderBudget";

            size = new Vector2(200, 10);

            backgroundSprite = "LevelBarBackground";
            canFocus = true;

            maxValue = 100f;
            minValue = 0f;
            orientation = UIOrientation.Horizontal;
            scrollWheelAmount = 1f;
            stepSize = 1f;
            thumbObject = _ThumbObj;
            thumbOffset = new Vector2(0, 2);
        }

    }
}
