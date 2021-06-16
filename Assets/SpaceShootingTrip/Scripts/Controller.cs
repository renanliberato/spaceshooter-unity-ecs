using SpaceShootingTrip.Systems;
using TinyECS.Impls;
using TinyECS.Interfaces;
using TinyECSUnityIntegration.Impls;
using UnityEngine;

namespace SpaceShootingTrip
{
    public class Controller : MonoBehaviour
    {
        public GameObject enemyPrefab;
        public GameObject bulletPrefab;

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
            mSystemManager.RegisterSystem(new DirectionalMovementSystem(mWorldContext));
            mSystemManager.RegisterSystem(new TargetMovementSystem(mWorldContext));
            mSystemManager.RegisterSystem(new AutoShootSystem(mWorldContext));
            mSystemManager.RegisterSystem(new ShootingSystem(mWorldContext, bulletPrefab, goFactory));
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
