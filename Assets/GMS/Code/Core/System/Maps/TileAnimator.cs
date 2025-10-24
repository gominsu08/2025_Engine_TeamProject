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
                mat_1.DisableKeyword("_MK_OUTLINE_HULL_CLIP");
                mat_2.DisableKeyword("_MK_OUTLINE_HULL_CLIP");
                mat_1.EnableKeyword("_MK_OUTLINE_HULL_ORIGIN");
                mat_2.EnableKeyword("_MK_OUTLINE_HULL_ORIGIN");
                
                mat_1.SetColor("_OutlineColor",Color.white);
                mat_2.SetColor("_OutlineColor", Color.white);
                mat_1.SetInt("_Outline", 1);
                mat_2.SetInt("_Outline", 1);
            }
            else
            {

                mat_1.DisableKeyword("_MK_OUTLINE_HULL_ORIGIN");
                mat_2.DisableKeyword("_MK_OUTLINE_HULL_ORIGIN");
                mat_1.EnableKeyword("_MK_OUTLINE_HULL_CLIP");
                mat_2.EnableKeyword("_MK_OUTLINE_HULL_CLIP");

                mat_1.SetColor("_OutlineColor", Color.black);
                mat_2.SetColor("_OutlineColor", Color.black);
                mat_1.SetInt("_Outline", 0);
                mat_2.SetInt("_Outline", 0);
            }
            
        }


    }
}