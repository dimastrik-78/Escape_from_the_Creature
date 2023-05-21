using UnityEngine;
using UnityEngine.Serialization;

namespace PlayerSystem
{
    public class Player : MonoBehaviour
    {
        [Header("Player settings")]
        [SerializeField] private Rigidbody rb;
        [FormerlySerializedAs("rotationCamera")] [SerializeField] private Transform transformCamera;
        [SerializeField] private CapsuleCollider playerCollider;
        [SerializeField] private int stepSpeed;
        [SerializeField] private int runSpeed;
        
        [Header("Interaction with items"), Space(5f)]
        [SerializeField] private LayerMask selectionItem;
        [SerializeField] private LayerMask useItem;
        [FormerlySerializedAs("distan")] [FormerlySerializedAs("direction")] [SerializeField] private float distance;
        [SerializeField] private Transform hand;

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
            if (Physics.Raycast(transform.position, transformCamera.forward, out _hit, distance, selectionItem))
            {
                _hit.transform.gameObject.SetActive(false);
            }
        }

        private void FixedUpdate()
        {
            _movement.Move(_input.Action.MoveX.ReadValue<float>(), _input.Action.MoveZ.ReadValue<float>());

            BodyRotate();
        }

        private void BodyRotate()
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, transformCamera.rotation.eulerAngles.y, transform.rotation.z);
        }

        private void Init()
        {
            _input = new PlayerInput();
            _interaction = new Interaction(hand);
            _movement = new Movement(rb, transform, playerCollider, stepSpeed);
            
            _input.Action.Run.started += _ => _movement.RunOn(runSpeed);
            _input.Action.Run.canceled += _ => _movement.RunOff(stepSpeed);
            _input.Action.Squat.started += _ => _movement.SquatOn();
            _input.Action.Squat.canceled += _ => _movement.SquatOff();
            _input.Action.Use.performed += _ => _interaction.Selection();
            _input.Action.Use.performed += _ => _interaction.Selection();

            // _input.Action.Run.ApplyBindingOverride($"<Keyboard>/{KeyCode.Q}");
        }
    }
}