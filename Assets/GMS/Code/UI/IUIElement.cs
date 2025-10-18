using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Code.UI
{
    public interface IUIElement
    {
        public void EnableForUI();
        public void DisableUI();
    }

    public interface IUIElement<T>
    {
        public void EnableForUI(T t);
        public void DisableUI();
    }

    public interface IUIElement<T1, T2>
    {
        public void EnableForUI(T1 t1 , T2 t2);
        public void DisableUI();
    }
}
