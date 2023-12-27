using PlayerSystem;
using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace CreatureSystem
{
    public class Search
    {
        private NavMeshAgent _navMeshAgent;
        private LayerMask _player;
        private Transform _head;
        private float _distance;
        private float _fovAngle;
        private float _pursuitSpeed;
        private float _headAngle;

        private const float HEAD_ROTATION_ANGEL_RIGHT = 45f;
        private const float HEAD_ROTATION_ANGEL_LEFT = -45f;

        public void SetParameters(NavMeshAgent navMeshAgent, LayerMask player, Transform head, float distance, float fovAngle, float pursuitSpeed)
        {
            _navMeshAgent = navMeshAgent;
            _player = player;
            _head = head;
            _distance = distance;
            _fovAngle = fovAngle;
            _pursuitSpeed = pursuitSpeed;
        }

        public bool PlayerFind(out IGetDamage player, ref Transform target)
        {
            Collider[] col = Physics.OverlapSphere(_head.position, _distance, _player);
            
            if (col.Length != 0)
            {
                Vector3 dir = col[0].transform.position - _head.position;
                float angle = Vector3.Angle(dir, _head.forward);
                
                if (Physics.Raycast(_head.position, dir, out RaycastHit hit, _distance)
                    && _player.Contains(hit.transform.gameObject.layer)
                    && angle < _fovAngle / 2)
                {
                    target = hit.transform;
                    // Debug.DrawRay(_head.position, dir, Color.red);
                    _navMeshAgent.enabled = true;
                    _navMeshAgent.speed = _pursuitSpeed;
                    _navMeshAgent.SetDestination(col[0].transform.position);
                    player = col[0].GetComponent<IGetDamage>();
                    
                    return true;
                }
            }

            player = null;
            return false;
        }

        public bool HeadRotationRight()
        {
            if (_headAngle < HEAD_ROTATION_ANGEL_RIGHT)
            {
                _head.rotation = Quaternion.Euler(0, _head.rotation.eulerAngles.y + 1, 0);
                _headAngle++;
                
                return true;
            }
            
            return false;
        }

        public bool HeadRotationLeft()
        {
            if (_headAngle > HEAD_ROTATION_ANGEL_LEFT)
            {
                _head.rotation = Quaternion.Euler(0, _head.rotation.eulerAngles.y - 1, 0);
                _headAngle--;
                
                return true;
            }
            
            return false;
        }

        public bool HeadRotationForward()
        {
            if (_headAngle < 0)
            {
                _head.rotation = Quaternion.Euler(0, _head.rotation.eulerAngles.y + 1, 0);
                _headAngle++;
                
                return true;
            }
            
            if (_headAngle > 0)
            {
                _head.rotation = Quaternion.Euler(0, _head.rotation.eulerAngles.y - 1, 0);
                _headAngle--;
                
                return true;
            }
            
            return false;
        }
    }
}