using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
namespace Nam.FlappyECS
{
    public readonly partial struct BirdAspect : IAspect
    {
        public readonly Entity Entity;
        private readonly TransformAspect _transformAspect;
        private readonly RefRO<BirdProperties> _birdPropertiesReadOnly;
    }
}

