using Spawner.Scripts.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Spawner.Scripts.Systems
{
    public partial struct CubeRotationSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (transform, _) in SystemAPI.Query<RefRW<LocalTransform>, CubeTag>())
            {
                transform.ValueRW = transform.ValueRW.RotateY(SystemAPI.Time.DeltaTime * 5f);
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}