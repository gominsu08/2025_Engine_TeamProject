using GMS.Code.Items;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemListSO", menuName = "SO/Item/List")]
public class ItemListSO : ScriptableObject
{
    public List<ItemSO> itemSOList;
}
