using UnityEngine;
using UnityEngine.Serialization;

namespace PlayerSystem
{
    public class Player : MonoBehaviour
    {
        [Header("Player settings")]
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Transform transformCamera;
        [SerializeField] private CapsuleCollider playerCollider;
        [SerializeField] private int stepSpeed;
        [SerializeField] private int runSpeed;
        
        [Header("Interaction with items"), Space(5f)]
        [SerializeField] private Transform hand;
        [SerializeField] private FixedJoint joint;
        [SerializeField] private LayerMask selectionItem;
        [SerializeField] private LayerMask lockItem;
        [SerializeField] private float distance;

        [Header("Button settings"), Space(5f)]
        // [SerializeField] private KeyCode step;
        // [SerializeField] private KeyCode run;
        // [SerializeField] private KeyCode squat;
        // [SerializeField] private KeyCode use;
        // [SerializeField] private KeyCode drop;
        
        private PlayerInput _input;
        private Interaction _interaction;
        private Movement _movement;
        private float _moveX;
        private float _moveZ;
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
            if (Physics.Raycast(transform.position, transformCamera.forward, out _hit, distance, selectionItem)
                && !_interaction.HaveItem)
            {
                _input.Action.SeletionItem.Enable();
            }
            else if (_interaction.HaveItem)
            {
                _input.Action.DropItem.Enable();

                if (Physics.Raycast(transform.position, transformCamera.forward, out _hit, distance, lockItem))
                {
                    _input.Action.UseItem.Enable();
                }
            }
            else
            {
                _input.Action.SeletionItem.Disable();
                _input.Action.DropItem.Disable();
                _input.Action.UseItem.Disable();
            }

            BodyRotate();
        }

        private void FixedUpdate()
        {
            _movement.Move(_input.Action.MoveX.ReadValue<float>(), _input.Action.MoveZ.ReadValue<float>());
        }

        private void BodyRotate()
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, transformCamera.rotation.eulerAngles.y, transform.rotation.z);
        }

        private void Init()
        {
            _input = new PlayerInput();
            _interaction = new Interaction(hand, joint);
            _movement = new Movement(rb, transform, playerCollider, stepSpeed);
            
            _input.Action.Run.started += _ => _movement.RunOn(runSpeed);
            _input.Action.Run.canceled += _ => _movement.RunOff(stepSpeed);
            
            _input.Action.Squat.started += _ => _movement.SquatOn();
            _input.Action.Squat.canceled += _ => _movement.SquatOff();
            
            _input.Action.SeletionItem.performed += _ => _interaction.Selection(_hit.transform);
            _input.Action.SeletionItem.performed += _ => _input.Action.SeletionItem.Disable();
            
            _input.Action.DropItem.performed += _ => _interaction.Drop();
            _input.Action.DropItem.performed += _ => _input.Action.DropItem.Disable();
            
            _input.Action.UseItem.performed += _ => _interaction.Use(_hit.transform.gameObject);
            _input.Action.UseItem.performed += _ => _input.Action.UseItem.Disable();

            // _input.Action.Run.ApplyBindingOverride($"<Keyboard>/{KeyCode.Q}");
        }
    }
}