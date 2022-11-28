using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace Nam.Zomebies
{
    [BurstCompile]
    public partial struct SpawnZombieSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GraveyardProperties>();
        }
        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            
        }
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecbSingleTon = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            new SpawnZombieJob
            {
                Dt = deltaTime,
                ECB = ecbSingleTon.CreateCommandBuffer(state.WorldUnmanaged),
            }.Run();
        }
    }
    [BurstCompile]
    [StructLayout(LayoutKind.Auto)]
    public partial struct SpawnZombieJob : IJobEntity
    {
        public float Dt;
        public EntityCommandBuffer ECB;
        [BurstCompile]
        private void Execute(GraveyardAspect graveyard)
        {
            graveyard.ZombieSpawnTimer -= Dt;
            if (!graveyard.TimeToSpawnZombie) return;
            if(graveyard.ZombieSpawnPoints.Length == 0) return;
            graveyard.ResetTimer();
            var newZombie = ECB.Instantiate(graveyard.ZombiePrefab);
            var newZombieTransform = graveyard.GetZombieSpawnPoint();
            ECB.SetComponent(newZombie,new LocalToWorldTransform { Value = newZombieTransform });
        }
    }
}
