using csiimnida.CSILib.SoundManager.RunTime;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSW.Code.Sawtooth
{
    public class SawtoothSystem : MonoBehaviour
    {
        [SerializeField] private List<SawtoothSystem> sawtoothSystemList;
        private string soundName = "Sawtooth";

        private float _rotationValue;
        private Vector3 _rotationDir;
        private Vector3 _startDir;
        private WaitForSeconds wait = new WaitForSeconds(0.5f);
        private bool _isEndRotation;
        private bool _isStopRotation = true;

        private void Awake()
        {
            _startDir = transform.eulerAngles;
        }

        public void StartSawtooth(float time, bool isLeft, Transform parent)
        {
            _rotationDir = transform.eulerAngles;
            _rotationValue = 360f / time;

            _rotationValue *= isLeft ? 1 : -1;

            StopAllCoroutines();
            StartCoroutine(SetTime());
            sawtoothSystemList.ForEach(v => v.StartSawtooth(time, !isLeft, parent));

            _isStopRotation = false;
            _isEndRotation = false;
            transform.SetParent(parent, true);
        }

        public void OneRotationSawtooth(float time, float dir = 1)
        {
            transform.DORotate(new Vector3(0, 0, 360f * dir), time, RotateMode.FastBeyond360);
        }

        public void SawtoothStop(bool isStopAll = false)
        {
            _isEndRotation = true;

            if(isStopAll)
            {
                StopAllCoroutines();
                DOTween.KillAll(gameObject);
            }

            _isStopRotation = true;
            sawtoothSystemList.ForEach(v => v.SawtoothStop(isStopAll));
        }

        public bool GetIsStopRotation() => _isStopRotation;

        public IEnumerator SetTime()
        {
            _rotationDir.z += _rotationValue;

            if(SoundManager.Instance != null)
                SoundManager.Instance.PlaySound(soundName);
            
            transform.DORotate(_rotationDir, 0.5f);

            yield return wait;

            if (_isEndRotation == false)
                StartCoroutine(SetTime());
            else
                _isStopRotation = true;
        }

        public void KillDOTween()
        {
            StopAllCoroutines();
            DOTween.KillAll(gameObject);
            foreach (var item in sawtoothSystemList)
            {
                item.KillDOTween();
            }
        }

        public void ResetSawtooth()
        {
            transform.DORotate(_startDir,0.5f);
            foreach (var item in sawtoothSystemList)
            {
                item.ResetSawtooth();
            }
        }

    }
}