using csiimnida.CSILib.SoundManager.RunTime;
using GMS.Code.Core;
using PSW.Code.Input;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private List<VolumeData> volumeTextList;
    [SerializeField] private Toggle paymentToggle;

    private AutoPaymentToggle _autoPaymentToggle = new AutoPaymentToggle();
    private string _soundName = "Fail";
    private void Start()
    {
        foreach (VolumeData text in volumeTextList)
            text.Init(_mixer);

        paymentToggle.onValueChanged.AddListener(SetAutoPayment);
    }

    private void OnDestroy()
    {
        paymentToggle.onValueChanged.RemoveListener(SetAutoPayment);
    }
    private void SetAutoPayment(bool isAuto)
    {
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlaySound(_soundName);

        print("s");
        _autoPaymentToggle.IsAuto = isAuto;
        Bus<AutoPaymentToggle>.Raise(_autoPaymentToggle);
    }
}

public struct AutoPaymentToggle : IEvent
{
    public bool IsAuto;
}

[Serializable]
public struct VolumeData
{
    public AudioMixerGroup Group;
    public TextMeshProUGUI Text;
    public Slider VolumeSlider;

    private AudioMixer _mixer;

    public void Init(AudioMixer mixer)
    {
        _mixer = mixer;
        VolumeSlider.onValueChanged.AddListener(SetVolume);
        SetSliderValue();
    }

    public void SetSliderValue()
    {
        float value = PlayerPrefs.GetFloat(Group.name);
        VolumeSlider.value = value;
        SetVolume(value);
    }

    public void SetVolume(float value)
    {
        _mixer.SetFloat(Group.name, value);
        SetText(value);
    }

    private void SetText(float value)
    {
        int tempValue = (int)value + 80;
        Text.text = tempValue.ToString() + '%';
    }
}
