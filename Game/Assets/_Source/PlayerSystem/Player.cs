using System;
using System.Collections;
using Cinemachine;
using CodeLockSystem;
using LevelSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;
using Utils.Event;
using Zenject;

namespace PlayerSystem
{
    public class Player : MonoBehaviour, IGetDamage
    {
        public event Action<string> LookOnItem;
        public event Action NotLookOnItem;
        public event Action OnPause;

        [Header("Player settings")] 
        [SerializeField] private Transform transformCamera;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private float stepSpeed;
        [SerializeField] private float runSpeed;
        [SerializeField] private float squatSpeed;
        [SerializeField] private float blockRun;
        [SerializeField] private AudioSource source;
        
        [Header("Interaction with items"), Space(5f)]
        [SerializeField] private LayerMask selectionItem;
        [SerializeField] private LayerMask lockItem;
        [SerializeField] private LayerMask enemyMask;
        [SerializeField] private LayerMask buttonCodeLock;
        [SerializeField] private LayerMask _trapLayer;
        [SerializeField] private float distance;

        [Inject] private PlayerInput _input;
        [Inject] private Interaction _interaction;
        [Inject] private Movement _movement;
        [Inject] private DamageReaction _damageReaction;
        [Inject] private Stamina _stamina;
        [Inject] private InteractionWithTrap _interactionWithTrap;

        private IEnumerator _decreasedStamina;
        private IEnumerator _restoringStamina;
        private RaycastHit _hit;

        void Start()
        {
            InitParameters();
            InitActions();
            BindEvent();
        }

        private void OnEnable()
        {
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        private void OnDestroy()
        {
            ExposeEvent();
        }

        private void Update()
        {
            Eye();
            BodyRotate();
        }

        private void FixedUpdate()
        {
            float moveX = _input.Action.MoveX.ReadValue<float>();
            float moveZ = _input.Action.MoveZ.ReadValue<float>();
            
            _movement.Move(moveX, moveZ);
            if ((moveX != 0
                || moveZ != 0)
                && !source.isPlaying)
            {
                source.Play();
            }
        }

        private void BindEvent()
        {
            Signals.Get<StanPlayerEvent>().AddListener(Trap);
            Signals.Get<TrappedPlayerEvent>().AddListener(Trapped);
        }

        private void ExposeEvent()
        {
            Signals.Get<StanPlayerEvent>().RemoveListener(Trap);
            Signals.Get<TrappedPlayerEvent>().RemoveListener(Trapped);
        }

        private void Eye()
        {
            if (Physics.Raycast(transformCamera.position, transformCamera.forward, out _hit, distance, selectionItem)
                && !_interaction.HaveItem)
            {
                _input.Action.SeletionItem.Enable();
                LookOnItem?.Invoke("Press");
            }
            else if (_interaction.HaveItem)
            {
                _input.Action.DropItem.Enable();
                NotLookOnItem?.Invoke();

                if (Physics.Raycast(transformCamera.position, transformCamera.forward, out _hit, distance, lockItem)
                    && _hit.transform.GetComponent<InteractionObject>().CanInteractionItem == _interaction.EnumTypeItem)
                {
                    _input.Action.UseItem.Enable();
                    LookOnItem?.Invoke("Press");
                }
            }
            else if (Physics.Raycast(transformCamera.position, transformCamera.forward, out _hit, distance, buttonCodeLock))
            {
                _input.Action.Press.Enable();
                LookOnItem?.Invoke("Press");
            }
            else
            {
                _input.Action.SeletionItem.Disable();
                _input.Action.DropItem.Disable();
                _input.Action.UseItem.Disable();
                _input.Action.Press.Disable();
                NotLookOnItem?.Invoke();
            }

            if (Physics.Raycast(transformCamera.position, transformCamera.forward, out _hit, distance, _trapLayer))
            {
                LookOnItem?.Invoke("Hold");
            }
        }

        private void Trap()
        {
            StartCoroutine(_interactionWithTrap.Stan());
        }

        private void Trapped()
        {
            _interactionWithTrap.InfiniteStan();
        }

        private void BodyRotate()
        {
            Quaternion rotation = transform.rotation;
            transform.rotation = Quaternion.Euler(rotation.x, transformCamera.rotation.eulerAngles.y, rotation.z);
        }

        private void OnStaminaOver()
        {
            StartCoroutine(BlockRun());
        }

        private IEnumerator BlockRun()
        {
            _input.Action.Run.Disable();
            
            yield return new WaitForSeconds(blockRun);
            
            _input.Action.Run.Enable();
        }

        private void InitParameters()
        {
            _decreasedStamina = _stamina.DecreasedStamina();
            _restoringStamina = _stamina.RestoringStamina();
            
            _movement.SetSpeed(stepSpeed, squatSpeed, runSpeed);
            
            _stamina.OnStaminaOver += OnStaminaOver;
            
            _interactionWithTrap.SetParameters(transformCamera, _input, 3f, distance, _trapLayer);
        }

        private void InitActions()
        {
            _input.Action.Pause.performed += _ => OnPause?.Invoke();
            
            _input.Action.Run.started += _ => _movement.RunOn();
            _input.Action.Run.started += _ =>  _decreasedStamina = _stamina.DecreasedStamina();
            _input.Action.Run.started += _ =>  StartCoroutine(_decreasedStamina);
            _input.Action.Run.started += _ =>  StopCoroutine(_restoringStamina);
            
            _input.Action.Run.canceled += _ => _movement.RunOff();
            _input.Action.Run.canceled += _ => _restoringStamina = _stamina.RestoringStamina();
            _input.Action.Run.canceled += _ => StartCoroutine(_restoringStamina);
            _input.Action.Run.canceled += _ => StopCoroutine(_decreasedStamina);
            
            _input.Action.Squat.started += _ => _movement.Squat();
            
            _input.Action.SeletionItem.performed += _ => _interaction.Selection(_hit.transform);
            _input.Action.SeletionItem.performed += _ => _input.Action.SeletionItem.Disable();
            
            _input.Action.DropItem.performed += _ => _interaction.Drop();
            _input.Action.DropItem.performed += _ => _input.Action.DropItem.Disable();
            
            _input.Action.UseItem.performed += _ => _interaction.Use(_hit.transform.gameObject, 
                _hit.transform.gameObject.GetComponent<InteractionObject>().ObjectEnum);
            _input.Action.UseItem.performed += _ => _input.Action.UseItem.Disable();

            _input.Action.Press.performed += _ => _hit.transform.GetComponent<ButtonNumber>().ButtonPress();
            
            _input.Action.IntaractionWithTrap.performed += _ => _interactionWithTrap.Interaction();
        }

        public void GetDamage(Transform enemy)
        {
            if (_interaction.HaveItem)
            {
                _interaction.Drop();
            }
            
            virtualCamera.enabled = false;
            enabled = false;
            StartCoroutine(_damageReaction.PlayerTurnOnAnObject(transform, enemy, enemyMask));
        }
    }
}