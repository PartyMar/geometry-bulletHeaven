using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

class SpawnerAuthoring : MonoBehaviour
{
    public bool Active;
    public GameObject Prefab;
    public float SpawnRate;
    public int Count;

    class SpawnerBaker : Baker<SpawnerAuthoring>
    {
        public override void Bake(SpawnerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new EnemySpawner
            {
                Active = authoring.Active,
                Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                SpawnPosition = authoring.transform.position,
                NextSpawnTime = authoring.SpawnRate,
                SpawnRate = authoring.SpawnRate,
                Count = authoring.Count,
            });
        }
    }
}



public struct EnemySpawner : IComponentData
{
    public bool Active;
    public Entity Prefab;
    public float3 SpawnPosition;
    public float NextSpawnTime;
    public float SpawnRate;
    public int Count;
}