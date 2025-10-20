using GMS.Code.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDataPanel : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI tierText;
    [SerializeField] private TextMeshProUGUI moneyText;

    private void Start()
    {
        Bus<ItemButtonEvt>.OnEvent += SetItemData;
    }

    private void SetItemData(ItemButtonEvt evt)
    {
        icon.sprite = evt.ItemData.icon;
        nameText.text = "이름 : " + evt.ItemData.itemName;
        tierText.text = $"자원 티어 : {(int)(evt.ItemData.tier)}";
        moneyText.text = "가격 : " + evt.ItemData.sellMoney;
    }
}
