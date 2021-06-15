using SpaceShootingTrip.Components;
using System;
using TinyECS.Interfaces;
using TinyECSUnityIntegration.Interfaces;
using UnityEngine;

namespace SpaceShootingTrip.Systems
{
    public class SpawnEnemiesSystem : IUpdateSystem
    {
        protected IWorldContext mWorldContext;
        protected GameObject mPrefab;
        protected IGameObjectFactory mFactory;

        private float _timeUntilNext;
        private float _interval;
        private int _numberOfEnemies;

        public SpawnEnemiesSystem(IWorldContext worldContext, GameObject prefab, IGameObjectFactory factory)
        {
            mWorldContext = worldContext;
            mPrefab = prefab;
            mFactory = factory;
            _interval = 2.5f;
            _numberOfEnemies = 3;
        }

        public void Update(float deltaTime)
        {
            var levelEntity = mWorldContext.GetSingleEntityWithAll(typeof(MatchLevelComponent));

            if (levelEntity == null)
                return;

            var level = levelEntity.GetComponent<MatchLevelComponent>();
            var timeToNext = level.timeToNextLevel;
            var interval = level.levelTimeInterval;
            var levelNumber = level.level;

            timeToNext -= deltaTime;
            if (timeToNext <= 0 && mWorldContext.GetEntitiesWithAll(typeof(EnemyComponent)).Count == 0)
            {
                timeToNext = (interval *= 1.05f);
                _numberOfEnemies = Math.Min(50, (levelNumber += 1));

                for (var i = 0; i < _numberOfEnemies; i++)
                {
                    mFactory.Spawn(mPrefab, Vector2.zero, Quaternion.Euler(0, 0, 180), null);
                }

                var player = mWorldContext.GetSingleEntityWithAll(typeof(PlayerComponent));
                var shootComponent = player.GetComponent<AutoShootComponent>();

                player.AddComponent(new AutoShootComponent { interval = shootComponent.interval * 0.8f, timeToNextShoot = shootComponent.interval * 0.8f });
            }

            if (mWorldContext.GetSingleEntityWithAll(typeof(MatchStepComponent)).GetComponent<MatchStepComponent>().step == MatchSteps.InMatch)
                Time.timeScale = Mathf.Lerp(Time.timeScale, 10, 0.00001f);

            levelEntity.AddComponent(new MatchLevelComponent { level = levelNumber, levelTimeInterval = interval, timeToNextLevel = timeToNext });
        }
    }
}
