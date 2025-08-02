using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Components
{
    public class InteractableProvider : MonoBehaviour 
    {
        public EcsEntity Entity { get; private set; }
        private EcsWorld _world;

        public EcsStartup ecsStartup;

        void Start()
        {
            Invoke(nameof(sfada), 0.3f); // Задержка для инициализации мира
        }

        private void sfada()
        {
            _world = ecsStartup.World; // Получаем мир из EcsStartup
            if (_world == null)
            {
                Debug.LogError("EcsWorld is not initialized!");
                return;
            }

            // Создаём сущность вручную, чтобы избежать автоматической конверсии
            Entity = _world.NewEntity();
            Entity.Get<InteractableComponent>();
        }

        void OnDestroy()
        {
            if (Entity.IsAlive())
            {
                Entity.Destroy();
                Debug.Log($"Entity destroyed for {gameObject.name}");
            }
        }
    }
}