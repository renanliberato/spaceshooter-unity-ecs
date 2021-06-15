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
        private int _level;
        private float _nextTimeToIncreaseNumberOfEnemies;
        private float _timeToIncreaseNumberOfEnemiesInterval;

        public SpawnEnemiesSystem(IWorldContext worldContext, GameObject prefab, IGameObjectFactory factory)
        {
            mWorldContext = worldContext;
            mPrefab = prefab;
            mFactory = factory;
            _interval = 2.5f;
            _numberOfEnemies = 1;
            _level = 1;
            _timeToIncreaseNumberOfEnemiesInterval = _nextTimeToIncreaseNumberOfEnemies = 10f;
        }

        public void Update(float deltaTime)
        {
            _nextTimeToIncreaseNumberOfEnemies -= deltaTime;
            if (_nextTimeToIncreaseNumberOfEnemies <= 0)
            {
                _nextTimeToIncreaseNumberOfEnemies = (_timeToIncreaseNumberOfEnemiesInterval *= 1.3f);
                _numberOfEnemies += (_level += 1);
            }
            
            _timeUntilNext -= deltaTime;
            if (_timeUntilNext <= 0)
            {
                _timeUntilNext = (_interval *= 0.95f);

                for (var i = 0; i < _numberOfEnemies; i++)
                {
                    mFactory.Spawn(mPrefab, Vector2.zero, Quaternion.Euler(0,0,180), null);
                }
            }
        }
    }
}
