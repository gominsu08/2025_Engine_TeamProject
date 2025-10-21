using DG.Tweening;
using GMS.Code.Core;
using GMS.Code.Items;
using PSW.Code.Container;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    [SerializeField] private ResourceContainer resourceContainer;
    [SerializeField] private ItemSO key1Item;
    [SerializeField] private ItemSO key2Item;
    [SerializeField] private ItemSO key3Item;
    [SerializeField] private int testValue;


    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            resourceContainer.PlusItem(key1Item, testValue);
            resourceContainer.PlusItem(key2Item, testValue);
            resourceContainer.PlusItem(key3Item, testValue);
        }
    }
}
