using Leopotam.Ecs;
using UnityEngine;
using Ecs.Providers;
using Ecs.Components;

namespace Ecs.Systems
{
    sealed class RaycastInteractionSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTag, CameraComponent> _filter = null;
        private readonly EcsFilter<InteractableComponent> _interactedFilter = null; //InteractableComponent
        //private Camera _camera;

        public void Init()
        {
            //    Debug.Log("RaycastInteractionSystem initialized.");

            //    foreach (var entity in _interactableFilter)
            //    {
            //        ref var cameraComponent = ref _interactableFilter.Get3(entity);
            //        ref _camera = ref cameraComponent.camera;
            //        Debug.Log($"Camera found: {camera.name}");
            //    }
        }

        public void Run()
        {
            Debug.Log($"Processing entity");

            foreach (var i in _filter)
            {
                Debug.Log($"1111111111111111111111111111111111111");

                ref var cameraComponent = ref _filter.Get2(i);
                Debug.Log($"Camera found: {cameraComponent.camera.name}");

                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log($"Mouse button pressed, raycasting...");
                    Ray ray = cameraComponent.camera.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        Debug.Log($"Raycast hit: {hit.collider.gameObject.name}");

                        InteractableComponentProvider provider = hit.collider.gameObject.GetComponent<InteractableComponentProvider>();

                        Debug.Log($"Provider found: {provider != null}");

                        if (provider != null && IsProviderEntityInFilter(provider))
                        {
                            Debug.Log("�������������� � ��������!");
                        }
                    }
                }
            }
                
        }

        // ����������: ���� entity, ��������� � �����������, � ��������� ������� � �������
        private bool IsProviderEntityInFilter(InteractableComponentProvider provider)
        {
            // ��������� ����� entity �� ���������� InteractableComponent
            for (int i = 0; i < _interactedFilter.GetEntitiesCount(); i++)
            {
                var entity = _interactedFilter.GetEntity(i);
                // ������ ��������� ������ ������ �� entity, �� ���� ���, ���������� �� GameObject
                // ��� �� �������� ����������, ���� ��� ��������
                // ����� ��������������, ��� ��������� � entity ������� �� GameObject
                // ����� �������� ���� public EcsEntity Entity � InteractableComponentProvider ��� ������� �������
                // �� ���� ��� ���, ���������� �� GameObject
                var filterProvider = provider.GetComponent<InteractableComponentProvider>();

                if (filterProvider == provider)
                {
                    return true;
                }
            }
            return false;
        }
    }

}