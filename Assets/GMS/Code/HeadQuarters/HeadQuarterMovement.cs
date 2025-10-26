using GMS.Code.Core.System.Maps;
using GMS.Code.UI.TileInfoUIPanel;
using System;
using System.Collections;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace GMS.Code.HeadQuarters
{
    public class HeadQuarterMovement : MonoBehaviour
    {
        [SerializeField] private HeadQuarter headQuarter;
        [SerializeField] private float speed = 1.0f;
        [SerializeField] private float rotationSpeed = 1.0f;
        [SerializeField] private Rigidbody rbCompo;

        private TileInformation _currentTileInfo;
        private Vector3 _direction;
        private Vector3 _targetPosition;
        private Vector3 CurrnetPosition => new Vector3(transform.position.x,0, transform.position.z);
        private bool _isMoving = false;

        public void MoveStart(TileInformation info)
        {
            _isMoving = true;
            _currentTileInfo = info;
            _direction = transform.position - new Vector3(_currentTileInfo.x, 0, _currentTileInfo.z);
            _targetPosition = new Vector3(_currentTileInfo.x, 0, _currentTileInfo.z);
        }

        public void Update()
        {
            if (_isMoving)
            {
                Movement();
                Rotate();
                EndMoveCheck();
            }
        }

        private void EndMoveCheck()
        {
            if(Vector3.Distance(CurrnetPosition,_targetPosition) <= Mathf.Epsilon)
            {
                _isMoving = false;
            }
        }


        private void Movement()
        {
            Vector3 forced = _direction * speed;
            forced.y = 0;
            rbCompo.linearVelocity = forced;
        }

        private void Rotate()
        {
            //러프써서 부드럽게
            Quaternion rotation = Quaternion.Euler(0, 45, 0) * Quaternion.LookRotation(new Vector3(_direction.x, 0, _direction.z));

            headQuarter.transform.rotation = Quaternion.Lerp(headQuarter.transform.rotation, rotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }
}