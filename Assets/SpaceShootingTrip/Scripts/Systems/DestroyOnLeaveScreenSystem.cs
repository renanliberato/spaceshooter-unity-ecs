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
                var limit = entity.GetComponent<Components.DestroyOnLeaveScreenComponent>().limit;

                if (y > 5 * limit || y < -5 * limit)
                {
                    mWorldContext.DestroyEntity(entity.Id);
                }
            }
        }
    }
}
