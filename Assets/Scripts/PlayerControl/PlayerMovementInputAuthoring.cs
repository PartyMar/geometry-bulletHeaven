using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace GBH
{

    public class PlayerMoveInputAuthoring : MonoBehaviour
    {
        public float MoveSpeed;

        private class Baker : Baker<PlayerMoveInputAuthoring>
        {
            public override void Bake(PlayerMoveInputAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<PlayerMoveInput>(entity);
                AddComponent<PlayerPosition>(entity);

                AddComponent(entity, new PlayerMoveSpeed { Value = authoring.MoveSpeed });

            }
        }
    }

    public struct PlayerMoveInput : IComponentData
    {
        public float2 Value;
    }

    public struct PlayerMoveSpeed : IComponentData
    {
        public float Value;
    }

    public struct PlayerPosition : IComponentData
    {
        public float3 Value;
    }


}