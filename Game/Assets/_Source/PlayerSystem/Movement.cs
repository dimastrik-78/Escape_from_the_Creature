using UnityEngine;

namespace PlayerSystem
{
    public class Movement
    {
        private readonly Rigidbody _rb;
        private readonly Transform _transform;
        private readonly CapsuleCollider _collider;
        private readonly LayerMask _wall;

        private const float DISTANCE_HEAD = 1.2f;
        
        private float _stepSpeed;
        private float _squatSpeed;
        private float _runSpeed;
        private bool _squat;

        public Movement(Rigidbody rb, Transform transform, CapsuleCollider collider, LayerMask wall)
        {
            _rb = rb;
            _transform = transform;
            _collider = collider;
            _wall = wall;
        }
        
        public float Speed { get; private set; }

        public void SetSpeed(float stepSpeed, float squatSpeed, float runSpeed)
        {
            _stepSpeed = stepSpeed;
            _squatSpeed = squatSpeed;
            _runSpeed = runSpeed;

            Speed = stepSpeed;
        }

        public void Move(float valueX, float valueZ) 
            => _rb.velocity = (_transform.right * valueX + _transform.forward * valueZ) * Speed;

        public void RunOn()
        {
            if (_squat)
            {
                return;
            }
            
            Speed = _runSpeed;
        }

        public void RunOff()
        {
            if (_squat)
            {
                return;
            }
            
            Speed = _stepSpeed;
        }

        public void Squat()
        {
            if (_squat)
            {
                SquatOff();
                return;
            }

            SquatOn();
        }

        private void SquatOn()
        {
            Speed = _squatSpeed;
            
            _collider.height = 1;
            
            var pos = _transform.position;
            _transform.position = new Vector3(pos.x, pos.y - 0.5f, pos.z);

            var headPos = _transform.GetChild(0).position;
            _transform.GetChild(0).position = new Vector3(headPos.x, headPos.y - 0.5f, headPos.z);
            
            _squat = true;
        }
        
        private void SquatOff()
        {
            if (!CanStandUp())
            {
                return;
            }

            Speed = _stepSpeed;
            
            _collider.height = 2;
            
            var pos = _transform.position;
            _transform.position = new Vector3(pos.x, pos.y + 0.5f, pos.z);
            
            var headPos = _transform.GetChild(0).position;
            _transform.GetChild(0).position = new Vector3(headPos.x, headPos.y + 0.5f, headPos.z);
            
            _squat = false;
        }

        private bool CanStandUp()
        {
            if (Physics.Raycast(_transform.position, Vector3.up, DISTANCE_HEAD, _wall))
            {
                return false;
            }

            return true;
        }
    }
}