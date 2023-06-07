using PlayerSystem;
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

        private Attacker _attacker;
        private Search _search;
        
        private Random _random;
        private IEnumerator _inspection;
        
        private const float ROTATION_TIME = 0.01f;
        private const float WAIT = 1f;
        
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
            if (_search.PlayerFind(out IGetDamage player))
            {
                StopCoroutine(_inspection);
                head.rotation = new Quaternion(0, 0, 0, 0);
                _attacker.Attack(player);
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

            while (_search.HeadRotationRight())
            {
                yield return new WaitForSeconds(ROTATION_TIME);
            }
            
            yield return new WaitForSeconds(WAIT);

            while (_search.HeadRotationLeft())
            {
                yield return new WaitForSeconds(ROTATION_TIME);
            }
            
            yield return new WaitForSeconds(WAIT);
            while (_search.HeadRotationForward())
            {
                yield return new WaitForSeconds(ROTATION_TIME);
            }

            navMeshAgent.enabled = true;
            navMeshAgent.SetDestination(point[_random.Next(0, point.Length)].position);
            _inspection = Inspection();
        }

        private void ReactionToSound(Transform soundPosition)
        {
            StopCoroutine(_inspection);
            head.rotation = new Quaternion(0, 0, 0, 0);
            navMeshAgent.SetDestination(soundPosition.position);
        }
        
        private void Init()
        {
            _attacker = new Attacker(transform, rangeAttack, player);
            _search = new Search(navMeshAgent, player, head, distance, fovAngel);
            
            navMeshAgent.SetDestination(point[0].position);
            _random = new Random();
            _inspection = Inspection();
        }
    }
}
