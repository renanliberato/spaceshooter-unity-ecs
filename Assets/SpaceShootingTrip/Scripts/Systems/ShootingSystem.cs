using SpaceShootingTrip.Components;
using System.Collections.Generic;
using TinyECS.Interfaces;
using TinyECSUnityIntegration.Interfaces;
using UnityEngine;

namespace SpaceShootingTrip.Systems
{
    public class ShootingSystem : IReactiveSystem
    {
        protected IWorldContext mWorldContext;
        protected GameObject mStraightLineBulletPrefab;
        protected GameObject mBulletPrefab;
        protected IGameObjectFactory mFactory;

        public ShootingSystem(IWorldContext worldContext, GameObject bulletPrefab, IGameObjectFactory factory)
        {
            mWorldContext = worldContext;
            mBulletPrefab = bulletPrefab;
            mFactory = factory;
        }

        public bool Filter(IEntity entity)
        {
            return entity.HasComponent<ShootingComponent>()
                && (
                    entity.HasComponent<StraightLineBulletShootingComponent>()
                    || entity.HasComponent<SparseDirectionalBulletShootingComponent>()
                )
                && entity.HasComponent<BulletGameObjectTag>();
        }

        public void Update(List<IEntity> entities, float deltaTime)
        {
            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];
                var angle = entity.GetComponent<RotationComponent>().angle;

                Quaternion rotation = Quaternion.Euler(0, 0, angle);

                if (entity.HasComponent<SparseDirectionalBulletShootingComponent>())
                    rotation = Quaternion.Euler(0, 0, angle + UnityEngine.Random.Range(-15f, 15f));

                var bullet = mFactory.Spawn(mBulletPrefab, entity.GetComponent<PositionComponent>().value, rotation, null);

                bullet.AddComponent(new GameObjectTag { tag = entity.GetComponent<BulletGameObjectTag>().tag });
                bullet.AddComponent(new TrailColor { color = entity.GetComponent<BulletTrailColor>().color });

                entity.RemoveComponent<ShootingComponent>();
            }
        }
    }
}
