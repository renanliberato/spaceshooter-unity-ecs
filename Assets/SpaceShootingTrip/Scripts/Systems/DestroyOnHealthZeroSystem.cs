using SpaceShootingTrip.Components;
using System.Collections.Generic;
using TinyECS.Interfaces;

namespace SpaceShootingTrip.Systems
{
    public class DestroyOnHealthZeroSystem : IReactiveSystem
    {
        protected IWorldContext mWorldContext;

        public DestroyOnHealthZeroSystem(IWorldContext worldContext)
        {
            mWorldContext = worldContext;
        }

        public bool Filter(IEntity entity)
        {
            return entity.HasComponent<HealthComponent>() && entity.GetComponent<HealthComponent>().current == 0;
        }

        public void Update(List<IEntity> entities, float deltaTime)
        {
            foreach (var e in entities)
            {
                if (e.HasComponent<PlayerComponent>())
                {
                    mWorldContext.GetSingleEntityWithAll(typeof(MatchStepComponent)).AddComponent(new MatchStepComponent { step = MatchSteps.End });
                }

                mWorldContext.DestroyEntity(e.Id);
            }
        }
    }
}
