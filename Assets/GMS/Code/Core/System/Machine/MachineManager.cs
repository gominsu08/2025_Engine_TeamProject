using DG.Tweening;
using GMS.Code.Core.Events;
using GMS.Code.Core.System.Maps;
using GMS.Code.Items;
using GMS.Code.UI.MainPanel;
using GMS.Code.Utill;
using PSW.Code.Container;
using System;
using System.Collections.Generic;
using System.Resources;
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

        public void AddMachine(Machine machine, TileInformation tileInfo)
        {
            if (HasMachineOnTile(tileInfo)) return;
            MachineAndTileInfoPair pair = new MachineAndTileInfoPair(machine, tileInfo);
            machineAndTileInfoPairs.Add(pair);
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
                        machine.transform.DOScale(2,0.1f);
                        machine.MachineInit(tileInfo.item,HandleItemGet);
                        machine.MachineEnable();
                        MachineContainer.AddMachine(machine, tileInfo);
                    }
                }
            }
        }

        private void HandleItemGet(ItemSO targetItem, MachineSO machineSO)
        {
           int value = machineSO.GetTierToValue(targetItem.tier);

            _container.PlusItem(targetItem,value / 12);
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
    }
}
