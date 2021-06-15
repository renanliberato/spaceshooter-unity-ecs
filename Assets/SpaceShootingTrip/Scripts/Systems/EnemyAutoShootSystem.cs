using SpaceShootingTrip.Components;
using TinyECS.Interfaces;
using TinyECSUnityIntegration.Interfaces;
using UnityEngine;

namespace SpaceShootingTrip.Systems
{
    public class EnemyAutoShootSystem : IUpdateSystem
    {
        protected IWorldContext mWorldContext;
        protected GameObject mPrefab;
        protected IGameObjectFactory mFactory;

        public EnemyAutoShootSystem(IWorldContext worldContext, GameObject prefab, IGameObjectFactory factory)
        {
            mWorldContext = worldContext;
            mPrefab = prefab;
            mFactory = factory;
        }

        public void Update(float deltaTime)
        {
            var ids = mWorldContext.GetEntitiesWithAll(typeof(EnemyComponent), typeof(AutoShootComponent));

            IEntity entity;

            for (var i = 0; i < ids.Count; i++)
            {
                entity = mWorldContext.GetEntityById(ids[i]);
                var shoot = entity.GetComponent<AutoShootComponent>();
                if (shoot.timeToNextShoot <= 0)
                {
                    var bullet = mFactory.Spawn(mPrefab, Vector2.zero, Quaternion.Euler(0, 0, 180), null);

                    bullet.AddComponent(new PositionComponent { value = entity.GetComponent<PositionComponent>().value + new Vector3(0, -0.3f, 0) });
                    entity.AddComponent(new AutoShootComponent { interval = shoot.interval, timeToNextShoot = shoot.interval });

                    return;
                };

                entity.AddComponent(new AutoShootComponent { interval = shoot.interval, timeToNextShoot = shoot.timeToNextShoot - deltaTime });
            }
        }
    }
}
