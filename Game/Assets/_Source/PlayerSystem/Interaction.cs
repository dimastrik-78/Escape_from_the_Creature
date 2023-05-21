using UnityEngine;

namespace PlayerSystem
{
    public class Interaction
    {
        private const float DROP_FORCE = 5f;
        
        private readonly Transform _hand;

        private GameObject _item;
        private Rigidbody _itemRb;
        private Collider _itemCollider;
        private bool _haveItem;

        public Interaction(Transform hand)
        {
            _hand = hand;
        }

        public bool HaveItem => _haveItem;

        public void Selection(GameObject gameObject)
        {
            _item = gameObject;
            _itemRb = _item.GetComponent<Rigidbody>();
            _itemCollider = _item.GetComponent<Collider>();

            _itemRb.useGravity = false;

            _itemCollider.isTrigger = true;

            _item.transform.parent = _hand;
            _item.transform.position = _hand.position;
            _item.transform.rotation = _hand.rotation;
            _haveItem = true;
        }

        public void Use()
        {
            Debug.Log("Try use item");
        }

        public void Drop()
        {
            _item.transform.parent = null;
            _itemRb.useGravity = true;
            _itemCollider.isTrigger = false;
            _itemRb.AddForce(_item.transform.forward * DROP_FORCE, ForceMode.Impulse);
            _itemCollider = null;
            _itemRb = null;
            _haveItem = false;
        }
    }
}