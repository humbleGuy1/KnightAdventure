using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    public class AgentMoverToPlayer : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;

        private Transform _playerTransform;
        private IGameFactory _gameFactory;

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            if (_gameFactory.PlayerGameObject != null)
                InitializePlayerTransform();
            else
                _gameFactory.PlayerCreated += PlayerCreated;
        }

        private void Update()
        {
            if (_playerTransform != null && MinDistanceNotReached())
                _agent.destination = _playerTransform.position;
        }

        private bool MinDistanceNotReached() => 
            Vector3.Distance(_agent.transform.position, _playerTransform.position) >= Constants.midDistance;

        private void PlayerCreated() => InitializePlayerTransform();

        private void InitializePlayerTransform() => 
            _playerTransform = _gameFactory.PlayerGameObject.transform;

    }
}

