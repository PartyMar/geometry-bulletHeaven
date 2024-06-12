using GBH;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;

namespace GBH
{
    [BurstCompile]
    public partial struct DamageEnemySystem : ISystem
    {

        public void OnCreate(ref SystemState state)
        {

        }

        public void OnDestroy(ref SystemState state)
        {

        }

        [BurstCompile]
        private void OnUpdate(ref SystemState state)
        {

            foreach (var zone in SystemAPI.Query<DamageEnemyAspect>())
            {
                RefRW<LocalTransform> entTransform = SystemAPI.GetComponentRW<LocalTransform>(zone.entity);

                PhysicsWorldSingleton physicsWorld = SystemAPI.GetSingleton<PhysicsWorldSingleton>();
                NativeList<ColliderCastHit> hits = new NativeList<ColliderCastHit>(Allocator.Temp);

            }

        }
    }


}