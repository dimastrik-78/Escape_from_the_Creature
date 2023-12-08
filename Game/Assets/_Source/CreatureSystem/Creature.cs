using PlayerSystem;
using System.Collections;
using TrapSystem;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Utils;
using Utils.Event;
using Zenject;
using Random = System.Random;

namespace CreatureSystem
{
    public class Creature : MonoBehaviour
    {
        [Header("Base")]
        [SerializeField] private Transform head;
        [SerializeField] private Transform _firstSpawnPoint;
        [FormerlySerializedAs("point")] [SerializeField] private Transform[] points;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private Animator animator;
        
        [Header("Settings")] 
        [SerializeField] private float _searchDistance;
        [SerializeField] private float _baseSpeed;
        [SerializeField] private float _pursuitSpeed;
        [SerializeField] private float _fovAngel;

        [Header("Time"), Space(5)]
        [SerializeField] private float _stalkingTime;
        [SerializeField] private float _waitTime;
        [SerializeField] private float _rotateTime;

        [Header("Layer"), Space(5)] 
        [SerializeField] private LayerMask _canWalk;
        [SerializeField] private LayerMask _target;
        [SerializeField] private LayerMask _obstacle;

        [Header("Action"), Space(5)] 
        [SerializeField] private bool _onGravity;
        [SerializeField] private bool _canHearNoises;
        [SerializeField] private bool _canExamined;

            
        [Inject] private Attacker _attacker;
        [Inject] private Search _search;
        [Inject] private TrapController _trapController;
        [Inject] private Random _random;
        
        private readonly int _moveForward = Animator.StringToHash("move_forward");
        
        private IEnumerator _inspection;
        private IEnumerator _timerForTrap;
        private bool _canSwitchPath;

        private const float ROTATION_TIME = 0.01f;
        private const float WAIT = 1f;
        private const float PRE_SET_TRAP = 25f;
        private const float INSTALL_TRAP = 2f;
        
        private void Awake()
        {
            Init();
        }

        private void OnEnable()
        {
            Signals.Get<PlayerMadeSound>().AddListener(ReactionToSound);
            StartCoroutine(_timerForTrap);
        }

        private void OnDisable()
        {
            Signals.Get<PlayerMadeSound>().RemoveListener(ReactionToSound);
            StopCoroutine(_timerForTrap);
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
            navMeshAgent.SetDestination(points[_random.Next(0, points.Length)].position);
            animator.SetBool(_moveForward, true);
            navMeshAgent.speed = _baseSpeed;
            
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
            navMeshAgent.SetDestination(points[_random.Next(0, points.Length)].position);
            animator.SetBool(_moveForward, true);
            _inspection = Inspection();

            _canSwitchPath = false;
            StartCoroutine(Timer());
            
            _search.SetParameters(navMeshAgent, _target, head, _searchDistance, _fovAngel, _pursuitSpeed);
            
            _trapController.SetParameters(navMeshAgent, transform, PRE_SET_TRAP, INSTALL_TRAP, _baseSpeed);
            _timerForTrap = _trapController.TimerForTrap();
        }
    }
}
