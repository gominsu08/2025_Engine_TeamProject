using GMS.Code.Core;
using GMS.Code.Core.Events;
using GMS.Code.Core.System.Machines;
using GMS.Code.Core.System.Maps;
using GMS.Code.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMS.Code.Units
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private UnitMover mover;
        [SerializeField] private UnitAnimator animator;
        [SerializeField] private GameObject backpack;

        public Machine TargetMachine { get; private set; }
        public Storage Storage { get; private set; }
        private UnitManager _manager;
        private TileManager _tileManager;
        private MachineManager _machineManager;
        private ItemSO _currentTargetItem;
        private int _count;

        public void InitUnit(UnitManager manager, Storage storage, MachineManager machineManager, TileManager tileManager)
        {
            _manager = manager;
            Storage = storage;
            _machineManager = machineManager;
            _tileManager = tileManager;
            _currentTargetItem = null;
            _count = 0;
            mover.InitMover(this);
            animator.ChangeAnimation("Move");
            StartCoroutine(WaitMachineFind());
        }

        public void SetTargetMachine(Machine machine)
        {
            TargetMachine = machine;
            mover.CalculateMachineDistance();
        }

        public void TakeItem()
        {
            //여기까지 들어왔으면 Pair안에 내용물은 무조건 존재한다.
            ItemAndValuePair pair = TargetMachine.TakeCarraringValue(_manager.UnitCarryingAmount - _count);

            if (_currentTargetItem == null && pair != null && pair.itemSO != null && pair.value != 0)
            {
                _currentTargetItem = pair.itemSO;
                backpack.gameObject.SetActive(true);
            }

            if (pair != null)
            {
                _count += pair.value;
            }
        }

        public void PutInStorage()
        {
            Debug.Log("PutInStorage");
            Bus<StorageItemAddEvent>.Raise(new StorageItemAddEvent(_currentTargetItem, _count));
            backpack.gameObject.SetActive(false);
            _currentTargetItem = null;
            _count = 0;
        }

        public void MoveEnd() //Machine으로 이동을 끝냈을때
        {
            animator.ChangeAnimation("Take");
        }

        private void Update()
        {
            if (animator.IsTake)
            {
                animator.IsTake = false;
                animator.ChangeAnimation("Move");
                if (mover.IsTargetMachine)
                {
                    
                    TakeItem();
                    if (_currentTargetItem == null)
                    {
                        Machine machine = _machineManager.GetTileInfoToHasCarrying();

                        if (machine == null)
                        {
                            StartCoroutine(WaitMachineFind());
                        }
                        else
                        {
                            SetTargetMachine(machine);
                        }
                    }
                    else if (_count < _manager.UnitCarryingAmount)
                    {
                        //현재 아이템과 같은 타일에 머신이 존재하고 그 머신에 아이템이 있다면
                        List<TileInformation> iteList = _tileManager.GetTileToItem(_currentTargetItem); //같은 아이템 타일들
                        Debug.Log(iteList);

                        List<TileInformation> machineTileList = _machineManager.GetTileInfoToHasMachine(iteList); //해당 아이템을 하나이상 가지고있는 타일들
                        Debug.Log(machineTileList);

                        if (machineTileList.Count != 0)
                        {
                            Machine nextMachine = _machineManager.MachineContainer.GetMachintToTileInfo(machineTileList[Random.Range(0, machineTileList.Count)]);
                            Debug.Log(nextMachine);
                            SetTargetMachine(nextMachine);
                        }
                        else
                        {
                            mover.CalculateStorageDistance();
                        }
                    }
                    else
                    {
                        mover.CalculateStorageDistance();
                    }

                    //만약 가방이 다 차지않았다면 다른 머신이 있는지 확인한다.
                    //다른 머신이 존재한다면 해당 머신으로 이동해서 다시 아이템을 가져온다.
                    //다른 머신이 존재하지 않는다면 바로 


                }
                else
                {
                    PutInStorage();

                    Machine machine = _machineManager.GetTileInfoToHasCarrying();

                    if (machine == null)
                    {
                        StartCoroutine(WaitMachineFind());
                    }
                    else
                    {
                        SetTargetMachine(machine);
                    }
                }
            }
        }

        private IEnumerator WaitMachineFind()
        {
            animator.ChangeAnimation("Idle");
            yield return new WaitForSeconds(Random.Range(2.5f, 3.5f));
            Machine machine = _machineManager.GetTileInfoToHasCarrying();
            if (machine == null)
            {
                StartCoroutine(WaitMachineFind());
            }
            else
            {
                animator.ChangeAnimation("Move");
                SetTargetMachine(machine);
            }
        }
    }
}