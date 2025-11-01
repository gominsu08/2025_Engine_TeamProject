using GMS.Code.Units;
using PSW.Code.Container;
using UnityEngine;

namespace GMS.Code.UI.MainPanel
{
    public class HeadQuarterPanel : MonoBehaviour, IUIElement<ResourceContainer, UnitManager>
    {
        [SerializeField] private UnitCreateButton createButton;
        [SerializeField] private TextUI text;
        private UnitManager _unitManager;
        private ResourceContainer _container;
        private bool _isEnable;

        public void DisableUI()
        {
            createButton.DisableUI();
            text.DisableUI();
            _isEnable = false;
            gameObject.SetActive(false);
        }

        public void EnableForUI(ResourceContainer resourceManager, UnitManager unitManager)
        {
            _isEnable = true;
            _unitManager = unitManager;
            _container = resourceManager;
            text.EnableForUI($"현재 유닛갯수 : {_unitManager.UnitCount}");
            createButton.EnableForUI(resourceManager, unitManager);
            gameObject.SetActive(true);
        }

        private void Update()
        {
            if (_isEnable)
                text.EnableForUI($"현재 유닛갯수 : {_unitManager.UnitCount}");
        }
    }
}