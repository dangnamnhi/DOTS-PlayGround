using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Nam.Zomebies
{
    public readonly partial struct GraveyardAspect : IAspect
    {
        private const float BRAIN_RADIUS_SQ = 50;

        public readonly Entity Entity;

        private readonly TransformAspect _transformAspect;

        private readonly RefRO<GraveyardProperties> _graveyardProperties; //Read only
        private readonly RefRW<GraveyardRandom> _graveyardRandom; // Read write
        private readonly RefRW<ZombieSpawnPoint> _zombieSpawnPoint;
        private readonly RefRW<ZombieSpawnTimer> _zomebieSpawnTimer;

        public int NumberTombstoneToSpawn => _graveyardProperties.ValueRO.NumberTombstonesToSpawn;
        public Entity TombstonePrefab => _graveyardProperties.ValueRO.TombstonePrefab;

        public NativeArray<float3> ZombieSpawnPoints
        {
            get => _zombieSpawnPoint.ValueRO.Value;
            set => _zombieSpawnPoint.ValueRW.Value = value;
        }

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

        public UniformScaleTransform GetZombieSpawnPoint()
        {
            var position = GetRandomZombiePosition();
            var lookDirection = math.normalizesafe(_transformAspect.Position - position);
            return new UniformScaleTransform
            {
                Position = position,
                Rotation = quaternion.LookRotationSafe(lookDirection, math.up()),
                Scale = 1,
            };
        }
        private float3 GetRandomZombiePosition()
        {
            return ZombieSpawnPoints[_graveyardRandom.ValueRW.Value.NextInt(ZombieSpawnPoints.Length)];
        }
        private float3 HalfDimensions => new()
        {
            x = _graveyardProperties.ValueRO.FieldDimensions.x * 0.5f,
            y = 0,
            z = _graveyardProperties.ValueRO.FieldDimensions.y * 0.5f,
        };
        private float3 MinCorner => _transformAspect.Position - HalfDimensions;
        private float3 MaxCorner => _transformAspect.Position + HalfDimensions;

        public float2 GetRandomOffSet()
        {
            return _graveyardRandom.ValueRW.Value.NextFloat2();
        }
        public float ZombieSpawnTimer
        {
            get => _zomebieSpawnTimer.ValueRO.Value;
            set => _zomebieSpawnTimer.ValueRW.Value = value;
        }
        public bool TimeToSpawnZombie => ZombieSpawnTimer <= 0f;
        public float ZombieSpawnRate => _graveyardProperties.ValueRO.ZombieSpawnRate;
        public Entity ZombiePrefab => _graveyardProperties.ValueRO.ZombiePrefab;
        public void ResetTimer()
        {
            ZombieSpawnTimer = ZombieSpawnRate;
        }
    }
}

