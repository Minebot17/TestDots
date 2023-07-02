using Spawner.Scripts.Components;
using Unity.Entities;
using UnityEngine;

namespace Spawner.Scripts.Bakers
{
    public class SpawnerAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        public float SpawnRate;
    }
    
    public class SpawnerBaker : Baker<SpawnerAuthoring>
    {
        public override void Bake(SpawnerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new SpawnerComponent
            {
                Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                Position = authoring.transform.position,
                NextSpawnTime = 0f,
                SpawnRate = authoring.SpawnRate
            });
        }
    }
}