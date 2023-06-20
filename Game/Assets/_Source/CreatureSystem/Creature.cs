using PlayerSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Utils;
using Utils.Event;
using Zenject;
using Random = System.Random;

namespace CreatureSystem
{
    public class Creature : MonoBehaviour
    {
        [SerializeField] private Transform head;
        [SerializeField] private Transform[] point;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private Animator animator;
        
        [Inject] private Attacker _attacker;
        [Inject] private Search _search;
        [Inject] private Random _random;
        
        private readonly int _moveForward = Animator.StringToHash("move_forward");
        
        private IEnumerator _inspection;
        private bool _canSwitchPath;

        private const float ROTATION_TIME = 0.01f;
        private const float WAIT = 1f;
        
        private void Start()
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
                && navMeshAgent.remainingDistance == 0
                && _canSwitchPath)
            {
                _inspection = Inspection();
                StartCoroutine(_inspection);
            }
        }

        private IEnumerator Inspection()
        {
            animator.SetBool(_moveForward, false);
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
            animator.SetBool(_moveForward, true);
            
            _canSwitchPath = false;
            StartCoroutine(Timer());
        }

        private IEnumerator Timer()
        {
            yield return new WaitForSeconds(1f);
            _canSwitchPath = true;
        }

        private void ReactionToSound(Transform soundPosition)
        {
            StopCoroutine(_inspection);
            
            head.rotation = new Quaternion(0, 0, 0, 0);

            navMeshAgent.enabled = true;
            navMeshAgent.SetDestination(soundPosition.position);
            animator.SetBool(_moveForward, true);
        }
        
        private void Init()
        {
            navMeshAgent.SetDestination(point[_random.Next(0, point.Length)].position);
            animator.SetBool(_moveForward, true);
            _inspection = Inspection();

            _canSwitchPath = false;
            StartCoroutine(Timer());
        }
    }
}
