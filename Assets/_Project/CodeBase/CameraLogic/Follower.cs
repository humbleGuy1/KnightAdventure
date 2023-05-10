using UnityEngine;

namespace CodeBase.CameraLog
{
    public class Follower : MonoBehaviour
    {
        [SerializeField] private Transform _following;
        [SerializeField] private float _rotationAngelX;
        [SerializeField] private float _distance;
        [SerializeField] private float _offsetY;

        private void LateUpdate()
        {
            if (_following is null)
                return;

            Quaternion rotation = Quaternion.Euler(_rotationAngelX, 0, 0);
            Vector3 followingPosition = _following.position;
            followingPosition.y += _offsetY;
            var position = rotation * new Vector3(0, 0, -_distance) + followingPosition;

            transform.rotation = rotation;
            transform.position = position;
        }

        public void Follow(GameObject following)
        {
            _following = following.transform;
        }
    }
}

