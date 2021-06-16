using TinyECS.Interfaces;
using UnityEngine;

namespace SpaceShootingTrip.Systems
{
    public class DirectionalMovementSystem : IUpdateSystem
    {
        protected IWorldContext mWorldContext;

        public DirectionalMovementSystem(IWorldContext worldContext)
        {
            mWorldContext = worldContext;
        }

        public void Update(float deltaTime)
        {
            var ids = mWorldContext
                .GetEntitiesWithAll(typeof(Components.PositionComponent), typeof(Components.DirectionalVelocityComponent), typeof(Components.RotationComponent));

            for (var i = 0; i < ids.Count; i++)
            {
                var entity = mWorldContext.GetEntityById(ids[i]);

                var curPosition = entity.GetComponent<Components.PositionComponent>().value;
                var velocity = entity.GetComponent<Components.DirectionalVelocityComponent>();
                var rotation = entity.GetComponent<Components.RotationComponent>();

                var d = Quaternion.Euler(0, 0, rotation.angle) * new Vector3(0, velocity.up, 0) * deltaTime;

                entity.AddComponent(new Components.PositionComponent
                {
                    value = curPosition + d
                });
            }
        }
    }
}
