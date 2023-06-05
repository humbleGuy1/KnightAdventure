using CodeBase.Enemy.Animation;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Player;
using System.Linq;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _attackCooldown;
        [SerializeField] private float _attackRadius;
        [SerializeField] private float _effectiveDistance;
        [SerializeField] private EnemyAnimator _animator;

        private IGameFactory _factory;
        private Transform _playerTransform;
        private bool _isAttacking;
        private float _currentCooldown;
        private int _layerMask;
        private Collider[] _hits = new Collider[1];
        private bool _attackIsActive;

        private void Awake()
        {
            _factory = AllServices.Container.Single<IGameFactory>();
            _factory.PlayerCreated += OnPlayerCreated;

            _layerMask = 1 << LayerMask.NameToLayer("Player");
        }

        private void Update()
        {
            UpdateCooldown();

            if (CanAttack())
                StartAttack();
        }

        private void OnDisable() =>
            _factory.PlayerCreated -= OnPlayerCreated;

        public void DisableAttack() =>
            _attackIsActive = false;

        public void EnableAttack() =>
            _attackIsActive = true;

        //Animation event
        private void OnAttack()
        {
            if (Hit(out Collider hit))
            {
                PhysiscDebug.DrawDebug(GetStartPoint(), _attackRadius, 1f);
                hit.transform.GetComponent<PlayerHealth>().TakeDamage(_damage);
            }
        }

        //Animation event
        private void OnAttackEnded()
        {
            _currentCooldown = _attackCooldown;
            _isAttacking = false;
        }

        private bool Hit(out Collider hit)
        {
            int hitCount = Physics.OverlapSphereNonAlloc(GetStartPoint(), _attackRadius, _hits, _layerMask);

            hit = _hits.FirstOrDefault();

            return hitCount > 0;
        }

        private Vector3 GetStartPoint()
        {
            return new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) +
                transform.forward * _effectiveDistance;
        }

        private void StartAttack()
        {
            transform.LookAt(_playerTransform);
            _animator.PlayAttack();

            _isAttacking = true;
        }

        private void OnPlayerCreated() =>
            _playerTransform = _factory.PlayerGameObject.transform;

        private void UpdateCooldown()
        {
            if (_currentCooldown > 0)
                _currentCooldown -= Time.deltaTime;
        }

        private bool CanAttack() =>
            _isAttacking == false && _currentCooldown <= 0 && _attackIsActive;
    }
}

