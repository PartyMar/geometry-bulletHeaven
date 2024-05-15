using GBH;
using Unity.Entities;
using Unity.Transforms;

namespace GBH
{
    public readonly partial struct DamageEnemyAspect : IAspect
    {
        public readonly Entity entity;
        private readonly RefRW<LocalTransform> Transform;
        private readonly RefRO<DamageEnemyTag> _tag;
        private readonly RefRO<DamageValue> _damage;

    }
}