using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;


namespace GBH
{

    public partial struct EnemyMoveSystem : ISystem
    {

        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EnemyTag>();
            state.RequireForUpdate<EnemyMoveSpeed>();
        }


        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var playerPos = SystemAPI.GetSingleton<PlayerPosition>().Value;

            EnemyMoveJob enemyMoveJob = new EnemyMoveJob
            {
                DeltaTime = deltaTime,
                PlayerPos = playerPos
            };

            enemyMoveJob.Schedule();
        }
    }


    [BurstCompile]
    public partial struct EnemyMoveJob : IJobEntity
    {
        public float DeltaTime;
        public float3 PlayerPos;

        private void Execute(
            ref LocalTransform transform,
            ref PhysicsVelocity velocity,
            in EnemyMoveSpeed moveSpeed
            )
        {
            var vector3 = PlayerPos - transform.Position;
            vector3 = math.normalize(vector3);
            float2 vector2 = new float2(vector3.x, vector3.z);

            velocity.Linear.xz = vector2 * moveSpeed.Value;


            if (math.lengthsq(vector2) > float.Epsilon)
            {

                float angle = math.atan2(vector2.y, vector2.x) * 57.2958f;
                float quantizedAngle = math.round(angle / 45) * 45;
                float quantizedAngleRad = quantizedAngle / 57.2958f;

                float newX = math.cos(quantizedAngleRad);
                float newY = math.sin(quantizedAngleRad);


                var forward = new float3(newX, 0f, newY);
                transform.Rotation = quaternion.LookRotation(forward, math.up());
            }
        }
    }

}