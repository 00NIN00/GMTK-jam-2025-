using Leopotam.Ecs;
using UnityEngine;
using Ecs.Providers;
using Ecs.Components;

namespace Ecs.Systems
{
    sealed class RaycastInteractionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTag, CameraComponent> _filter = null;
        private readonly EcsFilter<InteractableComponent> _interactedFilter = null;

        private InteractableComponent _oldInteractableComponent;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var cameraComponent = ref _filter.Get2(i);

                Ray ray = cameraComponent.camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    InteractableProvider provider = hit.collider.gameObject.GetComponent<InteractableProvider>();

                    if (provider != null && IsProviderEntityInFilter(provider))
                    {
                        var interactableComponent = provider.Entity.Get<InteractableComponent>();

                        interactableComponent.isInteract = true;

                        Debug.Log($"{interactableComponent.isInteract}__{interactableComponent.isInteracted}");

                        if (Input.GetMouseButtonDown(0))
                        {
                            interactableComponent.isInteracted = true;
                            Debug.Log($"{interactableComponent.isInteract}__{interactableComponent.isInteracted}");
                        }
                    }
                }
            }
                
        }

        // ����������: ���� entity, ��������� � �����������, � ��������� ������� � �������
        private bool IsProviderEntityInFilter(InteractableProvider provider)
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
                var filterProvider = provider.GetComponent<InteractableProvider>();

                if (filterProvider == provider)
                {
                    return true;
                }
            }
            return false;
        }
    }

}