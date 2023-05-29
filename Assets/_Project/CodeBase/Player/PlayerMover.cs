using Codebase.Data;
using CodeBase.Data;
using CodeBase.Infrastructure.Services;
using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Player
{
    public class PlayerMover : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _movementSpeed;

        private IInputService _inputService;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if(_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = Camera.main.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;

            _characterController.Move(_movementSpeed * Time.deltaTime * movementVector);
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.PositionOnLevel = new PositionOnLevel(GetCurrentLevelName(), transform.position.AsVectorData());
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if(GetCurrentLevelName() == progress.WorldData.PositionOnLevel.Level)
            {
                Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;

                if(progress.WorldData.PositionOnLevel.Position != null)
                    WarpTo(savedPosition);
            }
        }

        private void WarpTo(Vector3Data savedPosition)
        {
            _characterController.enabled = false;
            transform.position = savedPosition.AsUnityVector().AddY(_characterController.height);
            _characterController.enabled = true;
        }

        private string GetCurrentLevelName()
        {
            return SceneManager.GetActiveScene().name;
        }
    }
}
