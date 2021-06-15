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
            linkedEntity.AddComponent(new PositionComponent { value = new UnityEngine.Vector2(UnityEngine.Random.Range(-2, 2), Random.Range(5, 8)) });
            linkedEntity.AddComponent(new VelocityComponent { value = new Vector2(0, UnityEngine.Random.Range(-3, -1)) });
            linkedEntity.AddComponent(new AutoShootComponent { interval = UnityEngine.Random.Range(1, 3), timeToNextShoot = UnityEngine.Random.Range(0, 2) });
            linkedEntity.AddComponent(new DestroyOnLeaveScreenComponent { });
        }
    }
}
