using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using System.Collections.Generic;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(EnemySpawnerSystem))]
public sealed class EnemySpawnerSystem : UpdateSystem {
    [SerializeField] private EnemyData[] enemyDatas = new EnemyData[]{};
    [SerializeField] private float spawnerSize;
    [SerializeField] private int maxEnemies;
    private Filter filter;
    public override void OnAwake() {
        filter = this.World.Filter.With<EnemyComponent>();
    }

    public override void OnUpdate(float deltaTime) {
        if(filter.GetLengthSlow() < maxEnemies){
            SpawnEnemy();
        }
    }

    private void SpawnEnemy(){
        Debug.Log("Spawn Enemy");
        var newEnemy = this.World.CreateEntity();
        int r = Random.Range(0, enemyDatas.Length);
        var data = enemyDatas[r];

        Vector3 pos = new Vector3(Random.Range(-spawnerSize, spawnerSize), 0,Random.Range(-spawnerSize, spawnerSize));

        GameObject enemyGameObject = Instantiate(data.prefab, pos, Quaternion.identity);
        newEnemy.SetComponent(new GameObjectComponent{gameObject = enemyGameObject});
        newEnemy.SetComponent(new TransformComponent{position = pos});
        newEnemy.SetComponent(new EnemyComponent{});
        newEnemy.SetComponent(new HealthComponent{healthPoints = data.health, healthController = enemyGameObject.GetComponent<IHealthController>()});
    }
}