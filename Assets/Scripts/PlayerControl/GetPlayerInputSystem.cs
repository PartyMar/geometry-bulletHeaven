using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
public partial class GetPlayerInputSystem : SystemBase
{
    private MovementActions _movementActions;
    private Entity _playerEntity;

    protected override void OnCreate()
    {
        RequireForUpdate<PlayerTag>();
        RequireForUpdate<PlayerMoveInput>();
        RequireForUpdate<PlayerPosition>();

        _movementActions = new MovementActions();
    }

    protected override void OnStartRunning()
    {
        _movementActions.Enable();
        _playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
    }

    protected override void OnUpdate()
    {
        var curMoveInput = _movementActions.Map.PlayerMovement.ReadValue<Vector2>();
        SystemAPI.SetSingleton(new PlayerMoveInput { Value = curMoveInput });
        SystemAPI.SetSingleton(new PlayerPosition { Value = SystemAPI.GetComponent<LocalTransform>(_playerEntity).Position });
    }

    protected override void OnStopRunning()
    {
        _movementActions?.Disable();
        _playerEntity = Entity.Null;
    }
}
