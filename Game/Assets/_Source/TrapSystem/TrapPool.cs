using System.Collections.Generic;
using UnityEngine;

namespace TrapSystem
{
    public class TrapPool
    {
        private GameObject _trapPrefab;
        private Transform _parent;
        private List<Trap> _trapList = new();

        public Trap GetTrap()
        {
            return CheckTrapList();
        }

        public void SetParameters(GameObject trapPrefab)
        {
            _trapPrefab = trapPrefab;
        }

        private Trap CheckTrapList()
        {
            for (int i = 0; i < _trapList.Count; i++)
            {
                if (!_trapList[i].gameObject.activeSelf)
                {
                    return _trapList[i];
                }
            }

            return CreateTrap();
        }

        private Trap CreateTrap()
        {
            _trapList.Add(Object.Instantiate(_trapPrefab).GetComponent<Trap>());
            return _trapList[^1];
        }
    }
}