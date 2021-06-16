using SpaceShootingTrip.Components;
using TinyECS.Interfaces;

namespace SpaceShootingTrip.Systems
{
    public class AutoShootSystem : IUpdateSystem
    {
        protected IWorldContext mWorldContext;

        public AutoShootSystem(IWorldContext worldContext)
        {
            mWorldContext = worldContext;
        }

        public void Update(float deltaTime)
        {
            var ids = mWorldContext.GetEntitiesWithAll(typeof(AutoShootComponent));

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
