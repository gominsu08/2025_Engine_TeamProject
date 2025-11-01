using GMS.Code.Core.System.Machines;
using GMS.Code.UI.MainPanel;
using System.IO;
using System.Xml;
using UnityEngine;

namespace GMS.Code.UI.Braziers
{

    public class BrazierPanelDescriptionUI : MonoBehaviour, IUIElement<Brazier>
    {
        [SerializeField] private BarUI barUI;
        [SerializeField] private TextUI nameText;
        [SerializeField] private TextUI descriptionText;
        [SerializeField] private MachineManager machineManager;

        private ProcessItemPair _currentProcessPair;
        private Brazier _currentBrazier;
        private bool _isEnabled = false;

        public void DisableUI()
        {
            barUI.DisableUI();
            _isEnabled = false;
            gameObject.SetActive(false);
        }

        public void EnableForUI(Brazier brazier)
        {
            _currentBrazier = brazier;
            _currentProcessPair = brazier.GetProcessItemPair();

            barUI.EnableForUI(_currentBrazier.GetMaxTime(), null);
            SetText();

            gameObject.SetActive(true);
            _isEnabled = true;
        }

        private void SetText()
        {
            if(_currentProcessPair == null || _currentProcessPair.makeItem == null)
            {
                nameText.EnableForUI($"재료선택");

                descriptionText.EnableForUI($"현재 가공중인 아이템이 존재하지 않습니다.");
            }
            else if(!_currentBrazier.IsCanMake)
            {
                nameText.EnableForUI($"{_currentProcessPair.resourceItem.itemName} -> {_currentProcessPair.makeItem.itemName}");

                descriptionText.EnableForUI($"재료가 부족합니다..");
            }
            else
            {
                nameText.EnableForUI($"{_currentProcessPair.resourceItem.itemName} -> {_currentProcessPair.makeItem.itemName}");

                descriptionText.EnableForUI($"{_currentProcessPair.resourceItem.itemName}을(를) {_currentProcessPair.makeItem.itemName}로 가공중입니다.");
            }
        }

        public void Update()
        {
            if (_isEnabled)
            {
                barUI.UpdateUI(_currentBrazier.MiningTime);
                SetText();
            }

            
        }
    }
}