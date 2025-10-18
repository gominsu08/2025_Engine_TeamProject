using GMS.Code.Core;
using GMS.Code.Core.Events;
using GMS.Code.Core.System.Machine;
using GMS.Code.UI;
using GMS.Code.UI.MainPanel;
using PSW.Code.Sawtooth;
using System;
using UnityEngine;

namespace PSW.Code.Make
{
    public enum UIType
    {
        Mining,
        Create,
        /// <summary>
        /// 화로
        /// </summary>
        Brazier
    }

    public class SawtoothAndPanelSystem : MonoBehaviour
    {
        [SerializeField] private SawtoothSystem sawtoothSystem;
        [SerializeField] private MachineManager machineManager;
        [SerializeField] private MakePanel makePanel;

        [Header("Panels")]
        [SerializeField] private ResourceMiningPanel miningPanel;
        [SerializeField] private ToolBarUI toolBarUI;

        [Header("Sawtooth")]
        [SerializeField] private float rotationTime;
        [SerializeField] private Transform parentTransform;

        private bool _isLeft = true;

        private void Awake()
        {
            miningPanel.Init(machineManager, toolBarUI);
            Bus<TileSelectEvent>.OnEvent += HandleTileSelectEvent;
        }

        private void HandleTileSelectEvent(TileSelectEvent evt)
        {
            MachineType typeEnum = machineManager.IsMachineType(evt.tileInfo);

            if(typeEnum == MachineType.None)
            {
                //건설UI
            }
            else if(typeEnum == MachineType.Brazier)
            {
                //화로UI
            }
            else
            {
                miningPanel.EnableForUI(evt.tileInfo.item, evt.tileInfo);
                SawtoothButtonClick();
            }
        }

        public void SawtoothButtonClick()
        {
            if (sawtoothSystem.GetIsStopRotation() && makePanel.GetIsStopMove())
            {
                sawtoothSystem.StartSawtooth(rotationTime, _isLeft, parentTransform);
                _isLeft = !_isLeft;
                makePanel.StartPopPanel();
            }

        }

        public void DisableAllUI()
        {
            if (_isLeft)
                miningPanel.DisableUI();
        }
    }
}