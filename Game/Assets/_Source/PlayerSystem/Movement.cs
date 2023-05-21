using UnityEngine;

namespace PlayerSystem
{
    public class Movement
    {
        private readonly Rigidbody _rb;
        private readonly Transform _transform;
        private readonly CapsuleCollider _collider;
        
        private int _speed;

        public Movement(Rigidbody rb, Transform transform, CapsuleCollider collider, int speed)
        {
            _rb = rb;
            _transform = transform;
            _collider = collider;
            _speed = speed;
        }

        public void Move(float valueX, float valueZ)
        {
            _rb.velocity = (_transform.right * valueX + _transform.forward * valueZ) * _speed;
        }

        public void RunOn(int newSpeed) => _speed = newSpeed;
        public void RunOff(int newSpeed) => _speed = newSpeed;

        public void SquatOn()
        {
            _collider.height = 1;
            _transform.position = new Vector3(_transform.position.x, _transform.position.y - 0.5f, _transform.position.z);
        }
        
        public void SquatOff()
        {
            _collider.height = 2;
            _transform.position = new Vector3(_transform.position.x, _transform.position.y + 0.5f, _transform.position.z);
        }
    }
}