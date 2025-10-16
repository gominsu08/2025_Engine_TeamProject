using UnityEngine;

public class MakeSawtooh : MonoBehaviour
{
    [SerializeField] private float rotationTime;
    [SerializeField] private Transform parentTransform;
    [SerializeField] private SawtoothSystem sawtoothSystem;

    public void StartSawtoothRotation()
    {
        sawtoothSystem.StartSawtooth(rotationTime, true, parentTransform);
    }
}
