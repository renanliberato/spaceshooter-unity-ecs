using SpaceShootingTrip.Components;
using System.Collections.Generic;
using TinyECS.Interfaces;

namespace SpaceShootingTrip.Systems
{
    public class DecreaseHealthOnBulletCollisionSystem : IReactiveSystem
    {
        protected IWorldContext mWorldContext;

        public DecreaseHealthOnBulletCollisionSystem(IWorldContext worldContext)
        {
            mWorldContext = worldContext;
        }

        public bool Filter(IEntity entity)
        {
            return entity.HasComponent<HealthComponent>();
        }

        public void Update(List<IEntity> entities, float deltaTime)
        {
            foreach (var e in entities)
            {
                if (e.HasComponent<CollidedWithBulletComponent>())
                {
                    var curHealth = e.GetComponent<HealthComponent>();

                    e.AddComponent(new HealthComponent
                    {
                        current = curHealth.current - 1,
                        max = curHealth.max
                    });
                    e.RemoveComponent<CollidedWithBulletComponent>();
                }
            }
        }
    }
}
