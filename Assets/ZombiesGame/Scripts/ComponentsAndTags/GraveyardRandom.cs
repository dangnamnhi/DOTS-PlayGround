using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;

namespace Nam.Zomebies
{
    public struct GraveyardRandom : IComponentData
    {
        public Random Value;
    }
}
