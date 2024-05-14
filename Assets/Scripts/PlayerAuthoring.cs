using Unity.Entities;
using UnityEngine;


namespace GBH
{

    public class PlayerAuthoring : MonoBehaviour
    {

        private class Baker : Baker<PlayerAuthoring>
        {
            public override void Bake(PlayerAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<PlayerTag>(entity);
            }
        }
    }

    public struct PlayerTag : IComponentData { }

}