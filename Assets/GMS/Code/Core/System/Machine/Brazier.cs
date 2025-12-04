using GMS.Code.Core.System.Maps;
using GMS.Code.Items;
using GMS.Code.UI.Braziers;
using GMS.Code.UI.ItemPanel;
using PSW.Code.Container;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GMS.Code.Core.System.Machines
{
    public struct ItemMinusEvent : IEvent
    {
        public ItemSO item;
        public int count;

        public ItemMinusEvent(ItemSO item, int count)
        {
            this.item = item;
            this.count = count;
        }
    }

    public struct ItemNotHaveEvent : IEvent
    {
        public ItemSO item;

        public ItemNotHaveEvent(ItemSO item)
        {
            this.item = item;
        }
    }

    public class Brazier : Machine
    {
        private const float ONE_FUEL_TIME = 60f;
        private float _targetItemProcessTime;
        private float _brazierTimer;
        private int _fuel;
        private ItemSO _target;
        private ItemSO _resourceItem;
        private List<ItemAndValuePair> _items = new List<ItemAndValuePair>();

        public bool IsCanMake { get; private set; } = true;

        public int GetCurrentFuel() => _fuel;

        public void AddFuel(ItemSO item)
        {
            _fuel += item.fuelAmount;
        }

        public override void MachineInit(TileInformation tileInfo, ResourceContainer container)
        {
            base.MachineInit(tileInfo, _container);
            Bus<ItemNotHaveEvent>.OnEvent += HandleNotHaveEvent;
        }

        private void OnDestroy()
        {
            if (_warrningMassage != null)
                _warrningMassage.DisableUI();
            Bus<ItemNotHaveEvent>.OnEvent -= HandleNotHaveEvent;
        }

        public float GetMaxTime() => _targetItemProcessTime;

        public void SetCurrentTargetItem(ItemSO target , ItemSO resourceItem)
        {
            this._target = target;
            _resourceItem = resourceItem;
            if(target != null)
            {
                _targetItemProcessTime = target.tier switch
                {
                    UI.MainPanel.Tier.FirstTier => 10,
                    UI.MainPanel.Tier.SecondTier => 10,
                    UI.MainPanel.Tier.ThirdTier => 15,
                };
            }
            IsCanMake = true;
            _timer = 0;
        }

        public ProcessItemPair GetProcessItemPair() => new ProcessItemPair(_resourceItem, _target);

        private void HandleNotHaveEvent(ItemNotHaveEvent evt)
        {
            if (_resourceItem != null && _resourceItem.itemName == evt.item.itemName)
            {
                IsCanMake = false;
                _timer = 0;
            }
        }

        public override void MachineUpdate()
        {
            _brazierTimer += Time.deltaTime;

            if (_brazierTimer >= ONE_FUEL_TIME)
            {
                _brazierTimer = 0;
                _fuel = Mathf.Clamp(_fuel - 1, 0, int.MaxValue);
            }


            if (!IsCanMake || _curCarryingValue >= _maxCarryingValue)
            {
                if (_isFull)
                {
                    _timer += Time.deltaTime;
                    if (_timer >= _targetItemProcessTime)
                    {
                        _isFull = false;
                        ItemInformationUI ui = CreateInfoPanelUI(_target,"<size=1>가득참</size>", 1000);

                        if (_warrningMassage != null)
                            _warrningMassage.DisableUI();

                        _warrningMassage = ui;
                        _timer = 0;
                    }
                }

                return;
            }

            
            if (_fuel > 0 && _target != null)
            {
                _timer += Time.deltaTime;
                if (_timer >= _targetItemProcessTime)
                {
                    _timer = 0;
                    MakeItem();

                    carryingValueChangeEvent?.Invoke(_curCarryingValue);
                    CreateInfoPanelUI(_target,$"+{1}", 2);
                }
            }
        }

        private void MakeItem()
        {
            _curCarryingValue = Mathf.Clamp(1 + _curCarryingValue, 0, _maxCarryingValue);

            bool isHaveCarrying = false;

            if (_items.Count != 0)
            {
                foreach (ItemAndValuePair item in _items)
                {
                    if (item.itemSO == _target)
                    {
                        item.value += 1;
                        isHaveCarrying = true;
                    }
                }
            }

            if (isHaveCarrying == false)
            {
                _items.Add(new ItemAndValuePair(_target, 1));
            }

            Bus<ItemMinusEvent>.Raise(new ItemMinusEvent(_resourceItem, 1));

            if (_curCarryingValue >= _maxCarryingValue)
                _isFull = true;
        }

        private ItemSO _returnTargetItem;

        public override ItemAndValuePair TakeCarraringValue(int maxValue)
        {
            if (_items.Count <= 0) return null;

            int index = 0;

            if (_returnTargetItem != null)
            {
                for (int i = 0; i < _items.Count; i++)
                {
                    if (_items[i].itemSO.itemName.Contains(_returnTargetItem.itemName))
                    {
                        index = i;
                    }    
                }
            }

            ItemAndValuePair pair = new ItemAndValuePair(_items[index].itemSO, _items[index].value);
            pair.value = Mathf.Clamp(pair.value, 0, maxValue);
            _curCarryingValue -= pair.value;

            if (_items[index].value > pair.value)
            {
                _items[index].value -= pair.value;

            }
            else
                _items.RemoveAt(index);

            if (_warrningMassage != null)
                _warrningMassage.DisableUI();

            return pair;
        }

        public override bool IsCanTake(ItemSO item = null)
        {
            bool isHaveCarrying = false;
            if (item != null)
            {
                foreach (ItemAndValuePair pair in _items)
                {
                    if (pair.itemSO == _target)
                    {
                        isHaveCarrying = true;
                    }
                }
            }
            else
                isHaveCarrying = true;

            bool isCarraring = _curCarryingValue > 0;

            return isHaveCarrying && isCarraring;
        }
    }
}
