using Ecs.Components;
using Leopotam.Ecs;
using Leopotam.EcsLite;
using UnityEngine;
using EcsWorld = Leopotam.Ecs.EcsWorld;
using IEcsInitSystem = Leopotam.Ecs.IEcsInitSystem;
using IEcsRunSystem = Leopotam.Ecs.IEcsRunSystem;

namespace Ecs.Systems
{
    sealed class RaycastDeleteSystem : IEcsInitSystem, IEcsRunSystem {
        private readonly EcsWorld _world = null;
        
        private readonly EcsFilter<Deletable> _deletableFilter = null;
        private EcsComponentPool<Deletable> _deletablePool;
        private Camera _mainCamera;

        public void Init() {
            _deletablePool = _world.GetPool<Deletable>();
            _mainCamera = Camera.main;
            Debug.Log(_deletablePool);
        }

        public void Run() {
            // Проверяем клик левой кнопкой мыши
            if (!Input.GetMouseButtonDown(0)) return;
            Debug.Log("click");
            // Создаем рейкаст от позиции мыши
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Проверяем попадание рейкаста
            if (Physics.Raycast(ray, out hit)) {
                Debug.Log("hit");
                // Ищем объект среди сущностей
                foreach (int entity in _deletableFilter) {
                    var deletable = _deletablePool.GetItem(entity);
                    if (deletable.gameObject == hit.collider.gameObject) {
                        Debug.Log(hit.collider.gameObject.name);
                        // Удаляем GameObject из Unity
                        Object.Destroy(deletable.gameObject);
                        // Удаляем сущность из ECS
                        ref var _entity = ref _deletableFilter.GetEntity(entity);
                        _entity.Destroy();
                        Debug.Log($"Объект {deletable.gameObject.name} удален");
                        break;
                    }
                }
            }
        }
    }
}