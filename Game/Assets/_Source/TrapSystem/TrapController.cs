using System.Collections;
using TrapSystem.Data;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace TrapSystem
{
    public class TrapController
    {
        private SnareTrap _snareTrap;
        private PlushToyTrap _plushToyTrap;
        private BananaTrap _bananaTrap;
        private TrapPool _pool;
        private NavMeshAgent _navMeshAgent;
        private Transform _transform;
        private GameObject _snareTrapPrefab;
        private GameObject _plushToyTrapPrefab;
        private GameObject _bananaTrapPrefab;
        private float _preTrapTime;
        private float _settingTrapTime;
        private float _baseSpeed;

        [Inject]
        public TrapController(GameObject snareTrap, GameObject plushToyTrap, GameObject bananaTrap)
        {
            _snareTrap = new SnareTrap();
            _plushToyTrap = new PlushToyTrap();
            _bananaTrap = new BananaTrap();
            _pool = new TrapPool(this);
            _plushToyTrapPrefab = plushToyTrap;
            _bananaTrapPrefab = bananaTrap;
            _snareTrapPrefab = snareTrap;
        }
        
        public void SetParameters(NavMeshAgent navMeshAgent, Transform transform, float preTrapTime, float timeForSettingTrap, float baseSpeed)
        {
            _preTrapTime = preTrapTime;
            _settingTrapTime = timeForSettingTrap;
            _navMeshAgent = navMeshAgent;
            _baseSpeed = baseSpeed;
            _transform = transform;
            _pool.SetParameters(_plushToyTrapPrefab, _bananaTrapPrefab, _snareTrapPrefab);
        }
        
        public IEnumerator TimerForTrap()
        {
            while (true)
            {
                yield return new WaitForSeconds(_preTrapTime);
            
                _navMeshAgent.speed = 0;

                yield return new WaitForSeconds(_settingTrapTime);
            
                _navMeshAgent.speed = _baseSpeed;
                SetTrap(TrapType.PlushToy);
            }
        }

        public void TrapActivation(TrapType type, GameObject gameObject, Transform transform)
        {
            switch (type)
            {
                case TrapType.Banana:
                {
                    _bananaTrap.Use(gameObject);
                    break;
                }
                case TrapType.PlushToy:
                {
                    _plushToyTrap.Use(gameObject, transform);
                    break;
                }
                case TrapType.Snare:
                {
                    _snareTrap.Use(gameObject, transform);
                    break;
                }
            }
        }

        public void SetTrap(TrapType type)
        {
            var trap = _pool.GetTrap(type);
            trap.transform.position = new Vector3(_transform.position.x, 0, _transform.position.z);
            trap.gameObject.SetActive(true);
        }
    }
}