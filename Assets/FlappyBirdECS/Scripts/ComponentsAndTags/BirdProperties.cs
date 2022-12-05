using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Nam.FlappyECS
{
    public struct BirdProperties : IComponentData
    {
        public float FlappingForce;
    }
}

