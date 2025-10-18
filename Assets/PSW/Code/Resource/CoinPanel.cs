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
            Bus<ChangeCoin>.OnEvent += SetCoinText;
        }

        private void SetCoinText(ChangeCoin evt)
        {
            coinText.text = evt.coin.ToString();
        }
    }
}