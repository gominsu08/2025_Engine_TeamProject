using GMS.Code.Items;
using GMS.Code.UI.ItemPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private void AddFuel(ItemSO item)
        {
            if (item.itemType != ItemType.Fuel) return;

            _fuel += item.fuelAmount;
        }

        public void SetCurrentTargetItem(ItemSO target)
        {
            this.target = target;
            _targetItemProcessTime = target.tier switch
            {
                UI.MainPanel.Tier.FirstTier => 10,
                UI.MainPanel.Tier.SecondTier => 10,
                UI.MainPanel.Tier.ThirdTier => 15,
            };

        }

        public override void MachineUpdate()
        {
            if (_curCarryingValue >= _maxCarryingValue)
            {
                if (_isFull)
                {
                    _timer += Time.deltaTime;
                    if (_timer >= RESOURCE_GAIN_PER_COLLECT)
                    {
                        _isFull = false;
                        ItemInformationUI ui = CreateInfoPanelUI("<size=1>가득참</size>", 1000);

                        if (_warrningMassage != null)
                            _warrningMassage.DisableUI();

                        _warrningMassage = ui;
                        _timer = 0;
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

            if(_fuel > 0)
            {
                _brazierTimer += Time.deltaTime;
                if(_brazierTimer >= _targetItemProcessTime)
                {
                    _brazierTimer = 0;
                    int newValue = 1;
                    _curCarryingValue = Mathf.Clamp(newValue + _curCarryingValue, 0, _maxCarryingValue);

                    if (_curCarryingValue >= _maxCarryingValue)
                        _isFull = true;

                    carryingValueChangeEvent?.Invoke(_curCarryingValue);
                    CreateInfoPanelUI($"+{newValue}", 2);
                }
            }
        }
    }
}
