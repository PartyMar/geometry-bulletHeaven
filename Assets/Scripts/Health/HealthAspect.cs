using Unity.Entities;
using Unity.Transforms;

namespace GBH
{
    public readonly partial struct HealthAspect : IAspect
    {
        public readonly Entity entity;
        private readonly RefRW<LocalTransform> Transform;
        private readonly RefRW<Health> _health;
        private readonly DynamicBuffer<DamageBufferElement> _damageBuffer;

        public void ApplyDamage()
        {
            foreach (var damageElement in _damageBuffer)
            {
                _health.ValueRW.ValueCurrent -= damageElement.Value;
            }
            _damageBuffer.Clear();
        }
    }
}