using GMS.Code.Core;
using PSW.Code.Payment;
using PSW.Code.TimeSystem;
using TMPro;
using UnityEngine;

public class DayPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayText;
    private void Start()
    {
        Bus<OneDayTimeEvent>.OnEvent += NewDay;
    }

    private void NewDay(OneDayTimeEvent evt)
    {
        dayText.text = $"{evt.Day} Day";
    }
}
