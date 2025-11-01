using GMS.Code.UI.MainPanel;
using UnityEngine;

namespace GMS.Code.UI.TileInfoUIPanel
{
    public class ResourceTierUI : MonoBehaviour, IUIElement<Tier>
    {
        [SerializeField] private TextUI text;

        public void DisableUI()
        {
            gameObject.SetActive(false);
        }

        public void EnableForUI(Tier t)
        {
            gameObject.SetActive(true);
            SetText(t);
        }

        public void SetText(Tier t)
        {
            string s = "";

            switch (t)
            {
                case Tier.None:
                    s = "티어가 존재하지 않습니다.";
                    break;
                case Tier.FirstTier:
                    s = "티어 : 1";
                    break;
                case Tier.SecondTier:
                    s = "티어 : 2";
                    break;
                case Tier.ThirdTier:
                    s = "티어 : 3";
                    break;
            }

            text.EnableForUI(s);
        }
    }
}