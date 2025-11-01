using GMS.Code.Core.System.Machines;
using GMS.Code.Items;
using GMS.Code.UI.MainPanel;
using System;
using System.Threading;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

namespace GMS.Code.UI.Braziers
{
    public class SelectItemPanel : MonoBehaviour, IUIElement<Action<ItemSO,ItemSO>,Brazier>
    {
        [SerializeField] private ButtonUI resourceButton, fuelButton;
        [SerializeField] private SelectItemFuelPanel selectItemFuelPanel;
        [SerializeField] private SelectItemResourcePanel selectItemResourcePanel;

        private Action<ItemSO,ItemSO> action;
        private Brazier _currentBrazier;

        public void DisableUI()
        {
            selectItemResourcePanel.DisableUI();
            selectItemFuelPanel.DisableUI();
            gameObject.SetActive(false);
        }

        private void Awake()
        {
            resourceButton.Init(null,true);
            fuelButton.Init(null,true);
        }

        public void EnableForUI(Action<ItemSO, ItemSO> callback, Brazier brazier)
        {
            action = callback;
            _currentBrazier = brazier;
            selectItemResourcePanel.EnableForUI(callback);
            resourceButton.EnableForUI(new ToolBarUIData(),() => RefreshUI(true));
            fuelButton.EnableForUI(new ToolBarUIData(),() => RefreshUI(false));
            gameObject.SetActive(true);
        }

        public void RefreshUI(bool isResource)
        {
            if(isResource)
            {
                selectItemFuelPanel.DisableUI();
                selectItemResourcePanel.EnableForUI(action);
            }
            else
            {
                selectItemResourcePanel.DisableUI();
                selectItemFuelPanel.EnableForUI(_currentBrazier);
            }
        }
    }
}