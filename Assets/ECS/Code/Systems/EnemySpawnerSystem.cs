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
public sealed class EnemySpawnerSystem : UpdateSystem {
    [SerializeField] private EnemyData[] enemyDatas = new EnemyData[]{};
    [SerializeField] private float maxSpawnFromPlayerDistance;
    [SerializeField] private int maxEnemies;
    private Filter filter;
    private Entity player;
    public override void OnAwake() {
        filter = this.World.Filter.With<EnemyComponent>();
        player = this.World.Filter.With<PlayerComponent>().First();
    }

    public override void OnUpdate(float deltaTime) {
        if(filter.GetLengthSlow() < maxEnemies){
            SpawnEnemy();
        }
    }

    private void SpawnEnemy(){
        var newEnemy = this.World.CreateEntity();
        int r = Random.Range(0, enemyDatas.Length);
        var data = GetRandomEnemy();

        
        Vector3 pos = CreateStartPosition(player.GetComponent<TransformComponent>().position, player.GetComponent<PlayerComponent>().radius);
        GameObject enemyGameObject = Instantiate(data.prefab, pos, Quaternion.identity);
        newEnemy.SetComponent(new GameObjectComponent{gameObject = enemyGameObject});
        newEnemy.SetComponent(new TransformComponent{position = pos});
        newEnemy.SetComponent(new EnemyComponent{});
        newEnemy.SetComponent(new HealthComponent{healthPoints = data.health, healthController = enemyGameObject.GetComponent<IHealthController>()});
    }

    private Vector3 CreateStartPosition(Vector3 playerPosition, float playerRadius){
        float angle = Mathf.Deg2Rad * Random.Range(0, 360);
        float distance = Random.Range(0, maxSpawnFromPlayerDistance) + playerRadius;
        Vector3 pos = playerPosition + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * distance;
        return pos;
    }

    private EnemyData GetRandomEnemy(){
        int r = Random.Range(0, enemyDatas.Length);
        return enemyDatas[r];
    }
}