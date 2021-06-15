using TinyECS.Interfaces;
using UnityEngine;

namespace SpaceShootingTrip.Systems
{
    public class TargetMovementSystem : IUpdateSystem
    {
        protected IWorldContext mWorldContext;

        public TargetMovementSystem(IWorldContext worldContext)
        {
            mWorldContext = worldContext;
        }

        public void Update(float deltaTime)
        {
            var ids = mWorldContext
                .GetEntitiesWithAll(typeof(Components.PositionComponent), typeof(Components.TargetPositionComponent));

            for (var i = 0; i < ids.Count; i++)
            {
                var entity = mWorldContext.GetEntityById(ids[i]);

                var curPosition = entity.GetComponent<Components.PositionComponent>().value;
                var targetPosition = entity.GetComponent<Components.TargetPositionComponent>();

                entity.AddComponent(new Components.PositionComponent
                {
                    value = Vector3.MoveTowards(curPosition, targetPosition.position, targetPosition.step * deltaTime)
                });
            }
        }
    }
}
