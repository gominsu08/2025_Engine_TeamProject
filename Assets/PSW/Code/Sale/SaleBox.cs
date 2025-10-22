using GMS.Code.Core;
using GMS.Code.Items;
using PSW.Code.Container;
using PSW.Code.Sale;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaleBox : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private TMP_InputField inputField;

    private ItemSO _thisItem;
    private SalePanel _panel;
    private ResourceContainer _resourceContainer;

    private int _currentMaxCount;
    private long _currentCount;
    private bool _isSetText;

    public void Init(ItemSO item, SalePanel panel, ResourceContainer resourceContainer)
    {
        _thisItem = item;
        _panel = panel;
        panel.OnSaleEvent.AddListener(NullText);
        _resourceContainer = resourceContainer;

        icon.sprite = item.icon;
        nameText.text = item.itemName;
        _currentMaxCount = resourceContainer.GetItemCount(item);
        countText.text = _currentMaxCount.ToString();

        Bus<ChangeItem>.OnEvent += SetCountText;
        inputField.onEndEdit.AddListener(SetText);
    }

    private void SetText(string finalText)
    {
        bool isNumberOnly = finalText.All(char.IsDigit) && finalText.ToArray().Length > 0;
        _isSetText = true;

        if (isNumberOnly)
        {
            long currentText = long.Parse(finalText);

            if (currentText > _currentMaxCount)
                currentText = _currentMaxCount;
            else if (currentText < 0)
                currentText = 0;

            if(currentText > _currentCount)
            {
                print((currentText - _currentCount));
                _panel.SetAddCoin((int)(currentText - _currentCount) * _thisItem.sellMoney);
            }
            else
            {
                _panel.SetAddCoin(-(int)(currentText - _currentCount) * _thisItem.sellMoney);

            }


                _currentCount = currentText;
            inputField.text = currentText.ToString();
        }
        else
        {
            _panel.SetAddCoin(-(int)_currentCount * _thisItem.sellMoney);
            NullText();
        }
    }

    private void NullText()
    {
        inputField.text = "";
        _isSetText = false;
    }

    private void SetCountText(ChangeItem evt)
    {
        if(_thisItem == evt.KeyItem)
        {
            countText.text = evt.Count.ToString();
            _currentMaxCount = evt.Count;
        }
    }
}
