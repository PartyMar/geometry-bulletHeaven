using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;


namespace GBH
{

    public partial struct RotatingSystem : ISystem
    {

        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<RotateSpeed>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            //foreach ((RefRW<LocalTransform> localTranform, RefRO<RotateSpeed> rotateSpeed)
            //    in SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotateSpeed>>())
            //{
            //    localTranform.ValueRW = localTranform.ValueRO.RotateY(rotateSpeed.ValueRO.value*SystemAPI.Time.DeltaTime);
            //}

            RotatingJob rotatingJob = new RotatingJob
            {
                deltaTime = SystemAPI.Time.DeltaTime
            };

            rotatingJob.Schedule();
        }


        [BurstCompile]
        public partial struct RotatingJob : IJobEntity
        {
            public float deltaTime;
            public void Execute(ref LocalTransform localTranform, ref RotateSpeed rotateSpeed)
            {
                if (rotateSpeed.Timer > rotateSpeed.Delay)
                {
                    localTranform = localTranform.RotateY(45);
                    rotateSpeed.Timer = 0;
                }
                else
                {
                    rotateSpeed.Timer += deltaTime;
                }

            }
        }
    }

}