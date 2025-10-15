using DG.Tweening;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace PSW.Code.Make
{
    public class MakePanel : MonoBehaviour
    {
        [SerializeField] private float popUpTime;
        [SerializeField] private float moveValue;
        [SerializeField] private UnityEvent OnStopPanelMoveEvent;

        private WaitForSeconds wait = new WaitForSeconds(0.5f);

        private float _upValue;
        private float _currentMoveValue;
        private bool _isPopUp;
        private bool _isStopMove = true;

        private int _moveCount = 0;

        private void Awake()
        {
            _currentMoveValue = moveValue / popUpTime;
            _upValue = transform.localPosition.y;
        }

        public void StartPopPanel()
        {
            StopAllCoroutines();
            _isPopUp = !_isPopUp;
            _isStopMove = true;
            StartCoroutine(SetPopMove());
        }

        public bool GetIsStopMove() => _isStopMove;

        public IEnumerator SetPopMove()
        {
            if (_isPopUp)
            {
                _upValue += _currentMoveValue;
                _moveCount++;
            }
            else
            {
                _upValue -= _currentMoveValue;
                _moveCount--;
            }

            transform.DOLocalMoveY(_upValue, 0.5f);

            yield return wait;

            if (_moveCount < popUpTime && _moveCount > 0)
                StartCoroutine(SetPopMove());
            else
            {
                OnStopPanelMoveEvent?.Invoke();
                _isStopMove = true;
            }
        }
    }
}