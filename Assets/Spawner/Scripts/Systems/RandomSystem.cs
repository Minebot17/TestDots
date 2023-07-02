using Spawner.Scripts.Components;
using Unity.Burst;
using Unity.Entities;

namespace Spawner.Scripts.Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct RandomSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.EntityManager.CreateSingleton(new RandomData((uint) UnityEngine.Random.Range(0, int.MaxValue - 1)));
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            
        }
    }
}