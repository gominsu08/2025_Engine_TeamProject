using GMS.Code.Core;
using GMS.Code.Core.System.Machines;
using GMS.Code.Items;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PSW.Code.Container
{
    public class ResourceContainer : MonoBehaviour
    {
        [SerializeField] private int maxCoin = 999999;

        private ChangeCoinEvent _changeCoin;
        private ChangeItem _changeItem;

        private Dictionary<ItemSO, int> _resourceCountDic = new Dictionary<ItemSO, int>();
        private int _coin = 0;

        private void Awake()
        {
            Bus<ItemMinusEvent>.OnEvent += HandleMinusItem;
        }

        private void OnDestroy()
        {
            Bus<ItemMinusEvent>.OnEvent -= HandleMinusItem;
            
        }

        private void Start()
        {
            PlusCoin(5000);
        }

        private void HandleMinusItem(ItemMinusEvent evt)
        {
            MinusItem(evt.item, evt.count);
        }

        private void SetItem(ItemSO keyItem, int count)
        {
            if (_resourceCountDic.TryGetValue(keyItem, out int countValue))
            {
                countValue += count;
                _resourceCountDic[keyItem] = countValue;

                _changeItem.KeyItem = keyItem;
                _changeItem.Count = countValue;

                if (countValue <= 0)
                    Bus<ItemNotHaveEvent>.Raise(new ItemNotHaveEvent(keyItem));

                Bus<ChangeItem>.Raise(_changeItem);

                return;
            }

            _resourceCountDic.Add(keyItem, count);
            _changeItem.KeyItem = keyItem;
            _changeItem.Count = count;
            Bus<ChangeItem>.Raise(_changeItem);
        }
        public void PlusItem(ItemSO keyItem, int plusCount)
        {
            SetItem(keyItem, plusCount);
        }
        public void MinusItem(ItemSO keyItem, int minusCount)
        {
            SetItem(keyItem, -minusCount);
        }
        private void SetCoin(int coin)
        {
            _coin = Mathf.Clamp(_coin + coin, 0, maxCoin);
            _changeCoin.coin = _coin;
            Bus<ChangeCoinEvent>.Raise(_changeCoin);
        }
        public void PlusCoin(int plusCoin)
        {
            SetCoin(plusCoin);
            
        }
        public void MinusCoin(int minusCoin)
        {
            SetCoin(-minusCoin);
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

    public struct ChangeCoinEvent : IEvent { public int coin; }
    public struct ChangeItem : IEvent 
    {
        public ItemSO KeyItem;
        public int Count;
    }

}