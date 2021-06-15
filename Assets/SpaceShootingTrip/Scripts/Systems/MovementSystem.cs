using TinyECS.Interfaces;
using UnityEngine;

namespace SpaceShootingTrip.Systems
{
    public class MovementSystem : IUpdateSystem
    {
        protected IWorldContext mWorldContext;

        public MovementSystem(IWorldContext worldContext)
        {
            mWorldContext = worldContext;
        }

        public void Update(float deltaTime)
        {
            var ids = mWorldContext
                .GetEntitiesWithAll(typeof(Components.PositionComponent), typeof(Components.VelocityComponent));

            for (var i = 0; i < ids.Count; i++)
            {
                var entity = mWorldContext.GetEntityById(ids[i]);

                var curPosition = entity.GetComponent<Components.PositionComponent>().value;
                var velocity = entity.GetComponent<Components.VelocityComponent>().value;

                var next = new Vector2(curPosition.x, curPosition.y) + (velocity * deltaTime);

                entity.AddComponent(new Components.PositionComponent
                {
                    value = new Vector3(next.x, next.y, curPosition.z)
                });
            }
        }
    }
}
