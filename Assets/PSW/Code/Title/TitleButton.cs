using csiimnida.CSILib.SoundManager.RunTime;
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

    private string _soundName = "Button";

    private bool _isRotation;
    private bool _isClickPlay;

    public void Play(string sceneName)
    {
        if (_isClickPlay) return;

        _isClickPlay = true;
        Time.timeScale = 1;
        transaction.FadeIn(sceneName);
        ButtonSound();
    }
    public void ButtonSound()
    {
        if(SoundManager.Instance != null)
            SoundManager.Instance.PlaySound(_soundName);
    }
    public void Exit()
    {
        ButtonSound();
        Application.Quit();
    }

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
