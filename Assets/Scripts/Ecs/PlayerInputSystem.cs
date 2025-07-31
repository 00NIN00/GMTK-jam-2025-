using Leopotam.Ecs;
using UnityEngine;

namespace Ecs
{
    sealed class PlayerInputSystem : IEcsInitSystem, IEcsRunSystem  
    {
        private readonly EcsFilter<PlayerTag, DirectionComponent> _directionFilter = null;
        
        private GameInputSystem _gameInput;
        private IEcsInitSystem _ecsInitSystemImplementation;
        
        public void Init()
        {
            _gameInput = new GameInputSystem();
            _gameInput.Enable();
        }
        
        public void Run()
        {
            foreach (var i in _directionFilter)
            {
                ref var directionComponent = ref _directionFilter.Get2(i);
                ref var direction = ref directionComponent.direction;

                direction.x = Direction().x;
                direction.z = Direction().z;
            }
        }
        
        private Vector3 Direction()
        {
            Vector2 direction = _gameInput.Player.Move.ReadValue<Vector2>();
            return new Vector3(direction.x, 0, direction.y).normalized;
        }
    }
}