using UnityEngine;

namespace GMS.Code.Units
{
    public class UnitAnimator : MonoBehaviour
    {
        private int _currentAnimName = 0;
        [SerializeField] private Animator animator;

        public bool IsTake {  get; set; }

        public void ChangeAnimation(string animName)
        {
            if(_currentAnimName != 0)
            animator.SetBool(_currentAnimName, false);
            _currentAnimName =  Animator.StringToHash(animName);
            animator.SetBool(_currentAnimName, true);
        }

        public void TakeAnimationEnd() => IsTake = true;

    }
}