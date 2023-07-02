using Spawner.Scripts.Components;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Spawner.Scripts.Systems
{
    [BurstCompile]
    public partial struct SpawnerSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<RandomData>();
        }
        
        public void OnDestroy(ref SystemState state) { }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();

            new SpawnerJob
            {
                Ecb = ecb,
                ElapsedTime = SystemAPI.Time.ElapsedTime,
                Rnd = SystemAPI.GetSingletonRW<RandomData>()
            }.Schedule();
        }
    }
    
    [BurstCompile]
    public partial struct SpawnerJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter Ecb;
        public double ElapsedTime;
        [NativeDisableUnsafePtrRestriction] public RefRW<RandomData> Rnd;

        private void Execute([ChunkIndexInQuery] int chunkIndex, ref SpawnerComponent spawner)
        {
            if (spawner.NextSpawnTime < ElapsedTime)
            {
                var randomOffset = new float3(Rnd.ValueRW.Random.NextFloat(-5, 5), Rnd.ValueRW.Random.NextFloat(-5, 5), Rnd.ValueRW.Random.NextFloat(-5, 5));
                var newEntity = Ecb.Instantiate(chunkIndex, spawner.Prefab);
                Ecb.SetComponent(chunkIndex, newEntity, 
                    LocalTransform.FromPosition(spawner.Position + randomOffset));
                spawner.NextSpawnTime = (float) ElapsedTime + spawner.SpawnRate;
            }
        }
    }
}