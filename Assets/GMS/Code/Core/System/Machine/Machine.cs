using GMS.Code.Core.System.Maps;
using GMS.Code.Items;
using GMS.Code.UI.ItemPanel;
using System;
using UnityEngine;

namespace GMS.Code.Core.System.Machines
{
    public class Machine : MonoBehaviour
    {
        [SerializeField] private ItemInformationUI itemInformationUIPrefab;

        private const float RESOURCE_GAIN_PER_COLLECT = 5f;
        private float _timer = 0;
        private int _maxCarryingValue;
        private int _curCarryingValue;
        private ItemSO _targetItemData;
        private TileInformation _tileInformation;
        private ItemInformationUI _warrningMassage;

        public Action<int> carryingValueChangeEvent;
        public bool IsMining { get; private set; } = false;
        public float MiningTime => _timer;

        [field: SerializeField] public MachineSO machineSO { get; private set; }

        public void MachineInit(TileInformation tileInfo)
        {
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

        private bool _isFull;

        public void MachineUpdate()
        {
            if (_curCarryingValue >= _maxCarryingValue)
            {
                if (_isFull)
                {
                    _timer += Time.deltaTime;
                    if (_timer >= RESOURCE_GAIN_PER_COLLECT)
                    {
                        _isFull = false;
                        ItemInformationUI ui = CreateInfoPanelUI("<size=1>Ã¤±¼¿Ï·á</size>", 1000);

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
                CreateInfoPanelUI($"+{newValue}", 2);
            }
        }

        private ItemInformationUI CreateInfoPanelUI(string newValue, float duration)
        {
            Vector3 pos = new Vector3(_tileInformation.x, 0, _tileInformation.z) + new Vector3(0, 1.3f, -0.4f);

            ItemInformationUI ui = Instantiate(itemInformationUIPrefab, pos, itemInformationUIPrefab.transform.rotation);

            ui.Init(_targetItemData, newValue, duration);

            return ui;
        }

        public void MachineDestroy()
        {
            MachineDisable();
        }

        internal int GetCurCarraringValue() => _curCarryingValue;

        public int TakeCarraringValue()
        {
            int value = _curCarryingValue;
            carryingValueChangeEvent?.Invoke(_curCarryingValue);
            CreateInfoPanelUI($"-{_curCarryingValue}", 2);
            _curCarryingValue = 0;
            return value;
        }
    }
}