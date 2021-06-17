using TinyECS.Interfaces;
using TinyECSUnityIntegration.Interfaces;
using UnityEngine;

namespace SpaceShootingTrip.Systems
{
    public class SpawnHealingStarsSystem : IUpdateSystem
    {
        protected IWorldContext mWorldContext;
        protected GameObject mPrefab;
        protected IGameObjectFactory mFactory;

        private readonly float interval;
        private float timeToNext;

        public SpawnHealingStarsSystem(IWorldContext worldContext, GameObject prefab, IGameObjectFactory factory)
        {
            mWorldContext = worldContext;
            mPrefab = prefab;
            mFactory = factory;
            interval = 10f;
            timeToNext = UnityEngine.Random.Range(interval * 0.5f, interval) * Time.timeScale;
        }

        public void Update(float deltaTime)
        {
            timeToNext -= deltaTime;
            if (timeToNext <= 0)
            {
                timeToNext = UnityEngine.Random.Range(interval * 0.5f, interval) * Time.timeScale;
                mFactory.Spawn(mPrefab, new Vector3(UnityEngine.Random.Range(-2f, 2f), 5.5f, 0), Quaternion.identity, null);
            }
        }
    }
}
