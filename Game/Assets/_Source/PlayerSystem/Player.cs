using System;
using Cinemachine;
using LevelSystem;
using UnityEngine;
using Zenject;

namespace PlayerSystem
{
    public class Player : MonoBehaviour, IGetDamage
    {
        public event Action LookOnItem;
        public event Action NotLookOnItem;
        public event Action OnPause;
        
        [Header("Player settings")]
        [SerializeField] private Transform transformCamera;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private int stepSpeed;
        [SerializeField] private int runSpeed;
        [SerializeField] private int squatSpeed;
        
        [Header("Interaction with items"), Space(5f)]
        [SerializeField] private LayerMask selectionItem;
        [SerializeField] private LayerMask lockItem;
        [SerializeField] private LayerMask enemyMask;
        [SerializeField] private float distance;

        [Header("Button settings"), Space(5f)]
        // [SerializeField] private KeyCode step;
        // [SerializeField] private KeyCode run;
        // [SerializeField] private KeyCode squat;
        // [SerializeField] private KeyCode use;
        // [SerializeField] private KeyCode drop;
        
        [Inject] private PlayerInput _input;
        [Inject] private Interaction _interaction;
        [Inject] private Movement _movement;
        [Inject] private DamageReaction _damageReaction;

        private RaycastHit _hit;

        void Awake()
        {
            Init();
        }

        private void OnEnable()
        {
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        private void Update()
        {
            Eye();
            BodyRotate();
        }

        private void FixedUpdate()
        {
            _movement.Move(_input.Action.MoveX.ReadValue<float>(), _input.Action.MoveZ.ReadValue<float>());
        }

        private void Eye()
        {
            if (Physics.Raycast(transformCamera.position, transformCamera.forward, out _hit, distance, selectionItem)
                && !_interaction.HaveItem)
            {
                _input.Action.SeletionItem.Enable();
                LookOnItem?.Invoke();
            }
            else if (_interaction.HaveItem)
            {
                _input.Action.DropItem.Enable();
                NotLookOnItem?.Invoke();

                if (Physics.Raycast(transformCamera.position, transformCamera.forward, out _hit, distance, lockItem))
                {
                    _input.Action.UseItem.Enable();
                    LookOnItem?.Invoke();
                }
            }
            else
            {
                _input.Action.SeletionItem.Disable();
                _input.Action.DropItem.Disable();
                _input.Action.UseItem.Disable();
                NotLookOnItem?.Invoke();
            }
        }

        private void BodyRotate()
        {
            Quaternion rotation = transform.rotation;
            transform.rotation = Quaternion.Euler(rotation.x, transformCamera.rotation.eulerAngles.y, rotation.z);
        }

        private void Init()
        {
            _movement.SetSpeed(stepSpeed, squatSpeed, runSpeed);
            
            _input.Action.Pause.performed += _ => OnPause?.Invoke();

            _input.Action.Run.started += _ => _movement.RunOn();
            _input.Action.Run.canceled += _ => _movement.RunOff();
            
            _input.Action.Squat.started += _ => _movement.Squat();
            // _input.Action.Squat.started += _ => _movement.RunOff();
            
            _input.Action.SeletionItem.performed += _ => _interaction.Selection(_hit.transform);
            _input.Action.SeletionItem.performed += _ => _input.Action.SeletionItem.Disable();
            
            _input.Action.DropItem.performed += _ => _interaction.Drop();
            _input.Action.DropItem.performed += _ => _input.Action.DropItem.Disable();
            
            _input.Action.UseItem.performed += _ => _interaction.Use(_hit.transform.gameObject, 
                _hit.transform.gameObject.GetComponent<InteractionObject>().GetItemEnum());
            _input.Action.UseItem.performed += _ => _input.Action.UseItem.Disable();

            // _input.Action.Run.ApplyBindingOverride($"<Keyboard>/{KeyCode.Q}");
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