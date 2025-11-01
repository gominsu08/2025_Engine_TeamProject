using csiimnida.CSILib.SoundManager.RunTime;
using GMS.Code.Items;
using PSW.Code.Container;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace PSW.Code.Sale
{
    public class SalePanel : MonoBehaviour
    {
        [SerializeField] private ResourceContainer resourceContainer;
        [SerializeField] private TextMeshProUGUI coinText;

        [SerializeField] private ItemListSO itemListSO;
        [SerializeField] private GameObject saleBoxPrefab;
        [SerializeField] private Transform boxTrm;
        public UnityEvent OnSaleEvent { private set; get; } = new();
        public UnityEvent OnResetEvent { private set; get; } = new();

        private int addCoin = 0;
        private string _soundName = "Sale";
        private void Start()
        {
            foreach (ItemSO item in itemListSO.itemSOList)
            {
                SaleBox tempSaleBox = Instantiate(saleBoxPrefab, boxTrm)
                    .GetComponent<SaleBox>();

                tempSaleBox.Init(item, this, resourceContainer);

            }
        }

        public void ResetAddCoin()
        {
            OnResetEvent?.Invoke();
            addCoin = 0;
            coinText.text = addCoin.ToString();
        }

        public void Sale()
        {
            OnSaleEvent?.Invoke();
            resourceContainer.PlusCoin(addCoin);
            addCoin = 0;
            coinText.text = addCoin.ToString();
        }

        public void SetAddCoin(int coin)
        {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySound(_soundName);

            addCoin += coin;
            coinText.text = addCoin.ToString();
        }
    }
}