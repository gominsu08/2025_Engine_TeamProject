using GMS.Code.Core.System.Machines;
using GMS.Code.Core.System.Maps;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMS.Code.Units
{
    public class UnitManager : MonoBehaviour
    {
        [SerializeField] private Unit unitPrefab;
        [SerializeField] private TileManager tileManager;
        [SerializeField] private MachineManager machineManager;
 
        private List<Unit> curUnit = new List<Unit>();

        public int UnitCarryingAmount = 15;

        private void Start()
        {
            UnitCreate();
            UnitCreate();
            UnitCreate();
            UnitCreate();
            UnitCreate();
            UnitCreate();
        }

        [ContextMenu("UnitCreate")]
        public void UnitCreate()
        {
            Unit unit = Instantiate(unitPrefab, transform);
            unit.InitUnit(this,Storage.Instance, machineManager, tileManager);
            curUnit.Add(unit);
        }
    }
}