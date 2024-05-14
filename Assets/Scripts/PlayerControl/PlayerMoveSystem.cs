using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace GBH
{

    public partial struct PlayerMoveSystem : ISystem
    {

        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerMoveSpeed>();
            state.RequireForUpdate<PlayerMoveInput>();
        }


        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            PlayerMoveJob playerMoveJob = new PlayerMoveJob
            {
                DeltaTime = deltaTime
            };

            playerMoveJob.Schedule(state.Dependency).Complete();
        }
    }


    [BurstCompile]
    public partial struct PlayerMoveJob : IJobEntity
    {
        public float DeltaTime;

        private void Execute(
            ref LocalTransform transform,
            ref PhysicsVelocity velocity,
            in PlayerMoveInput moveInput,
            in PlayerMoveSpeed moveSpeed
            )
        {
            velocity.Linear.xz = moveInput.Value * moveSpeed.Value;

            if (math.lengthsq(moveInput.Value) > float.Epsilon)
            {
                float angle = math.atan2(moveInput.Value.y, moveInput.Value.x) * 57.2958f;
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