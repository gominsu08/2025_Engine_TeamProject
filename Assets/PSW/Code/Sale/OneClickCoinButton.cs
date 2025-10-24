using DG.Tweening;
using TMPro;
using UnityEngine;

public class OneClickCoinButton : MonoBehaviour
{
    [SerializeField] private int coinValue;
    [SerializeField] private TextMeshProUGUI countText;

    [Header("Set")]
    [SerializeField] private Vector3 setUpSize;
    [SerializeField] private Color setUpColor;

    [SerializeField] private Vector3 setDownSize;
    [SerializeField] private Color setDownColor;

    [SerializeField] private float setTime = 0.4f;
    public int GetCoinValue() => coinValue;

    public void SetButton(bool isSetUp)
    {
        countText.DOColor(isSetUp ? setUpColor : setDownColor, setTime);
        transform.DOScale(isSetUp ? setUpSize : setDownSize, setTime);
    }
}
