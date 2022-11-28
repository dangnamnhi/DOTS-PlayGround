using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace Nam.Zomebies
{
    public struct ZombieSpawnPoint : IComponentData
    {
        public NativeArray<float3> Value;
    }
}
