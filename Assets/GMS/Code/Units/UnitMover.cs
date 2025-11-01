using UnityEngine;
using UnityEngine.AI;

namespace GMS.Code.Units
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class UnitMover : MonoBehaviour
    {
        private Unit _owner;
        private bool _isMove;
        private Vector3 _targetPos;
        private NavMeshAgent _agent;

        public bool IsTargetMachine { get; private set; }
        public bool IsArrived => !_agent.pathPending && _agent.remainingDistance < _agent.stoppingDistance + stopOffset;

        public bool IsStopping => _agent.remainingDistance < _agent.stoppingDistance + stopOffset;

        [SerializeField] private float stopOffset;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float moveSpeed;

        public void InitMover(Unit unit)
        {
            _owner = unit;
            _isMove = false;
            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = moveSpeed;

        }

        public void Update()
        {
            if (_isMove)
            {
                Rotate();
                EndCheck();
            }
        }

        private void EndCheck()
        {
            if (IsArrived)
            {
                Debug.Log("EndMove");
                _owner.MoveEnd();
                _isMove = false;
                _agent.isStopped = true;
            }
        }

        private void Rotate()
        {
            if (!IsTargetMachine || (IsTargetMachine && _owner.TargetMachine != null))
            {

                Vector3 direction = _agent.steeringTarget - _owner.transform.position;
                direction.y = 0;
                Quaternion lookRotation = Quaternion.LookRotation(direction.normalized);

                _owner.transform.rotation = Quaternion.Slerp(_owner.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            }



        }

        internal void CalculateMachineDistance()
        {
            if (_owner.TargetMachine != null && _owner.TargetMachine.TryGetComponent<Collider>(out Collider collider))
            {
                _targetPos = collider.ClosestPoint(transform.position);
                _agent.SetDestination(_targetPos);
                _isMove = true;
                IsTargetMachine = true;
                _agent.isStopped = false;
            }
        }

        internal void CalculateStorageDistance()
        {
            if (_owner.Storage != null && _owner.Storage.TryGetComponent<Collider>(out Collider collider))
            {
                _targetPos = collider.ClosestPoint(transform.position);
                _agent.SetDestination(_targetPos);
                _isMove = true;
                IsTargetMachine = false;
                _agent.isStopped = false;
            }
        }
    }
}