using DG.Tweening;
using TMPro;
using UnityEngine;

public class DataPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dataText;
    [SerializeField] private float popTime;
    [SerializeField] private float popUpXValue;

    private RectTransform _rectTransform;

    private Vector3 targetSize;

    public void Init(string text)
    {
        _rectTransform = GetComponent<RectTransform>();
        targetSize = _rectTransform.sizeDelta;
        dataText.text = text;
    }

    public void PopUpDownPanel(bool isPopUp)
    {
        targetSize.y = isPopUp ? popUpXValue : 0;
        _rectTransform.DOSizeDelta(targetSize, popTime);
    }
}
