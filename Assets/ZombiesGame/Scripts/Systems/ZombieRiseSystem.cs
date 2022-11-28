using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Burst;
using Unity.Entities;

namespace Nam.Zomebies
{
    [BurstCompile]
    [UpdateAfter(typeof(SpawnZombieSystem))]
    public partial struct ZombieRiseSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            
        }
        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            
        }
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var delTime = SystemAPI.Time.DeltaTime;
            var ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            new ZombieRiseJob
            {
                Dt = delTime,
                ECB = ecb.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter(),
            }.ScheduleParallel();
        }
    }
    [BurstCompile]
    public partial struct ZombieRiseJob : IJobEntity
    {
        public float Dt;
        public EntityCommandBuffer.ParallelWriter ECB;
        [BurstCompile]
        private void Execute(ZombieRiseAspect zombie, [EntityInQueryIndex]int sortKey)
        {
            zombie.Rise(Dt);
            if(zombie.IsAboveGround)
            {
                ECB.RemoveComponent<ZombieRiseRate>(sortKey,zombie.Entity);
            }
        }

    }
}
