using GMS.Code.Core;
using GMS.Code.Items;
using PSW.Code.Container;
using PSW.Code.Make;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PSW.Code.Resource
{
    public class ItemData : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countText;
        [SerializeField] private Image icon;

        [Header("Panel")]
        [SerializeField] private DataPanel namePanel;
        [SerializeField] private DataPanel moneyPanel;

        private ItemSO _item;

        public void Init(ItemSO thisItem, int count)
        {
            _item = thisItem;
            icon.sprite = thisItem.icon;
            countText.text = count.ToString();
            Bus<ChangeItem>.OnEvent += SetText;

            namePanel.Init(thisItem.itemName);
            moneyPanel.Init(thisItem.sellMoney.ToString());
        }

        private void SetText(ChangeItem evt)
        {
            if(evt.KeyItem == _item)
                countText.text = evt.Count.ToString();
        }
    }
}