using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GMS.Code.UI.MainPanel
{
    public class ButtonUI : MonoBehaviour, IUIElement< ToolBarUIData, Action>, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected Button button;
        private RectTransform MyRect => transform as RectTransform;
        private ToolBarUI _toolBarUI;
        private ToolBarUIData _toolBarUIData;

        public virtual void DisableUI()
        {
            button.enabled = false;
            button.onClick.RemoveAllListeners();
            if(_toolBarUI != null)
            _toolBarUI.DisableUI();
            gameObject.SetActive(false);
        }

        public void Init(ToolBarUI toolBarUI)
        {
            _toolBarUI = toolBarUI;
        }

        public virtual void EnableForUI( ToolBarUIData toolBarUIData, Action callback)
        {
            button.enabled = true;
            button.onClick.AddListener(() => callback?.Invoke());
            _toolBarUIData = toolBarUIData;
            gameObject.SetActive(true);
        }

        public void SetVisualDisable()
        {
            button.enabled = false;
            button.onClick.RemoveAllListeners();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (button.enabled == false) return;
            Vector2 position = new Vector2(MyRect.position.x - MyRect.sizeDelta.x/2, MyRect.position.y + MyRect.sizeDelta.y/2);

            _toolBarUI.EnableForUI(position, _toolBarUIData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _toolBarUI.DisableUI();
        }
    }
}
