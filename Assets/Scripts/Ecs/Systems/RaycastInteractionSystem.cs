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

        // Исправлено: ищем entity, связанный с провайдером, и проверяем наличие в фильтре
        private bool IsProviderEntityInFilter(InteractableProvider provider)
        {
            // Попробуем найти entity по компоненту InteractableComponent
            for (int i = 0; i < _interactedFilter.GetEntitiesCount(); i++)
            {
                var entity = _interactedFilter.GetEntity(i);
                // Обычно провайдер хранит ссылку на entity, но если нет, сравниваем по GameObject
                // или по значению компонента, если это возможно
                // Здесь предполагается, что провайдер и entity связаны по GameObject
                // Можно добавить поле public EcsEntity Entity в InteractableComponentProvider для прямого доступа
                // Но если его нет, сравниваем по GameObject
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