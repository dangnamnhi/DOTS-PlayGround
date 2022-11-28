using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
namespace Nam.Zomebies
{
    public struct GraveyardProperties : IComponentData
    {
        public float2 FieldDimensions;
        public int NumberTombstonesToSpawn;
        public Entity TombstonePrefab;
        public Entity ZombiePrefab;
        public float ZombieSpawnRate;
    }

    public struct ZombieSpawnTimer : IComponentData
    {
        public float Value;
    }
}

