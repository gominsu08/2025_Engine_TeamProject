using GMS.Code.Core;
using GMS.Code.Core.Events;
using GMS.Code.UI;
using GMS.Code.UI.MainPanel;
using System;
using UnityEngine;

namespace GMS.Code.UI.TileInfoUIPanel
{
    public class HeadQuarterCallButton : MonoBehaviour, IUIElement
    {
        [SerializeField] private ButtonUI buttonUI;

        public void Awake()
        {
            buttonUI.Init(null,true);
        }

        public void DisableUI()
        {
            buttonUI.DisableUI();
        }

        public void EnableForUI()
        {
            buttonUI.EnableForUI(new ToolBarUIData(), HandleButtonClick);
        }

        private void HandleButtonClick()
        {
            Bus<HeadQurterCallEvent>.Raise(new HeadQurterCallEvent());
        }
    }
}