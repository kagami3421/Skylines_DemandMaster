using System;
using System.Collections.Generic;
using UnityEngine;

using ColossalFramework;
using ColossalFramework.UI;

namespace DemandMaster
{
    public class TitlePanel : UIPanel
    {
        private UILabel _TitileText;

        private UIDragHandle _drag;

        public UIPanel ParentUI { set; get; }

        public override void Awake()
        {
            base.Awake();

            _TitileText = AddUIComponent<UILabel>();
            _drag = AddUIComponent<UIDragHandle>();

            height = 50;
            width = 550;
        }

        public override void Start()
        {
            base.Start();

            _TitileText.relativePosition = new Vector3(125, 0);
            _TitileText.text = "Demand Master by Kagami";
            _TitileText.textScale = 1.5f;

            _drag.width = width - 50;
            _drag.height = height;
            _drag.relativePosition = Vector3.zero;
            _drag.target = ParentUI;
        }
    }
}
