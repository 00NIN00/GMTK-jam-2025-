using System;
using UnityEngine;

namespace Ecs
{
    [Serializable]
    public struct MovableComponent
    {
        public CharacterController characterController;
        public float speed;
    }
}