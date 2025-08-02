using System;
using UnityEngine;

namespace Ecs.Components
{
    [Serializable]
    public struct InteractableComponent 
    {
        public Color interectColor;
        public Color interactedColor;

        public Renderer renderer;

        public bool isInteract;
        public bool isInteracted;
    }
}