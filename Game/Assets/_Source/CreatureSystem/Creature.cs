using System;
using PlayerSystem;
using System.Collections;
using System.Collections.Generic;
using TrapSystem;
using TrapSystem.Data;
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
        [FormerlySerializedAs("point")] [SerializeField] private List<Transform> points;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private Animator animator;
        
        [Header("Settings")] 
        [SerializeField] private float _searchDistance;
        [SerializeField] private float _baseSpeed;
        [SerializeField] private float _pursuitSpeed;
        [FormerlySerializedAs("_fovAngel")] [SerializeField] private float _fovAngle;

        [Header("Time"), Space(5)]
        [SerializeField] private float _baseStalkingTime;
        [SerializeField] private float _waitTime;
        [SerializeField] private float _rotateTime;
        [SerializeField] private float _preSetTrap;
        [SerializeField] private float _installTrap;

        [Header("Layer"), Space(5)]
        // [SerializeField] private LayerMask _canWalk;
        [FormerlySerializedAs("_target")] [SerializeField] private LayerMask _targetLayer;
        // [SerializeField] private LayerMask _obstacle;
        [SerializeField] private LayerMask _triggerForInstallTrap;

        [Header("Action"), Space(5)]
        [SerializeField] private bool _canHearNoises;
        [SerializeField] private bool _canExamined;
        [SerializeField] private bool _canSetTrap;
        
        [Inject] private Attacker _attacker;
        [Inject] private Search _search;
        [Inject] private TrapController _trapController;
        [Inject] private Random _random;
        
        private readonly int _moveForward = Animator.StringToHash("move_forward");

        private Transform _target;
        private IEnumerator _inspection;
        private IEnumerator _timerForTrap;
        private bool _canSwitchPath;
        private bool _stalking;
        private float _stalkingTime;

        private void Awake()
        {
            Init();
        }

        private void OnEnable()
        {
            Signals.Get<PlayerMadeSound>().AddListener(ReactionToSound);
            
            if (_canSetTrap)
            {
                StartCoroutine(_timerForTrap);
            }
        }

        private void OnDisable()
        {
            Signals.Get<PlayerMadeSound>().RemoveListener(ReactionToSound);
            
            if (_canSetTrap)
            {
                StopCoroutine(_timerForTrap);
            }
        }

        private void Update()
        {
            if (_search.PlayerFind(out IGetDamage player, ref _target))
            {
                StopCoroutine(_inspection);
                head.rotation = new Quaternion(0, 0, 0, 0);
                _attacker.Attack(player);
                _stalkingTime = _baseStalkingTime;
                _stalking = true;
            }
            else if (_stalkingTime > 0)
            {
                navMeshAgent.SetDestination(_target.position);
                _stalkingTime -= Time.deltaTime;
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

            if (_canExamined)
            {
                while (_search.HeadRotationRight())
                {
                    yield return new WaitForSeconds(_rotateTime);
                }
            
                yield return new WaitForSeconds(_waitTime);

                while (_search.HeadRotationLeft())
                {
                    yield return new WaitForSeconds(_rotateTime);
                }
            
                yield return new WaitForSeconds(_waitTime);
                while (_search.HeadRotationForward())
                {
                    yield return new WaitForSeconds(_rotateTime);
                }
            }
            else
            {
                yield return new WaitForSeconds(_waitTime);
            }

            navMeshAgent.enabled = true;
            navMeshAgent.SetDestination(points[_random.Next(0, points.Count)].position);
            animator.SetBool(_moveForward, true);
            navMeshAgent.speed = _baseSpeed;

            if (!_stalking)
            {
                _stalking = false;
                
                if (_canSetTrap)
                {
                    _trapController.SetTrap(TrapType.Snare);
                }
            }
            
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
            if (_canHearNoises)
            {
                StopCoroutine(_inspection);
            
                head.rotation = new Quaternion(0, 0, 0, 0);

                navMeshAgent.enabled = true;
                navMeshAgent.SetDestination(soundPosition.position);
                animator.SetBool(_moveForward, true);
            }
        }
        
        private void Init()
        {
            transform.position = _firstSpawnPoint.position;
            
            navMeshAgent.speed = _baseSpeed;
            navMeshAgent.SetDestination(points[_random.Next(0, points.Count)].position);
            animator.SetBool(_moveForward, true);
            _inspection = Inspection();

            _canSwitchPath = false;
            StartCoroutine(Timer());
            
            _search.SetParameters(navMeshAgent, _targetLayer, head, _searchDistance, _fovAngle, _pursuitSpeed);
            
            _trapController.SetParameters(navMeshAgent, transform, _preSetTrap, _installTrap, _baseSpeed);
            _timerForTrap = _trapController.TimerForTrap();

            _rotateTime /= 100;
        }

        private void OnTriggerEnter(Collider other)
        { 
            if (_canSetTrap
                && _triggerForInstallTrap.Contains(other.gameObject.layer))
            {
                _trapController.SetTrap(TrapType.Banana);
            }
        }

        public void AddPoint(Transform point)
        {
            points.Add(point);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _searchDistance);
            var dir = Vector3.Angle(transform.position, transform.position + transform.forward);
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * _searchDistance);
            Gizmos.color = Color.red;
            // Vector3 vec = new Vector3(_fovAngle / 2, 0, transform.forward.z * _searchDistance);
            // Vector3 vec = new Vector3(transform.position.x + , );
            // Mathf.Cos();
            // Mathf.Sin();
            // Gizmos.DrawLine(transform.position, vec);
        }
    }
}
