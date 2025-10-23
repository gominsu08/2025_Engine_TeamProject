using DG.Tweening;
using GMS.Code.Core;
using GMS.Code.Core.Events;
using GMS.Code.Core.System.Machines;
using System.Collections.Generic;
using UnityEngine;

namespace GMS.Code.UI.MainPanel
{
    public class ResourcePopup : MonoBehaviour, IUIElement
    {
        [SerializeField] private RectTransform panel;
        [SerializeField] private float endY = 345;
        [SerializeField] private TextUI moneyTextUI;
        [SerializeField] private IconUI iconUI;
        [SerializeField] private List<IconAndTextUI> childPanels = new List<IconAndTextUI>();
        private MachineSO _machineSO;
        private RectTransform MyRect => transform as RectTransform;
        private float _startY = 0;

        public void DisableUI()
        {
            if (!_machineSO.isMoney)
            {
                foreach (var child in childPanels)
                    child.DisableUI();
            }
            panel.DOKill();
            MyRect.DOKill();
            panel.DOSizeDelta(new Vector2(panel.sizeDelta.x, _startY), 0.2f);
            MyRect.DOSizeDelta(new Vector2(panel.sizeDelta.x, _startY), 0.2f).OnComplete(() => gameObject.SetActive(false));

            Bus<MachineBuildingFailEvent>.OnEvent -= HandleMachineBuildingFailEvent;
        }

        public void EnableForUI()
        {
            Bus<MachineBuildingFailEvent>.OnEvent += HandleMachineBuildingFailEvent;

            panel.DOKill();
            MyRect.DOKill();
            gameObject.SetActive(true);

            if (!_machineSO.isMoney)
            {
                for (int i = 0; i < _machineSO.itemList.Count; i++)
                {
                    childPanels[i].EnableForUI(_machineSO.itemList[i].itemSO, _machineSO.itemList[i].value);
                }
            }

            panel.DOSizeDelta(new Vector2(panel.sizeDelta.x, endY), 0.5f);
            MyRect.DOSizeDelta(new Vector2(panel.sizeDelta.x, endY), 0.5f);

            if (_machineSO.isMoney)
            {
                moneyTextUI.SetColor(Color.white);
            }
        }

        private void HandleMachineBuildingFailEvent(MachineBuildingFailEvent evt)
        {
            List<bool> list = evt.trues;

            if (_machineSO.isMoney)
            {
                moneyTextUI.SetColor(Color.red);
                iconUI.SetColor(Color.red);

                DOVirtual.DelayedCall(0.3f, () =>
                {
                    moneyTextUI.SetColor(Color.white);
                    iconUI.SetColor(Color.white);
                });
            }

            for (int i = 0; i < _machineSO.itemList.Count; i++)
            {
                if (list[i] == false)
                    childPanels[i].ChangeColor();
            }
        }

        internal void Init(MachineSO machineSO)
        {
            _machineSO = machineSO;
        }
    }
}