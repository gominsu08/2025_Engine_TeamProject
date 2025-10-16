using DG.Tweening;
using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class SawtoothSystem : MonoBehaviour
{
    [SerializeField] private List<SawtoothSystem> sawtoothSystemList;
    private float _rotationValue;
    private Vector3 _rotationDir;
    private WaitForSeconds wait = new WaitForSeconds(0.5f);
    private bool _isEndRotation;

    public void StartSawtooth(float time, bool isLeft, Transform parent)
    {
        _rotationDir = transform.eulerAngles;
        _rotationValue = 360f / time;

        _rotationValue *= isLeft ? 1 : -1;

        StartCoroutine(SetTime());
        sawtoothSystemList.ForEach(v => v.StartSawtooth(time, !isLeft, parent));

        _isEndRotation = false;
        transform.SetParent(parent, true);
    }

    public void SawtoothStop()
    {
        _isEndRotation = true;
        sawtoothSystemList.ForEach(v => v.SawtoothStop());
    }

    public IEnumerator SetTime()
    {
        _rotationDir.z += _rotationValue;
        
        transform.DORotate(_rotationDir, 0.5f);

        yield return wait;

        if(_isEndRotation == false)
            StartCoroutine(SetTime());
    }

}
