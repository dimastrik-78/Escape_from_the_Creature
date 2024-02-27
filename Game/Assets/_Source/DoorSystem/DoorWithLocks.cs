using CreatureSystem;
using UnityEngine;
using Utils;
using Utils.Event;

namespace DoorSystem
{
    public class DoorWithLocks : MonoBehaviour
    {
        [SerializeField] private Creature _creature;
        [SerializeField] private Transform locks;
        [SerializeField] private Transform newPointForCreature;
        [SerializeField] private bool _finalDoor;

        private void OnEnable()
        {
            Signals.Get<LockOpeningSignal>().AddListener(CheckLocks);
        }

        private void OnDisable()
        {
            Signals.Get<LockOpeningSignal>().RemoveListener(CheckLocks);
        }

        private void CheckLocks()
        {
            if (locks.childCount != 0)
            {
                return;
            }

            if (_finalDoor)
            {
                Signals.Get<WinSignal>().Dispatch();
            }
            else
            {
                gameObject.SetActive(false);
                _creature.AddPoint(newPointForCreature);
            }
        }
    }
}
