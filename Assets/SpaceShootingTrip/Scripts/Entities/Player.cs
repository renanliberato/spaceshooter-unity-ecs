using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyECS.Interfaces;
using SpaceShootingTrip.Components;
using UnityEngine;

namespace SpaceShootingTrip.Entities
{
    public class Player
    {
        public static void Initialize(IEntity linkedEntity, Vector2 initialPosition)
        {
            linkedEntity.AddComponent(new PlayerComponent { shipType = 1 });
            linkedEntity.AddComponent(new PlayerControlsComponent { moveLeft = false, moveRight = false });
            linkedEntity.AddComponent(new HealthComponent { current = 3, max = 3 });
            linkedEntity.AddComponent(new PositionComponent { value = initialPosition });
            linkedEntity.AddComponent(new CollidesWithComponent { component = typeof(EnemyComponent) });
            linkedEntity.AddComponent(new VelocityComponent { value = new Vector2() });
            linkedEntity.AddComponent(new AutoShootComponent { interval = 0.5f, timeToNextShoot = 0.5f });
        }
    }
}
