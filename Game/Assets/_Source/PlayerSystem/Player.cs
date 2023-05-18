using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerSystem
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Transform body;
        [SerializeField] private Transform camer;
        [SerializeField] private int speed;

        //[Header("Cinemachine")]
        //[Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
        //public GameObject CinemachineCameraTarget;
        //[Tooltip("How far in degrees can you move the camera up")]
        //public float TopClamp = 90.0f;
        //[Tooltip("How far in degrees can you move the camera down")]
        //public float BottomClamp = -90.0f;

        private PlayerInput _input;
        private Movement _movement;
        private float _moveX;
        private float _moveZ;
        //private float _cinemachineTargetPitch;

        //private const float _threshold = 0.01f;

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

        private void FixedUpdate()
        {
            _moveX = _input.Action.MoveX.ReadValue<float>();
            _moveZ = _input.Action.MoveZ.ReadValue<float>();

            _movement.Move(_moveX, _moveZ);

            body.rotation = Quaternion.Euler(transform.rotation.x, camer.rotation.eulerAngles.y, transform.rotation.z);
        }

        //private void CameraRotation()
        //{
        //    //if there is an input
        //    if (_input.look.sqrMagnitude >= _threshold)
        //    {
        //        //Don't multiply mouse input by Time.deltaTime
        //        float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

        //        _cinemachineTargetPitch += _input.look.y * RotationSpeed * deltaTimeMultiplier;
        //        _rotationVelocity = _input.look.x * RotationSpeed * deltaTimeMultiplier;

        //        // clamp our pitch rotation
        //        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        //        // Update Cinemachine camera target pitch
        //        CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

        //        // rotate the player left and right
        //        transform.Rotate(Vector3.up * _rotationVelocity);
        //    }
        //}

        //public void LookInput(Vector2 newLookDirection)
        //{
        //    look = newLookDirection;
        //}

        private void Init()
        {
            _input = new PlayerInput();
            _movement = new Movement(rb, speed);
        }
    }
}