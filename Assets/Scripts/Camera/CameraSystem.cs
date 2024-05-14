using System.Diagnostics;
using Unity.Entities;
using Unity.Mathematics;

namespace GBH
{

    public partial class CameraSystem : SystemBase
    {

        protected override void OnUpdate()
        {
            float3 playerPos = SystemAPI.GetSingleton<PlayerPosition>().Value;

            float3 cameraPos = new float3(playerPos.x, playerPos.y + 7.5f, playerPos.z - 3.5f);

            CameraSingleton.instance.transform.position = cameraPos;
        }
    }

}