using PSW.Code.Sawtooth;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButton : MonoBehaviour
{
    [SerializeField] private Transaction transaction;
    [SerializeField] private Transform sawtoothTrm;
    [SerializeField] private float rotationTime;
    [SerializeField] private SawtoothSystem rootSawtooth;

    private bool _isRotation;

    public void Play(string sceneName) => transaction.FadeIn(sceneName);

    public void Exit() => Application.Quit();

    public void MouseUpDown(bool isUp)
    {
        if (isUp && _isRotation == false)
        {
            rootSawtooth.StartSawtooth(rotationTime, true, sawtoothTrm);
            _isRotation = true;
        }
        else if (isUp == false)
        {
            rootSawtooth.SawtoothStop();
            rootSawtooth.ResetSawtooth();
            _isRotation = false;
        }
    }
}
