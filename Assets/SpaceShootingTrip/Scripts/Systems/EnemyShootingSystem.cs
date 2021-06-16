using SpaceShootingTrip.Components;
using System.Collections.Generic;
using TinyECS.Interfaces;
using TinyECSUnityIntegration.Interfaces;
using UnityEngine;

namespace SpaceShootingTrip.Systems
{
    public class EnemyShootingSystem : IReactiveSystem
    {
        protected IWorldContext mWorldContext;
        protected GameObject mStraightLineBulletPrefab;
        protected GameObject mDirectionalBulletPrefab;
        protected IGameObjectFactory mFactory;

        public EnemyShootingSystem(IWorldContext worldContext, GameObject directionalBulletPrefab, GameObject straightLineBulletPrefab, IGameObjectFactory factory)
        {
            mWorldContext = worldContext;
            mDirectionalBulletPrefab = directionalBulletPrefab;
            mStraightLineBulletPrefab = straightLineBulletPrefab;
            mFactory = factory;
        }

        public bool Filter(IEntity entity)
        {
            return entity.HasComponent<EnemyComponent>() && entity.HasComponent<ShootingComponent>() && (entity.HasComponent<StraightLineBulletShootingComponent>() || entity.HasComponent<SparseDirectionalBulletShootingComponent>());
        }

        public void Update(List<IEntity> entities, float deltaTime)
        {
            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];
                var angle = entity.GetComponent<RotationComponent>().angle;

                if (entity.HasComponent<SparseDirectionalBulletShootingComponent>())
                    mFactory.Spawn(mDirectionalBulletPrefab, entity.GetComponent<PositionComponent>().value, Quaternion.Euler(0, 0, angle), null);

                if (entity.HasComponent<StraightLineBulletShootingComponent>())
                    mFactory.Spawn(mStraightLineBulletPrefab, entity.GetComponent<PositionComponent>().value, Quaternion.Euler(0, 0, angle), null);

                entity.RemoveComponent<ShootingComponent>();
            }
        }
    }
}
