using SpaceShootingTrip.Components;
using TinyECS.Interfaces;

namespace SpaceShootingTrip.Systems
{
    public class EnemyAutoShootSystem : IUpdateSystem
    {
        protected IWorldContext mWorldContext;

        public EnemyAutoShootSystem(IWorldContext worldContext)
        {
            mWorldContext = worldContext;
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
                    //var bullet = mFactory.Spawn(mPrefab, Vector2.zero, Quaternion.Euler(0, 0, 180), null);

                    //bullet.AddComponent(new PositionComponent { value = entity.GetComponent<PositionComponent>().value + new Vector3(0, -0.3f, 0) });

                    entity.AddComponent(new ShootingComponent { });

                    entity.AddComponent(new AutoShootComponent { interval = shoot.interval, timeToNextShoot = shoot.interval });

                    return;
                };

                entity.AddComponent(new AutoShootComponent { interval = shoot.interval, timeToNextShoot = shoot.timeToNextShoot - deltaTime });
            }
        }
    }
}
