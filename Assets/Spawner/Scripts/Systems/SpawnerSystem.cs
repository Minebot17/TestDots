using Spawner.Scripts.Components;
using Unity.Burst;
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
            state.RequireForUpdate<RandomData>();
        }
        
        public void OnDestroy(ref SystemState state) { }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var rnd = SystemAPI.GetSingletonRW<RandomData>();
            foreach (var spawner in SystemAPI.Query<RefRW<SpawnerComponent>>())
            {
                ProcessSpawner(ref state, spawner, rnd);
            }
        }

        private void ProcessSpawner(ref SystemState state, RefRW<SpawnerComponent> spawner, RefRW<RandomData> rnd)
        {
            if (spawner.ValueRO.NextSpawnTime < SystemAPI.Time.ElapsedTime)
            {
                var randomOffset = new float3(rnd.ValueRW.Random.NextFloat(-5, 5), rnd.ValueRW.Random.NextFloat(-5, 5), rnd.ValueRW.Random.NextFloat(-5, 5));
                var newEntity = state.EntityManager.Instantiate(spawner.ValueRO.Prefab);
                state.EntityManager.SetComponentData(newEntity, 
                    LocalTransform.FromPosition(spawner.ValueRO.Position + randomOffset));
                spawner.ValueRW.NextSpawnTime = (float) SystemAPI.Time.ElapsedTime + spawner.ValueRO.SpawnRate;
            }
        }
    }
}