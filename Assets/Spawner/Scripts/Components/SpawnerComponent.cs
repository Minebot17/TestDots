using Unity.Entities;
using Unity.Mathematics;

namespace Spawner.Scripts.Components
{
    public struct SpawnerComponent : IComponentData
    {
        public Entity Prefab;
        public float3 Position;
        public float NextSpawnTime;
        public float SpawnRate;
    }
}