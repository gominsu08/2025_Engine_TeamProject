using GMS.Code.Core.System.Machines;
using GMS.Code.UI;
using GMS.Code.UI.MainPanel;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Assets.GMS.Code.UI.MainPanel
{
    public class BuildingMachineButtonUI : MonoBehaviour, IUIElement<UnityAction<Tier, List<ItemAndValuePair>>, Tier>, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private ButtonUI button;
        [SerializeField] private TextUI text;
        [SerializeField] private IconUI icon;
        [SerializeField] private MachineSO machineSO;
        [SerializeField] private Color disableColor, defaultColor;
        private RectTransform MyRect => transform as RectTransform;
        private ToolBarUIData _toolBarUIData;
        private ToolBarUI _toolBarUI;
        private Tier _targetTier;
        private UnityAction<Tier, List<ItemAndValuePair>> _callback;


        public void DisableUI()
        {
            _callback = null;
            button.DisableUI();
            text.DisableUI();
            icon.DisableUI();
            gameObject.SetActive(false);
        }

        public void Init(ToolBarUI toolBarUI)
        {
            _toolBarUI = toolBarUI;
        }

        public void EnableForUI(UnityAction<Tier, List<ItemAndValuePair>> callback, Tier targetTier)
        {
            _targetTier = targetTier;
            _callback = callback;
            _toolBarUIData = new ToolBarUIData(machineSO.Description);
            button.Init(_toolBarUI);

            if ((int)targetTier > (int)machineSO.machineTier)
            {
                button.SetVisualDisable();
                icon.SetColor(disableColor);
            }
            else
            {
                button.EnableForUI(_toolBarUIData, HandleButtonClick);
                icon.SetColor(defaultColor);
                icon.EnableForUI(machineSO.machineIcon);
            }

            text.EnableForUI(machineSO.machineName);
            gameObject.SetActive(true);
        }

        private void HandleButtonClick()
        {
            _callback?.Invoke(machineSO.machineTier, machineSO.itemList);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Vector2 position = new Vector2(MyRect.position.x - MyRect.sizeDelta.x / 2, MyRect.position.y + MyRect.sizeDelta.y / 2);

            _toolBarUI.EnableForUI(position, _toolBarUIData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _toolBarUI.DisableUI();
        }
    }
}