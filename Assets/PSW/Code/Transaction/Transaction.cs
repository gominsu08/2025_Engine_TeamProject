using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Transaction : MonoBehaviour
{
    [SerializeField] private bool isStartOpen = true;
    [SerializeField] private bool isOpen;
    [SerializeField] private float time = 3;
    private List<TransactionPanel> _transactionPanelList;

    private void Awake()
    {
        _transactionPanelList = GetComponentsInChildren<TransactionPanel>().ToList();
        _transactionPanelList.ForEach(v => v.Init(time, isOpen));
    }

    private async void Start()
    {
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
        if (isStartOpen)
        {
            await Awaitable.WaitForSecondsAsync(0.1f);
            FadeOut();
        }
    }

    public void FadeIn(string loadSceneName) => _transactionPanelList.ForEach(v => v.CloseTransaction(loadSceneName));
    public void FadeOut() => _transactionPanelList.ForEach(v => v.OpenTransaction());
}
