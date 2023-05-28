using UnityEngine;

namespace PlayerSystem
{
    public class Interaction
    {
        private const float DROP_FORCE = 5f;
        
        private readonly Transform _hand;
        private FixedJoint _joint;

        private Transform _item;
        private Rigidbody _itemRb;
        private Collider _itemCollider;
        private bool _haveItem;

        public Interaction(Transform hand, FixedJoint joint)
        {
            _hand = hand;
            _joint = joint;
        }

        public bool HaveItem => _haveItem;

        public void Selection(Transform gameObject)
        {
            _item = gameObject;
            _itemRb = _item.GetComponent<Rigidbody>();
            _itemCollider = _item.GetComponent<Collider>();

            _itemRb.useGravity = false;

            _itemCollider.isTrigger = true;

            _item.parent = _hand;
            _item.position = _hand.position;
            _item.rotation = _hand.rotation;
            _joint.connectedBody = _itemRb;
            _haveItem = true;
        }

        public void Use()
        {
            Debug.Log("Try use item");
        }

        public void Drop()
        {
            _item.parent = null;
            _joint.connectedBody = null;
            _itemRb.useGravity = true;
            _itemCollider.isTrigger = false;
            _itemRb.AddForce(_item.forward * DROP_FORCE, ForceMode.Impulse);
            _itemCollider = null;
            _itemRb = null;
            _haveItem = false;
        }
    }
}