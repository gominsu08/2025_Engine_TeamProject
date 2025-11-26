using GMS.Code.Core;
using GMS.Code.Items;
using PSW.Code.Container;
using PSW.Code.Sale;
using System.Collections.Generic;
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
    [SerializeField] private OneClickCoinButton startOneClickCoinButton;

    private ItemSO _thisItem;
    private SalePanel _panel;
    private ResourceContainer _resourceContainer;

    private List<OneClickCoinButton> oneClickCoinButtonList;

    private int _currentMaxCount;
    private long _currentCount;
    private int _addCointValue;

    public void Init(ItemSO item, SalePanel panel, ResourceContainer resourceContainer)
    {
        _thisItem = item;
        _panel = panel;
        panel.OnSaleEvent.AddListener(SaleItem);
        panel.OnResetEvent.AddListener(ResetItem);
        _resourceContainer = resourceContainer;

        GetComponentsInChildren<PlusMinusButton>().ToList().ForEach(v => v.Init(this));
        oneClickCoinButtonList = GetComponentsInChildren<OneClickCoinButton>().ToList();

        startOneClickCoinButton.SetButton(true);
        _addCointValue = startOneClickCoinButton.GetCoinValue();

        icon.sprite = item.icon;
        nameText.text = item.itemName;
        _currentMaxCount = resourceContainer.GetItemCount(item);
        countText.text = _currentMaxCount.ToString();

        Bus<ChangeItem>.OnEvent += SetCountText;
        inputField.onEndEdit.AddListener(SetText);
    }

    private void OnDestroy()
    {
        Bus<ChangeItem>.OnEvent -= SetCountText;
    }

    private void SetText(string finalText)
    {
        bool isNumberOnly = finalText.All(char.IsDigit) && finalText.ToArray().Length > 0;

        if (isNumberOnly)
        {
            long currentText = long.Parse(finalText);

            if (currentText > _currentMaxCount)
                currentText = _currentMaxCount;
            else if (currentText < 0)
                currentText = 0;

            if(currentText > _currentCount)
                _panel.SetAddCoin((int)(currentText - _currentCount) * _thisItem.sellMoney);
            else
                _panel.SetAddCoin(-((int)(_currentCount - currentText) * _thisItem.sellMoney));

            _currentCount = currentText;
            inputField.text = currentText.ToString();
        }
        else
        {
            _panel.SetAddCoin(-(int)_currentCount * _thisItem.sellMoney);
            ResetItem();
        }
    }
    private void SaleItem()
    {
        _resourceContainer.MinusItem(_thisItem,(int)_currentCount);
        ResetItem();
    }
    private void ResetItem()
    {
        _currentCount = 0;
        NullText();
    }
    private void NullText()
    {
        inputField.text = "";
    }
    private void SetCountText(ChangeItem evt)
    {
        if(_thisItem == evt.KeyItem)
        {
            countText.text = evt.Count.ToString();
            _currentMaxCount = evt.Count;
        }
    }

    public void SetCount(bool isPius)
    {
        long tempCount = _currentCount;
        tempCount += (isPius ? _addCointValue : -_addCointValue);
        SetText(tempCount.ToString());
    }

    public void ButtonSetUp(OneClickCoinButton coinButton)
    {
        foreach (OneClickCoinButton oneClick in oneClickCoinButtonList)
            oneClick.SetButton(false);

        coinButton.SetButton(true);
        _addCointValue = coinButton.GetCoinValue();
    }
}
