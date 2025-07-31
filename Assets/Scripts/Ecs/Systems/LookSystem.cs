using Ecs.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Systems
{
    sealed class LookSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTag, ModelComponent, CameraComponent> _filter = null;

        private Quaternion _startTransformRotation;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var model = ref _filter.Get2(i);
                ref var camera = ref _filter.Get3(i);

                if (camera.cameraTransform == null || model.modelTransform == null)
                    continue;

                camera.direction = Vector2.Lerp(camera.direction, camera.targetDirection, 
                    camera.smoothSpeed * Time.deltaTime);
                
                model.modelTransform.rotation = Quaternion.Euler(0, camera.direction.x, 0);
                camera.cameraTransform.localRotation = Quaternion.Euler(camera.direction.y, 0, 0);
            }
        }

    }
}