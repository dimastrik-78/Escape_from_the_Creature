using UnityEngine;
using Utils;
using Utils.Event;

namespace DoorSystem
{
    public class DoorWithLocks : MonoBehaviour
    {
        [SerializeField] private Transform locks;

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
            for (int i = 0; i < locks.childCount; i++)
            {
                if (locks.GetChild(i).gameObject.activeSelf)
                {
                    return;
                }
            }
            gameObject.SetActive(false);
            Signals.Get<WinSignal>().Dispatch();
        }
    }
}
