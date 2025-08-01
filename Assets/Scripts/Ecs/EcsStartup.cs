using Ecs.Components;
using Ecs.Systems;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Ecs
{
    public class EcsStartup : MonoBehaviour
    {
        private EcsWorld _world;
        private EcsSystems _systems;

        private void Start()
        {
            _world =  new EcsWorld();
            _systems = new EcsSystems(_world);

            _systems.ConvertScene();
        
            AddInjections();
            AddOneFrames();
            AddSystems();
        
            _systems.Init();
        }

        private void AddInjections()
        {
        
        }

        private void AddSystems()
        {
            _systems
                .Add(new PlayerInputSystem())
                .Add(new MovementSystem())
                .Add(new LookSystem())
                .Add(new RaycastDeleteSystem());
        }

        private void AddOneFrames()
        {
        }

        private void Update()
        {
            _systems.Run();
        }

        private void OnDestroy()
        {
            if(_systems == null) return;
        
            _systems.Destroy();
            _systems = null;
        
            _world.Destroy();
            _world = null;
        }
    }
}