using GMS.Code.Core.System.Maps;
using UnityEngine;

namespace GMS.Code.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private MouseInputSO mouseInputSO;

        public void Awake()
        {
            mouseInputSO.OnClickAction += HandleMouseClickEvent;
        }

        public void OnDestroy()
        {
            mouseInputSO.OnClickAction -= HandleMouseClickEvent;
        }

        private void HandleMouseClickEvent()
        {

            RaycastHit hit = mouseInputSO.GetHitObject();
            if (hit.collider == null || hit.collider.gameObject == null) return;

            if (hit.collider.gameObject.TryGetComponent<IClickable>(out IClickable clickable))
            {
                clickable.OnClick();
            }
        }
    }
}