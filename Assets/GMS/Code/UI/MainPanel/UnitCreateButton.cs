using csiimnida.CSILib.SoundManager.RunTime;
using GMS.Code.Units;
using PSW.Code.Container;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GMS.Code.UI.MainPanel
{
    public class UnitCreateButton : MonoBehaviour, IUIElement<ResourceContainer, UnitManager>, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private ButtonUI button;
        [SerializeField] private UnitCreatePopupUI popupUI;
        private ResourceContainer _container;
        private UnitManager _unitManager;

        private int unitBuyAmount => _unitManager.UnitCount * (50 + ((_unitManager.UnitCount / 5) * 120));

        private void Awake()
        {
            button.Init(null,true);
        }

        public void DisableUI()
        {
            button.DisableUI();
            popupUI.DisableUI();
            gameObject.SetActive(false);
        }

        public void EnableForUI(ResourceContainer resourceManager, UnitManager UnitManager)
        {
            _unitManager = UnitManager;
            _container = resourceManager;
            button.EnableForUI(new ToolBarUIData(), HandleButtonClickEvent);
            popupUI.DisableUI();
            gameObject.SetActive(true);
        }

        private void Update()
        {
            popupUI.RefrashUI(unitBuyAmount);
        }

        private void HandleButtonClickEvent()
        {
            if(_container.IsTargetCoin(unitBuyAmount))
            {
                _container.MinusCoin(unitBuyAmount);
                _unitManager.UnitCreate();
            }
            else
            {
                popupUI.SetColor();
                SoundManager.Instance.PlaySound("Fail");
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            popupUI.EnableForUI(unitBuyAmount);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            popupUI.DisableUI();
        }
    }
}