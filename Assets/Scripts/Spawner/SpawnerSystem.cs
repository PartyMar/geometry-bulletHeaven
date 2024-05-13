using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Collections.LowLevel.Unsafe;

[BurstCompile]
public partial struct SpawnerSystem : ISystem
{

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<EnemySpawner>();
    }

    public void OnDestroy(ref SystemState state) { }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer.ParallelWriter ecb = GetEntityCommandBuffer(ref state);
        var playerPos = SystemAPI.GetSingleton<PlayerPosition>().Value;

        var randomSeed = new Random((uint)UnityEngine.Random.Range(1, 100000));

        new ProcessSpawnerJob
        {
            ElapsedTime = SystemAPI.Time.ElapsedTime,
            Ecb = ecb,
            RandomSeed = randomSeed,
        }.ScheduleParallel();
    }

    private EntityCommandBuffer.ParallelWriter GetEntityCommandBuffer(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
        return ecb.AsParallelWriter();
    }
}

[BurstCompile]
public partial struct ProcessSpawnerJob : IJobEntity
{
    public EntityCommandBuffer.ParallelWriter Ecb;
    public double ElapsedTime;
    public float3 PlayerPos;
    public Random RandomSeed;

    [NativeSetThreadIndex] private int _threadId;

    private void Execute([ChunkIndexInQuery] int chunkIndex, ref EnemySpawner spawner)
    {
        if (spawner.NextSpawnTime < ElapsedTime)
        {
            var rnd = RandomSeed.NextInt(0, 5);
            for (var i = 0; i < spawner.Count; i++)
            {
                float3 spawnPos = new float3(0, 0.5f, 0);
                var r = 10;
                spawnPos.x = PlayerPos.x + r * math.cos(2 * math.PI * (i + rnd) / (spawner.Count + rnd));
                spawnPos.z = PlayerPos.z + r * math.sin(2 * math.PI * (i + rnd) / (spawner.Count + rnd));

                Entity newEntity = Ecb.Instantiate(chunkIndex, spawner.Prefab);
                Ecb.SetComponent(chunkIndex, newEntity, LocalTransform.FromPosition(spawnPos));
            }

            spawner.NextSpawnTime = (float)ElapsedTime + spawner.SpawnRate;
        }
    }
}