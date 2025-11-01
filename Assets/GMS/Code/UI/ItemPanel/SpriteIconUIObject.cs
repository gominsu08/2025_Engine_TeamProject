using UnityEngine;

namespace GMS.Code.UI.ItemPanel
{
    public class SpriteIconUIObject : MonoBehaviour, IUIElement<Sprite>
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        public void DisableUI()
        {
            gameObject.SetActive(false);
        }

        public void EnableForUI(Sprite t)
        {
            spriteRenderer.sprite = t;
            gameObject.SetActive(true);
        }
    }
}