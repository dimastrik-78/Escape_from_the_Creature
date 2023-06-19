using System;
using UnityEngine;

namespace CodeLockSystem
{
    public class CodeLock : MonoBehaviour
    {
        [SerializeField] private GameObject key;
        [SerializeField] private int code;

        private int[] _inputCode = {-1, -1, -1, -1};
        private int _index;

        public void InputCode(int num)
        {
            _inputCode[_index] = num;
            _index++;

            if (_index < _inputCode.Length)
            {
                return;
            }
            
            CheckCode();
        }

        private void CheckCode()
        {
            double checkCode = code;
            double del = Math.Pow(10, _inputCode.Length - 1);
            
            for (int i = 0; i < _inputCode.Length; i++)
            {
                if ((int)(checkCode / del) != _inputCode[i])
                {
                    CodeReset();
                    return;
                }
                
                checkCode %= del;
                del /= 10;
            }

            LockOpen();
        }

        private void LockOpen()
        {
            key.SetActive(true);
            gameObject.SetActive(false);
        }

        private void CodeReset()
        {
            _inputCode = new [] {-1, -1, -1, -1};
            _index = 0;
        }
    }
}
