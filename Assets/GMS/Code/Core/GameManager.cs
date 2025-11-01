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
        [SerializeField] private GameObject cameraTargetObject;
        [SerializeField] private float moveSpeed = 1f;
        private bool _isCanClick;
        private bool _isOnRightClick;
        private float _doubleClickCheckTime;
        private const float DOUBLE_CLICK_DELAY_TIME = 0.2f;
        
        
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
            mouseInputSO.OnRightClickEvent += HandleRightClickEvent;
            mouseInputSO.OnRightClickTriggerEvent += HandleRightClickTriggerCheck;
        }

        private void HandleRightClickEvent(bool value)
        {
            _isOnRightClick = value;
        }

        private void HandleRightClickTriggerCheck()
        {
            if ((Time.time - _doubleClickCheckTime) < DOUBLE_CLICK_DELAY_TIME)
                cameraTargetObject.transform.position = Vector3.zero;

            _doubleClickCheckTime = Time.time;
        }

        public void OnDestroy()
        {
            mouseInputSO.OnClickAction -= HandleMouseClickEvent;
        }

        private void Update()
        {
            _isCanClick = !EventSystem.current.IsPointerOverGameObject();


            if (_isOnRightClick)
            {
                Vector3 pos = new Vector3(mouseInputSO.MouseDelta.x,0, mouseInputSO.MouseDelta.y);

                cameraTargetObject.transform.position += pos * Time.deltaTime * moveSpeed;
            }
        }

        private void HandleMouseClickEvent()
        {

            if (!_isCanClick)
            {
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