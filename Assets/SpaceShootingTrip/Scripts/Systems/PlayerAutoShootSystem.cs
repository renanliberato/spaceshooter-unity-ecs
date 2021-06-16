using SpaceShootingTrip.Components;
using System;
using TinyECS.Interfaces;
using TinyECSUnityIntegration.Interfaces;
using UnityEngine;

namespace SpaceShootingTrip.Systems
{
    public class PlayerAutoShootSystem : IUpdateSystem
    {
        protected IWorldContext mWorldContext;

        public PlayerAutoShootSystem(IWorldContext worldContext)
        {
            mWorldContext = worldContext;
        }

        public void Update(float deltaTime)
        {
            var ids = mWorldContext.GetEntitiesWithAll(typeof(PlayerComponent), typeof(AutoShootComponent));

            IEntity entity;

            for (var i = 0; i < ids.Count; i++)
            {
                entity = mWorldContext.GetEntityById(ids[i]);
                var shoot = entity.GetComponent<AutoShootComponent>();
                if (shoot.timeToNextShoot <= 0)
                {
                    entity.AddComponent(new ShootingComponent { });

                    entity.AddComponent(new AutoShootComponent { interval = shoot.interval, timeToNextShoot = shoot.interval });

                    return;
                };

                entity.AddComponent(new AutoShootComponent { interval = shoot.interval, timeToNextShoot = shoot.timeToNextShoot - deltaTime });
            }
        }
    }
}
