using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(EnemyRespawnSystem))]
public sealed class EnemyRespawnSystem : UpdateSystem
{
    [SerializeField] private float maxRespawFromPlayerDistance;
    private Filter enemyFilter;
    private Entity player;

    public override void OnAwake()
    {
        enemyFilter = this.World.Filter.With<EnemyComponent>();
        player = this.World.Filter.With<PlayerComponent>().First();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var entity in enemyFilter)
        {
            ref var healthComponent = ref entity.GetComponent<HealthComponent>();
            if (healthComponent.healthPoints <= 0)
            {
                RespawnEnemy(entity);
            }
        }
    }

    private void RespawnEnemy(Entity entity)
    {
        Vector3 pos = Helper.GetRandomPositionInCircle(player.GetComponent<TransformComponent>().position, player.GetComponent<PlayerComponent>().radius, maxRespawFromPlayerDistance);
        entity.GetComponent<HealthComponent>().ResetHealth();
        entity.GetComponent<TransformComponent>().position = pos;
        entity.GetComponent<GameObjectComponent>().PositionGameObject(pos);
    }
}