using ItemsSystem.Strategy;
using LevelSystem;
using UnityEngine;
using Zenject;

namespace PlayerSystem
{
    public class Interaction
    {
        private const float DROP_FORCE = 5f;
        
        private readonly Transform _hand;
        private readonly FixedJoint _joint;

        [Inject] private ChangeStrategy _changeStrategy;

        private Transform _item;
        private Rigidbody _itemRb;
        private Collider _itemCollider;
        private IStrategy _strategy;
        private bool _haveItem;

        public Interaction(Transform hand, FixedJoint joint)
        {
            _hand = hand;
            _joint = joint;
        }
        
        public bool HaveItem => _haveItem;

        private void ResetParameters()
        {
            _itemCollider = null;
            _itemRb = null;
            _haveItem = false;
        }

        public void Selection(Transform transform)
        {
            _item = transform;
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

        public void Use(GameObject gameObject, InteractionObjectEnum objectEnum)
        {
            _strategy = _changeStrategy.SwitchStrategy(objectEnum);
            _strategy.Use(_item.gameObject, gameObject);

            if (!_item.gameObject.activeSelf)
            {
                ResetParameters();
            }
        }

        public void Drop()
        {
            _item.parent = null;
            _joint.connectedBody = null;
            _itemRb.useGravity = true;
            _itemCollider.isTrigger = false;
            _itemRb.AddForce(_item.forward * DROP_FORCE, ForceMode.Impulse);
            ResetParameters();
        }
    }
}