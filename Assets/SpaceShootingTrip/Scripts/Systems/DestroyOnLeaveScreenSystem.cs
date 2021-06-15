using System;
using TinyECS.Interfaces;

namespace SpaceShootingTrip.Systems
{
    public class DestroyOnLeaveScreenSystem : IUpdateSystem
    {
        protected IWorldContext mWorldContext;

        public DestroyOnLeaveScreenSystem(IWorldContext worldContext)
        {
            mWorldContext = worldContext;
        }

        public void Update(float deltaTime)
        {
            var entityIds = mWorldContext
                .GetEntitiesWithAll(typeof(Components.PositionComponent), typeof(Components.DestroyOnLeaveScreenComponent));

            for (var i = 0; i < entityIds.Count; i++)
            {
                var entity = mWorldContext.GetEntityById(entityIds[i]);
                var y = entity.GetComponent<Components.PositionComponent>().value.y;
                var goingUp = Math.Sign(entity.GetComponent<Components.VelocityComponent>().value.y) > 0;
                if ((goingUp && y > 10) || y < -10)
                {
                    mWorldContext.DestroyEntity(entity.Id);
                }
            }
        }
    }
}
