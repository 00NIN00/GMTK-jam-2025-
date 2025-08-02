using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Components
{
    sealed class InderactDetectedSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<InteractableComponent> _interactableFilter = null;

        public void Run()
        {

            foreach (var i in _interactableFilter)
            {
                // �������� �������� �� �������
                ref var interactable = ref _interactableFilter.Get1(i);

                // ���������, ��� ������ � �������� ����������
                if (interactable.renderer == null || interactable.renderer.material == null)
                {
                    Debug.LogWarning($"Interactable {i} has no Renderer or Material!");
                    continue; // ���������� ���������� ��������
                }

                Debug.Log($"Interactable: {interactable.isInteract}, Interacted: {interactable.isInteracted}");

                if (interactable.isInteracted)
                {
                    interactable.renderer.material.color = interactable.interactedColor;
                    Debug.Log($"Interactable color changed to {interactable.interectColor} for entity {i}");
                }
                if (interactable.isInteract)
                {
                    interactable.renderer.material.color = interactable.interectColor;
                    Debug.Log($"Interactable color changed to {interactable.interectColor} for entity {i}");
                }
                
            }
        }
    }
}