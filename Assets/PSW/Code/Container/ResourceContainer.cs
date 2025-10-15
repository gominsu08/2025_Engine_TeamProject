using GMS.Code.Items;
using System.Collections.Generic;
using UnityEngine;

namespace PSW.Code.Container
{
    public class ResourceContainer : MonoBehaviour
    {
        [SerializeField] private int maxCoin = 999999;
        private Dictionary<ItemSO, int> _resourceCountDic = new Dictionary<ItemSO, int>();
        private int _coin = 0;

        public void PlusItem(ItemSO keyItem, int plusCount)
        {
            if (_resourceCountDic.TryGetValue(keyItem, out int countValue))
            {
                countValue += plusCount;
                _resourceCountDic[keyItem] = countValue;
                return;
            }

            _resourceCountDic.Add(keyItem, plusCount);
        }
        public void MinusItem(ItemSO keyItem, int minusCount)
        {
            if (_resourceCountDic.TryGetValue(keyItem, out int countValue))
            {
                countValue -= minusCount;
                _resourceCountDic[keyItem] = countValue;
                return;
            }

            _resourceCountDic.Add(keyItem, minusCount);
        }
        public void PlusCoin(int plusCoin)
        {
            _coin = Mathf.Clamp(_coin + plusCoin, 0, maxCoin);
        }
        public void MinusCoin(int minusCoin)
        {
            _coin = Mathf.Clamp(_coin - minusCoin, 0, maxCoin);
        }
        public bool IsTargetCountItem(ItemSO keyItem, int targetCount)
        {
            if (_resourceCountDic.TryGetValue(keyItem, out int countValue) && countValue >= targetCount)
                return true;
            else
                return false;
        }
        public bool IsTargetCoin(int targetCoin)
        {
            if (_coin >= targetCoin)
                return true;
            else
                return false;
        }
        public int GetItemCount(ItemSO keyItem)
        {
            int count = _resourceCountDic.GetValueOrDefault(keyItem);
            return count;
        }
        public int GetCoin()
        {
            return _coin;
        }
    }
}