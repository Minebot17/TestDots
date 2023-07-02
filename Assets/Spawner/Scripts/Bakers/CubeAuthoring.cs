using Spawner.Scripts.Components;
using Unity.Entities;
using UnityEngine;

namespace Spawner.Scripts.Bakers
{
    public class CubeAuthoring : MonoBehaviour
    {
        
    }
    
    public class CubeBaker : Baker<CubeAuthoring>
    {
        public override void Bake(CubeAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<CubeTag>(entity);
        }
    }
}