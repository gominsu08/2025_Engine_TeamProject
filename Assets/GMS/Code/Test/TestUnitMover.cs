using UnityEngine;
using UnityEngine.AI;

namespace GMS.Code.Test
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class TestUnitMover : MonoBehaviour
    {
        [SerializeField] private Transform target;
        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            Debug.Assert(_agent != null, "Unit must have a NavMeshAgent component");
        }

        private void Update()
        {
            if (target != null)
            {
                _agent.SetDestination(target.position);
            }
        }

    }
}