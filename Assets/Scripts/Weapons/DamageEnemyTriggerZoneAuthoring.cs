using Unity.Entities;
using UnityEngine;


namespace GBH
{
    public class DamageEnemyTriggerZoneAuthoring : MonoBehaviour
    {
        public float damageValue;

        private class Baker : Baker<DamageEnemyTriggerZoneAuthoring>
        {
            public override void Bake(DamageEnemyTriggerZoneAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent<DamageEnemyTag>(entity);
                AddComponent(entity, new DamageValue
                {
                    Value = authoring.damageValue,
                });
            }
        }
    }


    public struct DamageEnemyTag : IComponentData
    {

    }

    public struct DamageValue : IComponentData
    {
        public float Value;
    }

}