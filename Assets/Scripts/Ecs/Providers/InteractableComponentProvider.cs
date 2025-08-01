using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Ecs.Components
{
    public class InteractableComponentProvider : MonoBehaviour 
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

            Debug.Log($"InteractableProvider Awake on {gameObject.name}");
            _world = ecsStartup.World; // Получаем мир из EcsStartup
            if (_world == null)
            {
                Debug.LogError("EcsWorld is not initialized!");
                return;
            }

            // Создаём сущность вручную, чтобы избежать автоматической конверсии
            Entity = _world.NewEntity();
            Entity.Get<InteractableComponent>();
            Debug.Log($"Entity created for {gameObject.name}, Entity ID: {Entity.GetInternalId()}");
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