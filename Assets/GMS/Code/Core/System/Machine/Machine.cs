using GMS.Code.Core.System.Maps;
using GMS.Code.Items;
using GMS.Code.UI.ItemPanel;
using PSW.Code.Container;
using System;
using UnityEngine;

namespace GMS.Code.Core.System.Machines
{
    public class Machine : MonoBehaviour
    {
        [SerializeField] protected ItemInformationUI itemInformationUIPrefab;

        protected const float RESOURCE_GAIN_PER_COLLECT = 5f;
        protected float _timer = 0;
        protected int _maxCarryingValue;
        protected int _curCarryingValue;
        protected ItemSO _targetItemData;
        protected TileInformation _tileInformation;
        protected ItemInformationUI _warrningMassage;
        protected ResourceContainer _container;

        public Action<int> carryingValueChangeEvent;
        public bool IsMining { get; protected set; } = false;
        public float MiningTime => _timer;
        public bool IsTargetMachine { get; protected set; }

        [field: SerializeField] public MachineSO machineSO { get; protected set; }

        public virtual void MachineInit(TileInformation tileInfo, ResourceContainer container)
        {
            _container = container;
            _tileInformation = tileInfo;
            _targetItemData = tileInfo.item;
            _timer = 0;
            _maxCarryingValue = machineSO.maxCarryingValue;
            _curCarryingValue = 0;
        }

        public void MachineEnable()
        {
            _timer = 0;
            IsMining = true;
        }

        public void MachineDisable()
        {
            IsMining = false;
        }

        protected bool _isFull;

        public virtual void MachineUpdate()
        {
            if (_curCarryingValue >= _maxCarryingValue)
            {
                if (_isFull)
                {
                    _timer += Time.deltaTime;
                    if (_timer >= RESOURCE_GAIN_PER_COLLECT)
                    {
                        _isFull = false;
                        ItemInformationUI ui = CreateInfoPanelUI(_targetItemData, "<size=1>Ã¤±¼¿Ï·á</size>", 1000);

                        if (_warrningMassage != null)
                            _warrningMassage.DisableUI();

                        _warrningMassage = ui;
                        _timer = 0;
                    }
                }

                return;
            }

            _timer += Time.deltaTime;
            if (_timer >= RESOURCE_GAIN_PER_COLLECT)
            {
                _timer = 0;
                int newValue = machineSO.GetTierToValue(_targetItemData.tier) / 12;
                _curCarryingValue = Mathf.Clamp(newValue + _curCarryingValue, 0, _maxCarryingValue);

                if (_curCarryingValue >= _maxCarryingValue)
                    _isFull = true;

                carryingValueChangeEvent?.Invoke(_curCarryingValue);
                CreateInfoPanelUI(_targetItemData, $"+{newValue}", 2);
            }
        }

        protected ItemInformationUI CreateInfoPanelUI(ItemSO targetItemData, string newValue, float duration)
        {

            Vector3 pos = new Vector3(_tileInformation.x, 0, _tileInformation.z) + new Vector3(0, 1.3f, -0.4f);

            ItemInformationUI ui = Instantiate(itemInformationUIPrefab, pos, itemInformationUIPrefab.transform.rotation);

            ui.Init(targetItemData, newValue, duration);

            return ui;
        }

        public void MachineDestroy()
        {
            MachineDisable();
        }

        public bool isCarraringMax() => _curCarryingValue >= _maxCarryingValue;

        public int GetCurCarraringValue() => _curCarryingValue;

        public virtual ItemAndValuePair TakeCarraringValue(int maxValue)
        {
            int value = Mathf.Clamp(_curCarryingValue,0,maxValue);
            ItemAndValuePair pair = new ItemAndValuePair(_targetItemData, value);
            _curCarryingValue = 0;
            carryingValueChangeEvent?.Invoke(_curCarryingValue);
            if(pair.value != 0)
            //CreateInfoPanelUI(_targetItemData, $"-{pair.value}", 2);
            if (_warrningMassage != null)
                _warrningMassage.DisableUI();
            return pair;
        }

        public virtual bool IsCanTake(ItemSO item = null)
        {
            bool isValue = _curCarryingValue > 0;
            bool isItemContains = item != null ?  _targetItemData.itemName.Contains(item.itemName) : true;

            return isValue && isItemContains;
        }

        public void SetIsTargetMachine(bool isTargetMachine) => IsTargetMachine = isTargetMachine;
    }
}