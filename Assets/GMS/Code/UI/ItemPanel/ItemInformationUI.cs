using DG.Tweening;
using GMS.Code.Items;
using GMS.Code.UI.MainPanel;
using UnityEngine;

namespace GMS.Code.UI.ItemPanel
{
    public class ItemInformationUI : MonoBehaviour
    {
        [SerializeField] private SpriteIconUIObject spriteObjectl;
        [SerializeField] private TextUIObject textUI;

        public void Init(ItemSO item, string value, float duration)
        {
            spriteObjectl.EnableForUI(item.icon);
            textUI.EnableForUI(value);

            transform.DOMoveY(transform.position.y + 0.2f , duration).OnComplete(() => Destroy(gameObject));
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