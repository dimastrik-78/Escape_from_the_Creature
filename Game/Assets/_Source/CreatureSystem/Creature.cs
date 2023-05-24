using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Utils;
using Utils.Event;
using Random = System.Random;

namespace CreatureSystem
{
    public class Creature : MonoBehaviour
    {
        [SerializeField] private Transform head;
        [SerializeField] private float distance;
        [SerializeField] private float rangeAttack;
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

        private void OnEnable()
        {
            Signals.Get<PlayerMadeSound>().AddListener(ReactionToSound);
        }

        private void OnDisable()
        {
            Signals.Get<PlayerMadeSound>().RemoveListener(ReactionToSound);
        }

        private void Update()
        {
            if (PlayerFind())
            {
                StopCoroutine(_inspection);
                head.rotation = new Quaternion(0, 0, 0, 0);
                CheckRangeForAttack();
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
            
            while (rotationHead < 0)
            {
                head.rotation = Quaternion.Euler(0, head.rotation.eulerAngles.y + 1, 0);
                rotationHead++;
                yield return new WaitForSeconds(ROTATION_TIME);
            }
            
            head.rotation = new Quaternion(0, 0, 0, 0);
            navMeshAgent.enabled = true;
            navMeshAgent.SetDestination(point[_random.Next(0, point.Length)].position);
            _inspection = Inspection();
        }

        private bool PlayerFind()
        {
            Collider[] col = Physics.OverlapSphere(head.position, distance, player);
            if (col.Length != 0)
            {
                Vector3 dir = col[0].transform.position - head.position;
                float angle = Vector3.Angle(dir, head.forward);
                if (Physics.Raycast(head.position, dir, out RaycastHit hit, distance)
                    && player.Contains(hit.transform.gameObject.layer)
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

        private void CheckRangeForAttack()
        {
            if (Physics.Raycast(transform.position, transform.forward, rangeAttack, player))
            {
                Debug.Log("Attack");
            }
        }

        private void ReactionToSound(Transform soundPosition)
        {
            StopCoroutine(_inspection);
            head.rotation = new Quaternion(0, 0, 0, 0);
            navMeshAgent.SetDestination(soundPosition.position);
        }
        
        private void Init()
        {
            navMeshAgent.SetDestination(point[0].position);
            _random = new Random();
            _inspection = Inspection();
        }
    }
}
