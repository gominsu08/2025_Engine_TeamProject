using DG.Tweening;
using GMS.Code.Core;
using GMS.Code.Items;
using PSW.Code.Container;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private ResourceContainer resourceContainer;
    [SerializeField] private ItemSO keyItem;
    private void Start()
    {
        print((resourceContainer != null) + " " + (keyItem != null));
        resourceContainer.PlusItem(keyItem, 10);
        print(resourceContainer.IsTargetCountItem(keyItem, 20));
        resourceContainer.PlusItem(keyItem, 10);
        print(resourceContainer.IsTargetCountItem(keyItem, 20));
    }
}
