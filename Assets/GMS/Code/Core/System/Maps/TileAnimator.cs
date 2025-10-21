using System.Collections;
using UnityEngine;

namespace GMS.Code.Core.System.Maps
{
    public class TileAnimator : MonoBehaviour
    {
        [SerializeField] private Renderer tileRenderer;
        [SerializeField] private Material defaultUpTileMat;
        [SerializeField] private Material defaultdownTileMat;
        [SerializeField] private Material selectUpTileMat;
        [SerializeField] private Material selectDownTileMat;
        private Material mat_1;
        private Material mat_2;

        private void Awake()
        {
            mat_1 = tileRenderer.materials[0];
            mat_2 = tileRenderer.materials[1];
        }

        public void SetMaterial(bool isSelect)
        {
            if (isSelect)
            {
                mat_1.SetColor("_OutlineColor",Color.white);
                mat_2.SetColor("_OutlineColor", Color.white);
            }
            else
            {
                mat_1.SetColor("_OutlineColor", Color.black);
                mat_2.SetColor("_OutlineColor", Color.black);
            }
        }


    }
}