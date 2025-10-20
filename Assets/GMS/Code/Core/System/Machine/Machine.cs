using GMS.Code.Items;
using GMS.Code.UI.MainPanel;
using UnityEngine;
using UnityEngine.Events;

namespace GMS.Code.Core.System.Machines
{
    public class Machine : MonoBehaviour
    {
        private const float RESOURCE_GAIN_PER_COLLECT = 5f;
        private float _timer = 0;
        private ItemSO _targetItemData;
        private UnityAction<ItemSO, Tier> _action;

        public bool IsMining {  get; private set; } = false;
        public float MiningTime => _timer;

        [field: SerializeField] public MachineSO machineSO { get; private set; }

        public void MachineInit(ItemSO targetItemData , UnityAction<ItemSO,Tier> callback)
        {
            _action = callback;
            _targetItemData = targetItemData;
            _timer = 0;
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

        public void MachineUpdate()
        {
            _timer += Time.deltaTime;
            if(_timer >= RESOURCE_GAIN_PER_COLLECT)
            {
                _timer = 0;
                _action?.Invoke(_targetItemData, machineSO.machineTier);
            }
        }

        public void MachineDestroy()
        {
            MachineDisable();
        }
    }
}