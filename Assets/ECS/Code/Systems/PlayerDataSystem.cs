using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlayerDataSystem))]
public sealed class PlayerDataSystem : Initializer
{
    public PlayerData startPlayerData;
    private Entity playerEntity;
    public override void OnAwake()
    {
        playerEntity = this.World.CreateEntity();


        GameObject playerGameObject = Instantiate(startPlayerData.prefab);
        var model = playerGameObject.GetComponent<PlayerModel>();
        model.speed = startPlayerData.startSpeed;
        model.SetCallbacks(UpdatePlayerPosition);

        var player = new PlayerComponent
        {
            dps = startPlayerData.startDps,
            radius = startPlayerData.startRadius,
            controller = playerGameObject.GetComponent<PlayerController>()
        };
        playerEntity.SetComponent(player);
        playerEntity.SetComponent(new TransformComponent{position = Vector3.zero});
    }

    public void UpdatePlayerPosition(Vector3 position)
    {
        playerEntity.GetComponent<TransformComponent>().position = position;
        Debug.Log("Player position Updated " + playerEntity);
    }
}