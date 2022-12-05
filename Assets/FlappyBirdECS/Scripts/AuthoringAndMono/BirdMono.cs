using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Nam.FlappyECS
{
    public class BirdMono : MonoBehaviour
    {
        [SerializeField] SpriteRenderer _birdRenderer;
        [SerializeField] float _birdFlapForce = 10f;

        public float FlappingForce => _birdFlapForce;
    }

    public class BirdBaker : Baker<BirdMono>
    {
        public override void Bake(BirdMono authoring)
        {
            AddComponent(new BirdProperties
            {
                FlappingForce = authoring.FlappingForce,
            });
        }
    }
}

