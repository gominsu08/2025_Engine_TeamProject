using GMS.Code.Items;
using PSW.Code.Container;
using UnityEngine;
using UnityEngine.InputSystem;

public class CoinTestCode : MonoBehaviour
{
    public ResourceContainer resourceContainer;
    public ItemSO itemSO;
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
            resourceContainer.PlusItem(itemSO, itemCount);
        }
    }
}
