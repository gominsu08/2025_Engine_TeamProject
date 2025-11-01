using PSW.Code.Input;
using PSW.Code.Sale;
using PSW.Code.Sawtooth;
using UnityEngine;

public class SettingPopPanel : PopPanel
{
    [SerializeField] private UiInputSO inputSO;

    private void Start()
    {
        base.Start();
        inputSO.OnESCPressrd += ESCPopUpDownPanel;
    }

    private void OnDestroy()
    {
        inputSO.OnESCPressrd -= ESCPopUpDownPanel;
    }

    public void ESCPopUpDownPanel()
    {
        if (sawtoothSystem.GetIsStopRotation() == false) return;

        PopUpDownPanel();
    }
}
