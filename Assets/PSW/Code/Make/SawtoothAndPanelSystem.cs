using PSW.Code.Sawtooth;
using UnityEngine;

namespace PSW.Code.Make
{
    public class SawtoothAndPanelSystem : MonoBehaviour
    {
        [SerializeField] private SawtoothSystem sawtoothSystem;
        [SerializeField] private MakePanel makePanel;

        [Header("Sawtooth")]
        [SerializeField] private float rotationTime;
        [SerializeField] private Transform parentTransform;

        private bool _isLeft = true;

        public void SawtoothButtonClick()
        {
            if (sawtoothSystem.GetIsStopRotation() && makePanel.GetIsStopMove())
            {
                sawtoothSystem.StartSawtooth(rotationTime, _isLeft, parentTransform);
                _isLeft = !_isLeft;
                makePanel.StartPopPanel();
            }

        }
    }
}