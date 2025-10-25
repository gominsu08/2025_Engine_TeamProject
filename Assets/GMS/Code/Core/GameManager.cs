using GMS.Code.Core.Events;
using GMS.Code.Core.System.Maps;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace GMS.Code.Core
{
    public class GameManager : MonoBehaviour
    {
        public UnityEvent OnTabEvent;
        [SerializeField] private MouseInputSO mouseInputSO;
        private bool isCanClick;
        
        
        
        public static GameManager Instance
        {
            get; private set;
        }

        public bool IsBuildingMode { get; private set; }

        //[SerializeField] private InputSystemUIInputModule uiInputModule;

        public void Awake()
        {
            if (Instance != null)
                Destroy(gameObject);
            Instance = this;
            mouseInputSO.OnClickAction += HandleMouseClickEvent;
            mouseInputSO.OnTabKeyDownEvent += HandleTabKeyDownEvent;
        }

       

        public void OnDestroy()
        {
            mouseInputSO.OnClickAction -= HandleMouseClickEvent;
        }

        private void Update()
        {
            isCanClick = !EventSystem.current.IsPointerOverGameObject();

            if (isCanClick == false && Keyboard.current.shiftKey.isPressed)
            {
                PointerEventData pointerData = new PointerEventData(EventSystem.current);
                pointerData.position = Mouse.current.position.ReadValue();
                List<RaycastResult> raycastResults = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerData, raycastResults);

                foreach (var data in raycastResults)
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

        public void HandleTabKeyDownEvent()
        {
            OnTabEvent?.Invoke();
        }

        public void ChangeSelectMode()
        {
            IsBuildingMode = !IsBuildingMode;
            Bus<ChangeSelectModeEvent>.Raise(new ChangeSelectModeEvent());
        }
    }
}