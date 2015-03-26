using System;
using System.Collections.Generic;
using UnityEngine;

using ColossalFramework;
using ColossalFramework.UI;

namespace DemandMaster
{
    public class RCICheckBox : UISprite
    {
        public bool IsChecked { get; set; }

        public override void Awake()
        {
            base.Awake();
            IsChecked = false;
            spriteName = "check-unchecked";
        }

        public override void Update()
        {
            base.Update();
            spriteName = IsChecked ? "check-checked" : "check-unchecked";
        }
    }
}
