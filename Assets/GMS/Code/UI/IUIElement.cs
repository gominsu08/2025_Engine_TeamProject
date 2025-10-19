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

    public interface IUIElement<T1, T2, T3>
    {
        public void EnableForUI(T1 t1, T2 t2, T3 t3);
        public void DisableUI();
    }

    public interface IUIElement<T1, T2, T3, T4>
    {
        public void EnableForUI(T1 t1, T2 t2, T3 t3, T4 t4);
        public void DisableUI();
    }

    public interface IUIElement<T1, T2, T3, T4, T5>
    {
        public void EnableForUI(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5);
        public void DisableUI();
    }
}
