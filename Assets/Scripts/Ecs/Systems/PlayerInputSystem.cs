using Ecs.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Systems
{
    sealed class PlayerInputSystem : IEcsInitSystem, IEcsRunSystem  
    {
        private readonly EcsFilter<PlayerTag, DirectionComponent, CameraComponent> _directionFilter = null;
        
        private GameInputSystem _gameInput;
        
        public void Init()
        {
            _gameInput = new GameInputSystem();
            _gameInput.Enable();
            
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        public void Run()
        {
            foreach (var i in _directionFilter)
            {
                ref var directionComponent = ref _directionFilter.Get2(i);
                ref var direction = ref directionComponent.direction;
                
                ref var cameraComponent = ref _directionFilter.Get3(i);
                ref var targetDirection = ref cameraComponent.targetDirection;

                direction.x = DirectionMove().x;
                direction.z = DirectionMove().z;
                
                var lookInput = DirectionLook();
                targetDirection.x += lookInput.x * cameraComponent.mouseSensitivity;
                targetDirection.y -= lookInput.y * cameraComponent.mouseSensitivity;
                
                targetDirection.y = Mathf.Clamp(targetDirection.y, cameraComponent.minPitch, cameraComponent.maxPitch);
                
                Debug.Log(targetDirection);
            }
        }
        
        private Vector3 DirectionMove()
        {
            var direction = _gameInput.Player.Move.ReadValue<Vector2>();
            return new Vector3(direction.x, 0, direction.y).normalized;
        }
        private Vector2 DirectionLook()
        {
            var direction = _gameInput.Player.Look.ReadValue<Vector2>();
            return direction;
        }
    }
}