using UnityEngine;
using Unity.Entities;

namespace GBH
{

    public class RotateSpeedAuthoring : MonoBehaviour
    {
        public float value;
        public float delay;

        private class Baker : Baker<RotateSpeedAuthoring>
        {
            public override void Bake(RotateSpeedAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new RotateSpeed { Timer = 0, Delay = authoring.delay, Value = authoring.value, });
            }
        }
    }

    public struct RotateSpeed : IComponentData
    {
        public float Timer;
        public float Delay;
        public float Value;
    }

}