using DG.Tweening;
using UnityEngine;

namespace GMS.Code.UI.MainPanel
{
    public class UnitCreatePopupUI : MonoBehaviour, IUIElement<int>
    {
        [SerializeField] private float endY = 345;
        [SerializeField] private TextUI moneyTextUI;
        [SerializeField] private IconUI iconUI;
        [SerializeField] private RectTransform panel;
        private RectTransform MyRect => transform as RectTransform;
        private float _startY = 0;
        public void DisableUI()
        {
            panel.DOSizeDelta(new Vector2(panel.sizeDelta.x, _startY), 0.2f);
            MyRect.DOSizeDelta(new Vector2(panel.sizeDelta.x, _startY), 0.2f).OnComplete(() => gameObject.SetActive(false));
        }

        public void EnableForUI(int amount)
        {
            gameObject.SetActive(true);
            panel.DOSizeDelta(new Vector2(panel.sizeDelta.x, endY), 0.5f);
            MyRect.DOSizeDelta(new Vector2(panel.sizeDelta.x, endY), 0.5f);

            moneyTextUI.EnableForUI($"{amount}원");

            iconUI.SetColor(Color.white);
            moneyTextUI.SetColor(Color.white);
        }

        public void RefrashUI(int amount)
        {
            moneyTextUI.EnableForUI($"{amount}원");
        }

        public void SetColor()
        {
            moneyTextUI.SetColor(Color.red);
            iconUI.SetColor(Color.red);

            DOVirtual.DelayedCall(0.3f, () =>
            {
                moneyTextUI.SetColor(Color.white);
                iconUI.SetColor(Color.white);
            });
        }
    }
}