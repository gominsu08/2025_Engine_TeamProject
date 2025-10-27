using DG.Tweening;
using GMS.Code.Items;
using UnityEngine;

namespace GMS.Code.UI.ItemPanel
{
    public class ItemInformationUI : MonoBehaviour
    {
        [SerializeField] private SpriteIconUIObject spriteObjectl;
        [SerializeField] private TextUIObject textUI;

        public void Init(ItemSO item, string value, float duration, bool isNotDestroy = false)
        {
            spriteObjectl.EnableForUI(item.icon);
            textUI.EnableForUI(value);

            transform.DOMoveY(transform.position.y + 0.2f, duration).OnComplete(() =>
            {
                if (!isNotDestroy)
                    Destroy(gameObject);
            });
        }

        public void DisableUI()
        {
            Destroy(gameObject);
        }

        private void Update()
        {
            //transform.rotation = Quaternion.LookRotation(Camera.main.transform.position);
        }

    }
}