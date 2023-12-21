using System.Collections.Generic;
using TrapSystem.Data;
using UnityEngine;

namespace TrapSystem
{
    public class TrapPool
    {
        private TrapController _trapController;
        private GameObject _bananaTrapPrefab;
        private GameObject _plushToyTrapPrefab;
        private GameObject _snareTrapPrefab;
        private Transform _parent;
        private List<Trap> _trapList = new();

        public TrapPool(TrapController trapController)
        {
            _trapController = trapController;
        }

        public Trap GetTrap(TrapType type)
        {
            return CheckTrapList(type);
        }

        public void SetParameters(GameObject plushToyTrapPrefab, GameObject bananaTrapPrefab, GameObject snareTrapPrefab)
        {
            _plushToyTrapPrefab = plushToyTrapPrefab;
            _bananaTrapPrefab = bananaTrapPrefab;
            _snareTrapPrefab = snareTrapPrefab;
        }

        private Trap CheckTrapList(TrapType type)
        {
            for (int i = 0; i < _trapList.Count; i++)
            {
                if (!_trapList[i].gameObject.activeSelf
                    && _trapList[i].GetTrapType() == type)
                {
                    return _trapList[i];
                }
            }

            return CreateTrap(type);
        }

        private Trap CreateTrap(TrapType type)
        {
            GameObject trapPrefab = null;
            
            switch (type)
            {
                case TrapType.PlushToy:
                {
                    trapPrefab = _plushToyTrapPrefab;
                    break;
                }
                case TrapType.Banana:
                {
                    trapPrefab = _bananaTrapPrefab;
                    break;
                }
                case TrapType.Snare:
                {
                    trapPrefab = _snareTrapPrefab;
                    break;
                }
            } 
            
            _trapList.Add(Object.Instantiate(trapPrefab).GetComponent<Trap>());
            _trapList[^1].Construct(_trapController);
            return _trapList[^1];
        }
    }
}