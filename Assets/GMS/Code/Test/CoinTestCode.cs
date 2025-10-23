using GMS.Code.Items;
using PSW.Code.Container;
using UnityEngine;
using UnityEngine.InputSystem;

public class CoinTestCode : MonoBehaviour
{
    public ResourceContainer resourceContainer;
    public ItemSO itemSO_1;
    public ItemSO itemSO_2;
    public ItemSO itemSO_3;
    public ItemSO itemSO_4;
    public ItemSO itemSO_5;
    public ItemSO itemSO_6;
    public ItemSO itemSO_7;
    public int itemCount = 1;
    public int coinCount = 20000;

    public void Update()
    {
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            resourceContainer.PlusCoin(coinCount);
        }

        if(Keyboard.current.qKey.wasPressedThisFrame)
        {
            resourceContainer.PlusItem(itemSO_1, itemCount);
            resourceContainer.PlusItem(itemSO_2, itemCount);
            resourceContainer.PlusItem(itemSO_3, itemCount);
            resourceContainer.PlusItem(itemSO_4, itemCount);
            resourceContainer.PlusItem(itemSO_5, itemCount);
            resourceContainer.PlusItem(itemSO_6, itemCount);
            resourceContainer.PlusItem(itemSO_7, itemCount);
        }
    }
}
