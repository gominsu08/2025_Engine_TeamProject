using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Transaction : MonoBehaviour
{
    [SerializeField] private bool isStartOpen = true;
    [SerializeField] private float time = 3;
    private List<TransactionPanel> _transactionPanelList;

    private void Awake()
    {
        _transactionPanelList = GetComponentsInChildren<TransactionPanel>().ToList();
        _transactionPanelList.ForEach(v => v.Init(time));
    }

    private async void Start()
    {
        await Awaitable.WaitForSecondsAsync(0.1f);
        FadeOut();
    }

    public void FadeIn(string loadSceneName) => _transactionPanelList.ForEach(v => v.CloseTransaction(loadSceneName));
    public void FadeOut() => _transactionPanelList.ForEach(v => v.OpenTransaction());
}
