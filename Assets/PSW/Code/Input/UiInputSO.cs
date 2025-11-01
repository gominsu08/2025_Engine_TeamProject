using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PSW.Code.Input
{
    [CreateAssetMenu(fileName = "UiInput", menuName = "SO/Input/UI")]
    public class UiInputSO : ScriptableObject, UiInput.IUiActions
    {
        public event Action OnESCPressrd;

        private UiInput _controller;

        private void OnEnable()
        {
            if (_controller == null)
            {
                _controller = new UiInput();
                _controller.Ui.SetCallbacks(this);
            }
            _controller.Ui.Enable();
        }
        private void OnDisable()
        {
            _controller.Ui.Disable();
        }

        public void OnESC(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnESCPressrd?.Invoke();
        }
    }
}