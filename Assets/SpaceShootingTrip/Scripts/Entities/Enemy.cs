using TinyECS.Interfaces;
using SpaceShootingTrip.Components;
using UnityEngine;
using System;

namespace SpaceShootingTrip.Entities
{
    public class Enemy
    {
        public static void Initialize(IEntity linkedEntity, int level)
        {
            int health = 1 + (int)Math.Floor((level / 5.0));
            var shootIncrease = UnityEngine.Random.Range(1f, 3f) * (1 + level / 10);

            linkedEntity.AddComponent(new EnemyComponent { shipType = 1 });
            linkedEntity.AddComponent(new HealthComponent { current = health, max = health });
            linkedEntity.AddComponent(new PositionComponent { value = new Vector2(UnityEngine.Random.Range(-2f, 2f), UnityEngine.Random.Range(5f, 7.5f)) });
            linkedEntity.AddComponent(new TargetPositionComponent { position = new Vector2(UnityEngine.Random.Range(-2f, 2f), UnityEngine.Random.Range(1.5f, 3f)), step = UnityEngine.Random.Range(0.15f, 1f) });
            linkedEntity.AddComponent(new AutoShootComponent { interval = shootIncrease, timeToNextShoot = shootIncrease });
            linkedEntity.AddComponent(new RotationComponent { angle = 180 });
            linkedEntity.AddComponent(new StraightLineBulletShootingComponent { });
        }
    }
}
