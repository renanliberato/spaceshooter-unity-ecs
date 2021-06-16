using TinyECS.Interfaces;
using SpaceShootingTrip.Components;
using UnityEngine;
using SpaceShootingTrip.Enums;

namespace SpaceShootingTrip.Entities
{
    public class Player
    {
        public static ShootingType ShootingType = ShootingType.StraightLine;

        public static void Initialize(IEntity linkedEntity, Vector2 initialPosition, Color bulletTrailColor)
        {
            linkedEntity.AddComponent(new PlayerComponent { shipType = 1 });
            linkedEntity.AddComponent(new PlayerControlsComponent { moveLeft = false, moveRight = false });
            linkedEntity.AddComponent(new HealthComponent { current = 3, max = 3 });
            linkedEntity.AddComponent(new PositionComponent { value = initialPosition });
            linkedEntity.AddComponent(new RotationComponent { angle = 0 });
            linkedEntity.AddComponent(new VelocityComponent { value = new Vector2() });
            linkedEntity.AddComponent(new BulletGameObjectTag { tag = "PlayerBullet" });
            linkedEntity.AddComponent(new BulletTrailColor { color = bulletTrailColor });

            switch (ShootingType)
            {
                case ShootingType.StraightLine:
                    linkedEntity.AddComponent(new AutoShootComponent { interval = 0.6f, timeToNextShoot = 0.6f });
                    linkedEntity.AddComponent(new StraightLineBulletShootingComponent { });
                    break;
                case ShootingType.SparseDirectional:
                    linkedEntity.AddComponent(new AutoShootComponent { interval = 0.35f, timeToNextShoot = 0.35f });
                    linkedEntity.AddComponent(new SparseDirectionalBulletShootingComponent { });
                    break;
            }
        }
    }
}
