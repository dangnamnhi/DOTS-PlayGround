using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Nam.Zomebies
{
    public readonly partial struct ZombieRiseAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly TransformAspect _transformAspect;
        private readonly RefRO<ZombieRiseRate> _zombieRiseRate;

        public void Rise(float deltaTime)
        {
            _transformAspect.Position += math.up() * deltaTime;
            if(IsAboveGround) SnapOnGround();
        }
        public bool IsAboveGround => _transformAspect.Position.y >= 0f;
        public void SnapOnGround()
        {
            var pos = _transformAspect.Position;
            pos.y = 0;
            _transformAspect.Position = pos;
        }
    }
}
