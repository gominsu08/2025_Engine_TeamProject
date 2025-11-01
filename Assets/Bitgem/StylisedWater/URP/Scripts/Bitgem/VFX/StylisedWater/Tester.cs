using Bitgem.VFX.StylisedWater;
using UnityEngine;

public class Tester : MonoBehaviour
{
    [SerializeField] private WaterVolumeBase waterVolume;


    private void Start()
    {
        waterVolume.SetTileSize(10);
        waterVolume.Validate();
        waterVolume.SetIsDirty();
    }
}
