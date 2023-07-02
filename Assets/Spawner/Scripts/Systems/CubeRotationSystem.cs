using Spawner.Scripts.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

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
            new CubeRotationJob { DeltaTime = SystemAPI.Time.DeltaTime }.Schedule();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
    
    [BurstCompile]
    [WithAll(typeof(CubeTag), typeof(LocalTransform))]
    public partial struct CubeRotationJob : IJobEntity
    {
        public float DeltaTime;
        
        private void Execute([ChunkIndexInQuery] int chunkIndex, ref LocalTransform transform)
        {
            transform = transform.RotateY(DeltaTime * 5f);
        }
    }
}