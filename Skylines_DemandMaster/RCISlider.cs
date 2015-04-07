using System;
using System.Collections.Generic;
using UnityEngine;

using ColossalFramework;
using ColossalFramework.UI;

namespace DemandMaster
{
    public class RCISlider5 : UISlider
    {
        //private UISprite _FillObj;

        private UISprite _ThumbObj;

        public override void Start()
        {
            base.Start();

            //_FillObj = AddUIComponent<UISprite>();
            //_FillObj.spriteName = "LevelBarForeground";
            //_FillObj.size = new Vector2(200, 20);
            //_FillObj.fillDirection = UIFillDirection.Horizontal;
            //_FillObj.fillAmount = 1f;


            _ThumbObj = AddUIComponent<UISprite>();
            _ThumbObj.spriteName = "SliderBudget";

            size = new Vector2(200, 10);

            backgroundSprite = "LevelBarBackground";
            canFocus = true;
            //fillIndicatorObject = _FillObj;
            //fillMode = UIFillMode.Fill;
            
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
