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
            Invoke(nameof(sfada), 0.3f); // �������� ��� ������������� ����
        }

        private void sfada()
        {
            _world = ecsStartup.World; // �������� ��� �� EcsStartup
            if (_world == null)
            {
                Debug.LogError("EcsWorld is not initialized!");
                return;
            }

            // ������ �������� �������, ����� �������� �������������� ���������
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