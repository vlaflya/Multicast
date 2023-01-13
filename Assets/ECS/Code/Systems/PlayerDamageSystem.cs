using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using System.Linq;
using System.Collections.Generic;
using System;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlayerDamageSystem))]
public sealed class PlayerDamageSystem : UpdateSystem
{
    [SerializeField] private int maxEnemiesAttack;
    private Filter filter;
    private Entity player;

    public override void OnAwake()
    {
        filter = this.World.Filter.With<EnemyComponent>().With<HealthComponent>();
        player = this.World.Filter.With<PlayerComponent>().First();
    }

    public override void OnUpdate(float deltaTime)
    {
        var comparer = new EnemyToPlayerDistanceComparer();
        var playerComponent = player.GetComponent<PlayerComponent>();
        var playerTransformComponent = player.GetComponent<TransformComponent>();
        comparer.playerPos = playerTransformComponent.position;
        var distanceFilter = this.filter.Where(entity =>
        {
            var enemyTransform = entity.GetComponent<TransformComponent>();
            var distance = Vector3.Distance(enemyTransform.position, playerTransformComponent.position);
            return distance < playerComponent.radius;
        });
        var sortedFilter = distanceFilter.OrderBy(entity => entity, comparer);
        var counter = 0;
        foreach (var entity in sortedFilter)
        {
            entity.GetComponent<HealthComponent>().healthPoints -= deltaTime * playerComponent.dps;
            counter++;
            if (counter > maxEnemiesAttack)
                break;
        }
    }

    class EnemyToPlayerDistanceComparer : IComparer<Entity>
    {
        public Vector3 playerPos;
        public int Compare(Entity x, Entity y)
        {
            Vector3 a = playerPos - x.GetComponent<TransformComponent>().position;
            Vector3 b = playerPos - y.GetComponent<TransformComponent>().position;
            return Comparer<float>.Default.Compare(a.sqrMagnitude, b.sqrMagnitude);
        }
    }
}