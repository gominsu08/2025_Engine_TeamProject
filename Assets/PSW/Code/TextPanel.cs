using DG.Tweening;
using GMS.Code.Core;
using PSW.Code.TimeSystem;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class TextPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI panelText;

    private void Awake()
    {
        Bus<PaymentTimeEvent>.OnEvent += Text;
    }

    public async void Text(PaymentTimeEvent time)
    {
        transform.DOScale(Vector3.one, 0.5f);
        panelText.text = "납품시간입니다.";
        await Awaitable.WaitForSecondsAsync(0.5f + 0.8f);
        transform.DOScale(Vector3.zero, 0.5f);
    }
}
