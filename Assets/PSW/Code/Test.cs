using DG.Tweening;
using GMS.Code.Core;
using GMS.Code.Items;
using PSW.Code.Container;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    [SerializeField] private ResourceContainer resourceContainer;
    [SerializeField] private ItemSO keyItem;
    private void Start()
    {
        print((resourceContainer != null) + " " + (keyItem != null));
        resourceContainer.PlusCoin(10);
        print(resourceContainer.IsTargetCountItem(keyItem, 20));
        resourceContainer.PlusCoin(10);
        print(resourceContainer.IsTargetCountItem(keyItem, 20));
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            resourceContainer.PlusCoin(10);
    }
}
