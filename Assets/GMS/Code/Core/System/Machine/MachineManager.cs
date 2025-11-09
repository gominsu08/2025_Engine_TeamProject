using DG.Tweening;
using GMS.Code.Core.Events;
using GMS.Code.Core.System.Maps;
using GMS.Code.UI.MainPanel;
using GMS.Code.Utill;
using PSW.Code.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GMS.Code.Core.System.Machines
{
    public enum MachineType
    {
        None,
        Brazier,
        MiningMachine,
        DrillingMachine,
        LoggingMachine,
        Excavator,
    }

    public class MachineAndTileInfoPair
    {
        public Machine machine;
        public TileInformation tileInformation;

        public MachineAndTileInfoPair(Machine target, TileInformation tileInfo)
        {
            machine = target;
            tileInformation = tileInfo;
        }
    }

    public class MachineContainer
    {
        public List<MachineAndTileInfoPair> machineAndTileInfoPairs = new List<MachineAndTileInfoPair>();

        public int AddEventCarryingValueChangedEvent(Action<int> handler, TileInformation tileInfo)
        {
            GetMachintToTileInfo(tileInfo).carryingValueChangeEvent += handler;

            return GetCurCarrayingValue(tileInfo);
        }

        public void RemoveEventCarryingValueChangedEvent(Action<int> handler, TileInformation tileInfo)
        {
            GetMachintToTileInfo(tileInfo).carryingValueChangeEvent -= handler;
        }

        public MachineType GetMachintTypeToTileInfo(TileInformation info)
        {
            MachineType machineType = MachineType.None;

            for (int i = 0; i < machineAndTileInfoPairs.Count; i++)
            {
                if (TileUtill.IsSame(machineAndTileInfoPairs[i].tileInformation, info))
                    machineType = machineAndTileInfoPairs[i].machine.machineSO.machineType;
            }

            return machineType;
        }

        public int GetMaxCarrayingValue(TileInformation info)
        {
            Machine machine = GetMachintToTileInfo(info);
            if (machine == null)
                return 0;
            return machine.machineSO.maxCarryingValue;
        }

        public int GetCurCarrayingValue(TileInformation info)
        {
            Machine machine = GetMachintToTileInfo(info);
            return machine.GetCurCarraringValue();
        }

        public MachineAndTileInfoPair GetPair(TileInformation info)
        {
            MachineAndTileInfoPair pair = default;
            for (int i = 0; i < machineAndTileInfoPairs.Count; i++)
            {
                if (TileUtill.IsSame(machineAndTileInfoPairs[i].tileInformation, info))
                    pair = machineAndTileInfoPairs[i];
            }

            return pair;
        }

        public Machine GetMachintToTileInfo(TileInformation info)
        {
            Machine machine = null;

            for (int i = 0; i < machineAndTileInfoPairs.Count; i++)
            {
                if (TileUtill.IsSame(machineAndTileInfoPairs[i].tileInformation, info))
                    machine = machineAndTileInfoPairs[i].machine;
            }

            return machine;
        }

        public bool HasMachineOnTile(TileInformation info)
        {
            return GetMachintToTileInfo(info) != null;
        }

        public void RemoveMachine(TileInformation tileInfo)
        {
            MachineAndTileInfoPair pair = GetPair(tileInfo);
            machineAndTileInfoPairs.Remove(pair);
            MonoBehaviour.Destroy(pair.machine.gameObject);
        }

        public void AddMachine(Machine machine, TileInformation tileInfo)
        {
            if (HasMachineOnTile(tileInfo)) return;
            MachineAndTileInfoPair pair = new MachineAndTileInfoPair(machine, tileInfo);
            machineAndTileInfoPairs.Add(pair);
        }

        public List<Machine> GetAllMachine()
        {
            List<Machine> machines = new List<Machine>();
            foreach (var machine in machineAndTileInfoPairs)
                machines.Add(machine.machine);
            return machines;
        }

        public void Update()
        {
            for (int i = 0; i < machineAndTileInfoPairs.Count; i++)
            {
                if (machineAndTileInfoPairs[i].machine.IsMining)
                {
                    machineAndTileInfoPairs[i].machine.MachineUpdate();
                }
            }
        }
    }

    public class MachineManager : MonoBehaviour
    {
        [SerializeField] private ResourceContainer _container;
        public MachineContainer MachineContainer { get; private set; }
        public List<MachineSO> machineSOs = new List<MachineSO>();

        public void Awake()
        {
            Bus<MachineBuildingEvent>.OnEvent += HandleBuildingMachine;
            MachineContainer = new MachineContainer();
        }

        public void OnDestroy()
        {
            Bus<MachineBuildingEvent>.OnEvent -= HandleBuildingMachine;
        }

        private void HandleBuildingMachine(MachineBuildingEvent evt)
        {
            MachineType machineType = evt.machineType;
            Tier machineTier = evt.tier;
            TileInformation tileInfo = evt.tileInformation;

            foreach (MachineSO machineSO in machineSOs)
            {
                if (machineSO.machineTier == machineTier)
                {
                    if (machineSO.machineType == machineType)
                    {
                        Machine machine = Instantiate(machineSO.machinePrefab, tileInfo.tileObject.transform);
                        machine.transform.position += Vector3.up * 0.5f;
                        machine.transform.DOScale(2, 0.1f);
                        machine.MachineInit(tileInfo, _container);
                        machine.MachineEnable();
                        MachineContainer.AddMachine(machine, tileInfo);
                    }
                }
            }
        }

        private void Update()
        {
            MachineContainer.Update();
        }

        public MachineType IsMachineType(TileInformation tileInfo)// 해당 타일에 설치되어있는 기계의 종류를 반환하는 함수
        {
            return MachineContainer.GetMachintTypeToTileInfo(tileInfo);
        }

        public float GetCurrentMiningTime(TileInformation tileInfo)
        {
            Machine machine = MachineContainer.GetMachintToTileInfo(tileInfo);

            return machine.MiningTime;
        }

        internal void DestroyMachine(TileInformation tileInfo)
        {
            MachineContainer.RemoveMachine(tileInfo);
        }

        internal List<Machine> GetTileInfoToHasMachine(List<TileInformation> iteList)
        {
            List<Machine> result = new List<Machine>();
            result = iteList.Aggregate(new List<Machine>(), (list, item) =>
            {
                Machine machine = MachineContainer.GetMachintToTileInfo(item);
                if (machine != null)
                {
                    if (machine.IsCanTake() && !machine.IsTargetMachine)
                    {
                        list.Add(machine);
                    }
                }
                return list;
            });

            return result;
        }

        public Machine GetTileInfoToHasCarrying()
        {
            Machine result = null;

            List<Machine> temp = MachineContainer.GetAllMachine().Aggregate(new List<Machine>(), (list, item) =>
            {
                if (item.IsCanTake())
                {
                    list.Add((Machine)item);
                }
                return list;
            });

            for (int i = 0; i < temp.Count; i++)
            {
                if (temp[i].isCarraringMax() && !temp[i].IsTargetMachine)
                {
                    result = temp[i];
                }
            }

            if(result !=null)
                return result;

            if (temp.Count > 0)
            {
                int keyCount = 0;
                while (keyCount <= 1000 && result == null)
                {
                    keyCount++;
                    Machine tempMachine = temp[UnityEngine.Random.Range(0, temp.Count)];
                    if (!tempMachine.IsTargetMachine)
                        result = tempMachine;
                }
                Debug.Log(result);
            }

            return result;
        }
    }
}
