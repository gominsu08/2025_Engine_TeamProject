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
    public class ButtonUI : MonoBehaviour, IUIElement<ToolBarUI, ToolBarUIData, Action>, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected Button button;
        private RectTransform myRect => transform as RectTransform;
        private ToolBarUI _toolBarUI;
        private ToolBarUIData _toolBarUIData;

        public virtual void DisableUI()
        {
            button.enabled = false;
            button.onClick.RemoveAllListeners();
            _toolBarUI.DisableUI();
            gameObject.SetActive(false);
        }

        public virtual void EnableForUI(ToolBarUI toolBarUI, ToolBarUIData toolBarUIData, Action callback)
        {
            button.enabled = true;
            button.onClick.AddListener(() => callback?.Invoke());
            _toolBarUI = toolBarUI;
            _toolBarUIData = toolBarUIData;
            gameObject.SetActive(true);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Vector2 position = new Vector2(myRect.position.x - myRect.sizeDelta.x, myRect.position.y + myRect.sizeDelta.y);

            _toolBarUI.EnableForUI(position, _toolBarUIData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _toolBarUI.DisableUI();
        }
    }
}
