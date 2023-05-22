using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

namespace CreatureSystem
{
    public class Creature : MonoBehaviour
    {
        [SerializeField] private Transform head;
        [SerializeField] private float distance;
        [SerializeField] private Transform[] point;
        [SerializeField] private float fovAngel;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private LayerMask player;

        private const float ROTATION_TIME = 0.01f;
        
        private Random _random;
        private IEnumerator _inspection;
        
        void Awake()
        {
            Init();
        }

        private void Update()
        {
            if (PlayerFind())
            {
                // StopAllCoroutines();
                StopCoroutine(_inspection);
                head.rotation = new Quaternion(0, 0, 0, 0);
            }
            else if (navMeshAgent.enabled
                && navMeshAgent.remainingDistance == 0)
            {
                StartCoroutine(_inspection);
            }
        }

        private IEnumerator Inspection()
        {
            navMeshAgent.enabled = false;
            var rotationHead = 0;
            while (rotationHead < 45)
            {
                head.rotation = Quaternion.Euler(0, head.rotation.eulerAngles.y + 1, 0);
                rotationHead++;
                yield return new WaitForSeconds(ROTATION_TIME);
            }
            yield return new WaitForSeconds(1f);
            
            while (rotationHead > -45)
            {
                head.rotation = Quaternion.Euler(0, head.rotation.eulerAngles.y - 1, 0);
                rotationHead--;
                yield return new WaitForSeconds(ROTATION_TIME);
            }
            yield return new WaitForSeconds(1f);
            head.rotation = new Quaternion(0, 0, 0, 0);
            navMeshAgent.enabled = true;
            navMeshAgent.SetDestination(point[_random.Next(0, point.Length)].position);
        }

        private bool PlayerFind()
        {
            Collider[] col = Physics.OverlapSphere(head.position, distance, player);
            if (col.Length != 0)
            {
                Vector3 dir = col[0].transform.position - head.position;
                float angle = Vector3.Angle(dir, head.forward);
                RaycastHit hit;
                if (Physics.Raycast(head.position, dir, out hit, distance, player)
                    && angle < fovAngel / 2)
                {
                    Debug.DrawRay(head.position, dir, Color.red);
                    navMeshAgent.enabled = true;
                    navMeshAgent.SetDestination(col[0].transform.position);
                    return true;
                }
            }

            return false;
        }

        private void Init()
        {
            navMeshAgent.SetDestination(point[0].position);
            _random = new Random();
            _inspection = Inspection();
        }

        private void OnDrawGizmos()
        {
            // RaycastHit hit;
            // // Physics.Raycast(transform.position, transform.forward, out hit,Mathf.Infinity, player);
            // Gizmos.color = Color.white;
            // Gizmos.DrawWireSphere(head.position, 5f);
            // Gizmos.color = Color.red;
            // // if (Physics.SphereCast(head.position, 5f, head.forward, out hit))
            // // {
            // //     Gizmos.DrawLine(head.position, hit.transform.position);
            // // }
            // Collider[] col = Physics.OverlapSphere(head.position, 5f, player);
            // if (col.Length != 0
            //     && col[0].transform)
            // {
            //     // Physics.Raycast(head.position, col[0].transform.position, out hit, Mathf.Infinity, player);
            //     // Gizmos.DrawLine(head.position, col[0].transform.position);
            //     search.LookAt(col[0].transform);
            //     if (search.rotation.eulerAngles.y is > -45 and < 45)
            //     {
            //         Debug.Log("I see u");
            //     }
            // }
        }
    }
}
