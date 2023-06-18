using System;
using UnityEngine;

namespace CodeLockSystem
{
    public class ButtonNumber : MonoBehaviour
    {
        public event Action<int> OnPress;

        [SerializeField] private int num;

        public void ButtonPress()
        {
            OnPress?.Invoke(num);
        }
    }
}