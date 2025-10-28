using GMS.Code.Items;
using GMS.Code.UI.ItemPanel;
using System.Collections.Generic;
using UnityEngine;

namespace GMS.Code.Core.System.Machines
{
    public class Brazier : Machine
    {
        private const float ONE_FUEL_TIME = 60f;
        private float _targetItemProcessTime;
        private float _brazierTimer;
        private int _fuel;
        private ItemSO target;
        private List<ItemAndValuePair> _items = new List<ItemAndValuePair>();

        private void AddFuel(ItemSO item)
        {
            if (item.itemType != ItemType.Fuel) return;

            _fuel += item.fuelAmount;
        }

        public void SetCurrentTargetItem(ItemSO target)
        {
            this.target = target;
            if(target != null)
            {
                _targetItemProcessTime = target.tier switch
                {
                    UI.MainPanel.Tier.FirstTier => 10,
                    UI.MainPanel.Tier.SecondTier => 10,
                    UI.MainPanel.Tier.ThirdTier => 15,
                };
            }
            _brazierTimer = 0;
        }

        public override void MachineUpdate()
        {
            if (_curCarryingValue >= _maxCarryingValue)
            {
                if (_isFull)
                {
                    _brazierTimer += Time.deltaTime;
                    if (_brazierTimer >= _targetItemProcessTime)
                    {
                        _isFull = false;
                        ItemInformationUI ui = CreateInfoPanelUI("<size=1>가득참</size>", 1000);

                        if (_warrningMassage != null)
                            _warrningMassage.DisableUI();

                        _warrningMassage = ui;
                        _brazierTimer = 0;
                    }
                }

                return;
            }

            _timer += Time.deltaTime;

            if (_timer >= ONE_FUEL_TIME)
            {
                _timer = 0;
                _fuel--;
            }

            if (_fuel > 0 && target != null)
            {
                _brazierTimer += Time.deltaTime;
                if (_brazierTimer >= _targetItemProcessTime)
                {
                    _brazierTimer = 0;
                    int newValue = 1;
                    _curCarryingValue = Mathf.Clamp(newValue + _curCarryingValue, 0, _maxCarryingValue);

                    bool isHaveCarrying = false;
                    
                    if(_items.Count != 0)
                    {
                        foreach (ItemAndValuePair item in _items)
                        {
                            if (item.itemSO == target)
                            {
                                item.value += 1;
                                isHaveCarrying = true;
                            }
                        }
                    }
                    else
                        _items.Add(new ItemAndValuePair(target, 1));

                    if (isHaveCarrying == false)
                        _items.Add(new ItemAndValuePair(target, 1));

                    if (_curCarryingValue >= _maxCarryingValue)
                        _isFull = true;

                    carryingValueChangeEvent?.Invoke(_curCarryingValue);
                    CreateInfoPanelUI($"+{newValue}", 2);
                }
            }
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

            if (_items[index].value > pair.value)
                _items[index].value -= pair.value;
            else
                _items.RemoveAt(index);

            return pair;
        }

        public override bool IsCanTake(ItemSO item = null)
        {
            bool isHaveCarrying = false;
            if (item != null)
            {
                foreach (ItemAndValuePair pair in _items)
                {
                    if (pair.itemSO == target)
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
