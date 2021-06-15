using TinyECS.Interfaces;
using SpaceShootingTrip.Components;
using UnityEngine;

namespace SpaceShootingTrip.Entities
{
    public class Enemy
    {
        public static void Initialize(IEntity linkedEntity)
        {
            linkedEntity.AddComponent(new EnemyComponent { shipType = 1 });
            linkedEntity.AddComponent(new HealthComponent { current = 1, max = 1 });
            linkedEntity.AddComponent(new PositionComponent { value = new UnityEngine.Vector2(UnityEngine.Random.Range(-2f, 2f), Random.Range(5f, 9.5f)) });
            linkedEntity.AddComponent(new TargetPositionComponent { position = new UnityEngine.Vector2(UnityEngine.Random.Range(-2f, 2f), Random.Range(1.5f, 3f)), step = Random.Range(0.15f, 1f) });
            linkedEntity.AddComponent(new AutoShootComponent { interval = UnityEngine.Random.Range(1f, 3f), timeToNextShoot = UnityEngine.Random.Range(0f, 2f) });
            linkedEntity.AddComponent(new DestroyOnLeaveScreenComponent { limit = 2f });
        }
    }
}
