using DG.Tweening;
using GMS.Code.Core.System.Machines;
using GMS.Code.Core.System.Maps;
using PSW.Code.Container;
using System.Collections.Generic;
using UnityEngine;

namespace GMS.Code.HeadQuarters
{
    public class HeadQuarterMovement : MonoBehaviour
    {
        [SerializeField] private HeadQuarter headQuarter;
        [SerializeField] private float speed = 1.0f;
        [SerializeField] private float rotationSpeed = 1.0f;
        [SerializeField] private float arriveThreshold = 0.1f;   // 도착 거리
        [SerializeField] private Rigidbody rbCompo;
        [SerializeField] private ResourceContainer container;

        private TileInformation _currentTileInfo;
        private Vector3 _direction;
        private Vector3 _targetPosition;
        private Vector3 CurrnetPosition => new Vector3(transform.position.x, 0, transform.position.z);
        private bool _isMoving = false;
        public bool IsCenter { get; set; } = false;

        public void MoveStart(TileInformation info, bool isCenter = false)
        {
            _targetPosition = new Vector3(info.x, 2, info.z); // 높이 고정
            _isMoving = true;
            _currentTileInfo = info;
            this.IsCenter = isCenter;
        }

        private void FixedUpdate()
        {
            if (_isMoving)
            {
                MoveTowardsTarget();
                Rotate();
            }
            else
            {
                // 이동이 아닐 땐 속도 0으로 고정
                rbCompo.linearVelocity = Vector3.zero;
            }
        }

        private void MoveTowardsTarget()
        {
            Vector3 currentPos = rbCompo.position;
            Vector3 direction = (_targetPosition - currentPos);
            direction.y = 0f; // 높이는 제외
            _direction = direction;

            if (_direction.magnitude < arriveThreshold)
            {
                // 도착
                rbCompo.linearVelocity = Vector3.zero;
                _isMoving = false;
                if (IsCenter)
                {
                    headQuarter.transform.DOMoveY(1, 3).OnComplete(() =>
                    {
                        List<ItemAndValuePair> list = Storage.Instance.TakeItemVlaue();

                        foreach (ItemAndValuePair item in list)
                        {
                            container.PlusItem(item.itemSO, item.value);
                        }
                        DOVirtual.DelayedCall(1, () => headQuarter.transform.DOMoveY(2, 3).OnComplete(() => headQuarter.MoveEnd()));
                        
                    });
                }
                else
                    headQuarter.MoveEnd();
                return;
            }

            // 목표 방향으로 속도 설정
            Vector3 velocity = _direction.normalized * speed;
            rbCompo.linearVelocity = new Vector3(velocity.x, 0f, velocity.z);
        }

        private void Rotate()
        {
            //러프써서 부드럽게
            Quaternion rotation = Quaternion.LookRotation(new Vector3(_direction.x, 0, _direction.z));

            headQuarter.transform.rotation = Quaternion.Lerp(headQuarter.transform.rotation, rotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }
}