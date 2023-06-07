using UnityEngine;

namespace PlayerSystem
{
    public class Movement
    {
        private readonly Rigidbody _rb;
        private readonly Transform _transform;
        private readonly CapsuleCollider _collider;

        private const float DISTANCE_HEAD = 1.2f;

        private int _speed;
        private LayerMask _wall;
        private bool _squat;

        public Movement(Rigidbody rb, Transform transform, CapsuleCollider collider, int speed, LayerMask wall)
        {
            _rb = rb;
            _transform = transform;
            _collider = collider;
            _speed = speed;
            _wall = wall;
        }

        public void Move(float valueX, float valueZ)
        {
            _rb.velocity = (_transform.right * valueX + _transform.forward * valueZ) * _speed;
        }

        public void RunOn(int newSpeed) => _speed = newSpeed;
        public void RunOff(int newSpeed) => _speed = newSpeed;

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
            _collider.height = 1;
            _transform.position = new Vector3(_transform.position.x, _transform.position.y - 0.5f, _transform.position.z);
            _head.position = new Vector3(_head.position.x, _head.position.y - 0.5f, _head.position.z);
            _squat = true;
        }
        
        private void SquatOff()
        {
            if (!CanStandUp())
            {
                return;
            }

            _collider.height = 2;
            _transform.position = new Vector3(_transform.position.x, _transform.position.y + 0.5f, _transform.position.z);
            _head.position = new Vector3(_head.position.x, _head.position.y + 0.5f, _head.position.z);
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