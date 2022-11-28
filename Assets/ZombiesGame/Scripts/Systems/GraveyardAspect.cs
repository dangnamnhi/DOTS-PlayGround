using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Nam.Zomebies
{
    public readonly partial struct GraveyardAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly TransformAspect _transformAspect;

        private readonly RefRO<GraveyardProperties> _graveyardProperties; //Read only
        private readonly RefRW<GraveyardRandom> _graveyardRandom; // Read write

        public int NumberTombstoneToSpawn => _graveyardProperties.ValueRO.NumberTombstonesToSpawn;
        public Entity TombstonePrefab => _graveyardProperties.ValueRO.TombstonePrefab;

        public UniformScaleTransform GetRandomTombstoneTransform()
        {
            return new UniformScaleTransform
            {
                Position = GetRandomPosition(),
                Rotation = GetRandomRotaion(),
                Scale = GetRandomScale(0.3f),
            };
        }
        private float3 GetRandomPosition()
        {
            float3 randomPosition;
            do
            {
                randomPosition = _graveyardRandom.ValueRW.Value.NextFloat3(MinCorner, MaxCorner);
            }
            while (math.distancesq(_transformAspect.Position, randomPosition) <= BRAIN_RADIUS_SQ);
            return randomPosition;
        }
        private quaternion GetRandomRotaion() => quaternion.RotateY(_graveyardRandom.ValueRW.Value.NextFloat(-0.25f, 0.25f));
        private float GetRandomScale(float min) => _graveyardRandom.ValueRW.Value.NextFloat(min, 1f);
        private float3 HalfDimensions => new()
        {
            x = _graveyardProperties.ValueRO.FieldDimensions.x * 0.5f,
            y = 0,
            z = _graveyardProperties.ValueRO.FieldDimensions.y * 0.5f,
        };
        private float3 MinCorner => _transformAspect.Position - HalfDimensions;
        private float3 MaxCorner => _transformAspect.Position + HalfDimensions;

        private const float BRAIN_RADIUS_SQ = 50; 
    }
}

