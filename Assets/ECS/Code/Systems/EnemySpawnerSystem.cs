using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using System.Collections.Generic;
using Scellecs.Morpeh.Providers;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(EnemySpawnerSystem))]
public sealed class EnemySpawnerSystem : Initializer
{
    [SerializeField] private EnemyData[] enemyDatas = new EnemyData[] { };
    [SerializeField] private float maxSpawnFromPlayerDistance;
    [SerializeField] private int maxEnemies;
    private Filter enemyFilter;
    private Entity player;

    public override void OnAwake()
    {
        enemyFilter = this.World.Filter.With<EnemyComponent>();
        player = this.World.Filter.With<PlayerComponent>().First();
        for (int i = 0; i < maxEnemies; i++)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        var newEnemy = this.World.CreateEntity();
        int r = Random.Range(0, enemyDatas.Length);
        var data = GetRandomEnemy();
        Vector3 pos = Helper.GetRandomPositionInCircle(player.GetComponent<TransformComponent>().position, player.GetComponent<PlayerComponent>().radius, maxSpawnFromPlayerDistance);
        GameObject enemyGameObject = Instantiate(data.prefab, pos, Quaternion.identity);
        newEnemy.SetComponent(new GameObjectComponent { gameObject = enemyGameObject });
        newEnemy.SetComponent(new TransformComponent { position = pos });
        newEnemy.SetComponent(new EnemyComponent { });
        newEnemy.SetComponent(new HealthComponent
        {
            destroyOnDeath = false,
            maxHealth = data.health,
            healthPoints = data.health,
            healthController = enemyGameObject.GetComponent<IHealthController>()
        });
    }

    private void RespawnEnemy(Entity entity)
    {
        Vector3 pos = Helper.GetRandomPositionInCircle(player.GetComponent<TransformComponent>().position, player.GetComponent<PlayerComponent>().radius, maxSpawnFromPlayerDistance);
        entity.GetComponent<HealthComponent>().ResetHealth();
        entity.GetComponent<TransformComponent>().position = pos;
        entity.GetComponent<GameObjectComponent>().PositionGameObject(pos);
    }

    private EnemyData GetRandomEnemy()
    {
        int r = Random.Range(0, enemyDatas.Length);
        return enemyDatas[r];
    }
}