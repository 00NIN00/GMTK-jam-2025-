using System;
using UnityEngine;

namespace Ecs.Components
{
    [Serializable]
    public struct CameraComponent
    {
        public Transform cameraTransform;
        [Range(0, 10)] public float mouseSensitivity;
        [Range(1, 20)] public float smoothSpeed;
        public float minPitch;        
        public float maxPitch; 
        [HideInInspector] public Vector2 direction;
        [HideInInspector] public Vector2 targetDirection;
    }
}