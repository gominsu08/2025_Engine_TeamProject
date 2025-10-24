using DG.Tweening;
using GMS.Code.Core.Events;
using GMS.Code.Core.System.Machines;
using GMS.Code.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GMS.Code.UI.MainPanel
{
    public class IconAndTextUI : MonoBehaviour, IUIElement<ItemSO, int>
    {
        [SerializeField] private TextUI text;
        [SerializeField] private IconUI icon;

        public void DisableUI()
        {
            text.DisableUI();
            icon.DisableUI();
            gameObject.SetActive(false);
        }

        

        public void EnableForUI(ItemSO itemSO, int count)
        {
            text.SetColor(Color.white);
            text.EnableForUI($"{itemSO.itemName} : {count}개");
            icon.EnableForUI(itemSO.icon);
            gameObject.SetActive(true);
        }

        public void ChangeColor()
        {
            text.SetColor(Color.red);
            icon.SetColor(Color.red);

            DOVirtual.DelayedCall(0.3f,()=>
            {
                text.SetColor(Color.white);
                icon.SetColor(Color.white);
            });
        }

        public void SetColor(Color color)
        {
            text.SetColor(color);
            icon.SetColor(color);
        }
    }
}
