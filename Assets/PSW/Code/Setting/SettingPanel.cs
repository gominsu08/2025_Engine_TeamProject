using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    [SerializeField] private List<VolumeText> volumeTextList;

    private void Start()
    {
        foreach(VolumeText text in volumeTextList)
        {
            print(text.Group.name);
        }
    }
}

[Serializable]
public struct VolumeText
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
