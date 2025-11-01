using GMS.Code.Core.System.Machines;
using GMS.Code.Items;
using GMS.Code.UI.MainPanel;
using System;
using System.Threading;
using UnityEngine;

namespace GMS.Code.UI.Braziers
{
    public class BrazierPanel : MonoBehaviour, IUIElement<Brazier>
    {
        [SerializeField] private SelectItemPanel selectItemPanel;
        [SerializeField] private BrazierPanelDescriptionUI brazierPanelDescriptionUI;
        [SerializeField] private TextUI fuelText;

        private Brazier _currentBrazier;


        public void DisableUI()
        {
            selectItemPanel.DisableUI();
            brazierPanelDescriptionUI.DisableUI();
            gameObject.SetActive(false);
        }

        public void EnableForUI(Brazier brazier)
        {
            _currentBrazier = brazier;
            RefreshUI();
            gameObject.SetActive(true);
        }

        private void RefreshUI()
        {
            selectItemPanel.EnableForUI(HandleMakeItemChange,_currentBrazier);
            brazierPanelDescriptionUI.EnableForUI(_currentBrazier);
            fuelText.EnableForUI($"연료 : <color=orange>{_currentBrazier.GetCurrentFuel()}</color>");
        }

        private void HandleMakeItemChange(ItemSO makeTarget, ItemSO resourceItem)
        {
            _currentBrazier.SetCurrentTargetItem(makeTarget,resourceItem);
            RefreshUI();
        }

        public void Update()
        {
            fuelText.EnableForUI($"연료 : <color=orange>{_currentBrazier.GetCurrentFuel()}</color>");
        }
    }
}