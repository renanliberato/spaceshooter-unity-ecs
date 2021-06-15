using SpaceShootingTrip.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyECS.Impls;
using TinyECS.Interfaces;
using TinyECSUnityIntegration.Impls;
using UnityEngine;

namespace SpaceShootingTrip
{
    public class Controller : MonoBehaviour
    {
        public GameObject enemyPrefab;
        public GameObject enemyBulletPrefab;
        public GameObject playerBulletPrefab;

        protected IWorldContext mWorldContext;

        protected ISystemManager mSystemManager;

        private void Awake()
        {
            mWorldContext = new WorldContextFactory().CreateNewWorldInstance();

            mSystemManager = new SystemManager(mWorldContext);

            WorldContextsManagerUtils.CreateWorldContextManager(mWorldContext, "WorldContextManager_System");
            SystemManagerObserverUtils.CreateSystemManagerObserver(mSystemManager, "SystemManagerObserver_System");
            // register our systems here
            var goFactory = new GameObjectFactory(mWorldContext);
            mSystemManager.RegisterSystem(new RegisterViewSystem(mWorldContext));
            mSystemManager.RegisterSystem(new PlayerInputSystem(mWorldContext));
            mSystemManager.RegisterSystem(new SpawnEnemiesSystem(mWorldContext, enemyPrefab, goFactory));
            mSystemManager.RegisterSystem(new MovementSystem(mWorldContext));
            mSystemManager.RegisterSystem(new TargetMovementSystem(mWorldContext));
            mSystemManager.RegisterSystem(new EnemyAutoShootSystem(mWorldContext, enemyBulletPrefab, goFactory));
            mSystemManager.RegisterSystem(new PlayerAutoShootSystem(mWorldContext, playerBulletPrefab, goFactory));
            mSystemManager.RegisterSystem(new DecreaseHealthOnBulletCollisionSystem(mWorldContext));
            mSystemManager.RegisterSystem(new DestroyOnLeaveScreenSystem(mWorldContext));
            mSystemManager.RegisterSystem(new DestroyOnHealthZeroSystem(mWorldContext));

            mSystemManager.Init();
        }

        private void Update()
        {
            mSystemManager.Update(Time.deltaTime);
        }
    }
}
