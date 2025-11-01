using GMS.Code.Core;
using PSW.Code.Container;
using TMPro;
using UnityEngine;

namespace PSW.Code.Resource
{
    public class CoinPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI coinText;

        private void Start()
        {
            Bus<ChangeCoinEvent>.OnEvent += SetCoinText;
        }

        private void OnDestroy()
        {
            Bus<ChangeCoinEvent>.OnEvent -= SetCoinText;
        }

        private void SetCoinText(ChangeCoinEvent evt)
        {
            coinText.text = evt.coin.ToString();
        }
    }
}