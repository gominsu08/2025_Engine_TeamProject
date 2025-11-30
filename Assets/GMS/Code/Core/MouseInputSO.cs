using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace GMS.Code.Core
{
    [CreateAssetMenu(fileName = "MouseInputSO", menuName = "SO/GMS/System/Input")]
    public class MouseInputSO : ScriptableObject, Controls.IPlayerActions
    {
        public Action OnClickAction;
        public Action OnTabKeyDownEvent;
        public Action<bool> OnRightClickEvent;
        public Action OnRightClickTriggerEvent;
        public Action OnZoomEvent;
        public Action<Vector2> OnWASDEvent;
        [SerializeField] private LayerMask whatIsTargetLayer;

        public Vector2 MousePosition { get; private set; }
        public Vector2 MouseDelta { get; private set; }
        public Vector2 ZoomValue { get; private set; }
        public Vector2 keyValue { get; private set; }

        private Controls controls;

        private void OnEnable()
        {
            if (controls == null)
            {
                controls = new Controls();
                controls.Player.SetCallbacks(this);
            }
            controls.Player.Enable();
        }

        private void OnDisable()
        {
            controls.Player.Disable();
        }

        public void OnMousePosition(InputAction.CallbackContext context)
        {
            MousePosition = context.ReadValue<Vector2>();
        }

        public void OnClick(InputAction.CallbackContext context)
        {
           
            if (context.performed)
                OnClickAction?.Invoke();
        }

        public Vector3 GetWorldPosition()
        {
            Vector3 worldPosition = new Vector3();
            Camera mainCam = Camera.main; //유니티 2022부터는 내부 캐싱하기 때문에 이렇게 써도 돼.
            Debug.Assert(mainCam != null, "No main camera in this scene.");
            Ray cameraRay = mainCam.ScreenPointToRay(MousePosition);
            if (Physics.Raycast(cameraRay, out RaycastHit hit, mainCam.farClipPlane, whatIsTargetLayer))
            {
                worldPosition = hit.point;
            }
            return worldPosition;
        }

        public RaycastHit GetHitObject()
        {
            Camera mainCam = Camera.main; //유니티 2022부터는 내부 캐싱하기 때문에 이렇게 써도 돼.
            Debug.Assert(mainCam != null, "No main camera in this scene.");
            Ray cameraRay = mainCam.ScreenPointToRay(MousePosition);
            if (Physics.Raycast(cameraRay, out RaycastHit hit, mainCam.farClipPlane, whatIsTargetLayer))
            {
                return hit;
            }
            return default;
        }

        public void OnTab(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnTabKeyDownEvent?.Invoke();
        }

        public void OnRightClick(InputAction.CallbackContext context)
        {
            if (context.started)
                OnRightClickEvent?.Invoke(true);
            else if (context.canceled)
                OnRightClickEvent?.Invoke(false);


            if (context.performed)
            {
                OnRightClickTriggerEvent?.Invoke();
            }
        }

        public void OnDelta(InputAction.CallbackContext context)
        {
            MouseDelta = context.ReadValue<Vector2>();
        }

        public void OnZoom(InputAction.CallbackContext context)
        {
            ZoomValue = context.ReadValue<Vector2>();
            OnZoomEvent?.Invoke();
        }

        public void OnWASD(InputAction.CallbackContext context)
        {
            keyValue = context.ReadValue<Vector2>();
        }
    }
}