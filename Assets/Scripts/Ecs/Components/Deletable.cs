using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ecs.Components
{
    [Serializable]
    public struct Deletable
    {
        public GameObject gameObject;
    }
}