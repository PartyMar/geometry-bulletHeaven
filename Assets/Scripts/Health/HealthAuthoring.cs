using Unity.Entities;
using UnityEngine;


namespace GBH
{
    public class HealthAuthoring : MonoBehaviour
    {
        public float healthCurrent;
        public float healthMax;

        private class Baker : Baker<HealthAuthoring>
        {
            public override void Bake(HealthAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new Health
                {
                    ValueCurrent = authoring.healthCurrent,
                    ValueMax = authoring.healthMax,
                });

                AddBuffer<DamageBufferElement>(entity);
            }
        }
    }


    public struct Health : IComponentData
    {
        public float ValueCurrent;
        public float ValueMax;
    }

    public struct DamageBufferElement: IBufferElementData
    {
        public float Value;
    }
}