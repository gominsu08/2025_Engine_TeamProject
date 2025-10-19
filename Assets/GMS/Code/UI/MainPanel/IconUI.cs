using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace GMS.Code.UI.MainPanel
{
    public class IconUI : MonoBehaviour, IUIElement<Sprite>
    {
        [SerializeField] private Image iconImage;

        public void DisableUI()
        {
            iconImage.sprite = null;
            gameObject.SetActive(false);
        }

        public void EnableForUI(Sprite image)
        {
            iconImage.sprite = image;
            gameObject.SetActive(true);
        }

        public void SetColor(Color color)
        {
            iconImage.color = color;
        }
    }
}
