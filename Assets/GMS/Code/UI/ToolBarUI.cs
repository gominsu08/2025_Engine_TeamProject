using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GMS.Code.UI
{
    public struct ToolBarUIData
    {
        string text;
    }

    public class ToolBarUI : MonoBehaviour, IUIElement<Vector2, ToolBarUIData>
    {
        public void DisableUI()
        {

        }

        public void EnableForUI(Vector2 t1, ToolBarUIData t2)
        {

        }
    }
}
