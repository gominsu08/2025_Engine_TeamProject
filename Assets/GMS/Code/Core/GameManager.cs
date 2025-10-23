using GMS.Code.Core.System.Maps;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace GMS.Code.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private MouseInputSO mouseInputSO;
        //[SerializeField] private InputSystemUIInputModule uiInputModule;
        private bool isCanClick;

        public void Awake()
        {
            mouseInputSO.OnClickAction += HandleMouseClickEvent;
        }

        public void OnDestroy()
        {
            mouseInputSO.OnClickAction -= HandleMouseClickEvent;
        }

        private void Update()
        {
            isCanClick = !EventSystem.current.IsPointerOverGameObject();

            if(isCanClick ==false && Keyboard.current.shiftKey.isPressed)
            {
                PointerEventData pointerData = new PointerEventData(EventSystem.current);
                pointerData.position = Mouse.current.position.ReadValue();
                List<RaycastResult> raycastResults = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerData, raycastResults);

                foreach(var data in raycastResults)
                {
                    Debug.Log(data.gameObject.name);
                }
            }
        }

        private void HandleMouseClickEvent()
        {
            
            if (!isCanClick)
            {
                Debug.Log(!isCanClick);
                return;
            }

            RaycastHit hit = mouseInputSO.GetHitObject();
            if (hit.collider == null || hit.collider.gameObject == null) return;

            if (hit.collider.gameObject.TryGetComponent<IClickable>(out IClickable clickable))
            {
                clickable.OnClick();
            }
        }
    }
}