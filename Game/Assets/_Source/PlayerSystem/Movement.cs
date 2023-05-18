using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerSystem
{
    public class Movement
    {
        private Rigidbody _rb;
        private int _speed;

        public Movement(Rigidbody rb, int speed)
        {
            _rb = rb;
            _speed = speed;
        }

        public void Move(float valueX, float valueZ)
        {
            _rb.velocity = new Vector3(valueX, 0, valueZ) * _speed;
        }
    }
}