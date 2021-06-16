using SpaceShootingTrip.Components;
using SpaceShootingTrip.Entities;
using System.Collections.Generic;
using TinyECS.Impls;
using TinyECS.Interfaces;
using TinyECSUnityIntegration.Impls;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShootingTrip.Views
{
    public class PlayerView : BaseStaticView, IEventListener<TComponentChangedEvent<PositionComponent>>, IEventListener<TComponentChangedEvent<HealthComponent>>, IEventListener<TEntityDestroyedEvent>
    {
        public Image health1Image;
        public Image health2Image;
        public Image health3Image;

        public Button moveLeftButton;
        public Button moveRightButton;

        public Color bulletTrailColor;

        private readonly IList<uint> _eventManagerSubscriptions = new List<uint>();

        public void OnEvent(TComponentChangedEvent<PositionComponent> eventData)
        {
            if (eventData.mOwnerId == LinkedEntityId)
                transform.position = eventData.mValue.value;
        }

        public void OnEvent(TComponentChangedEvent<HealthComponent> eventData)
        {
            if (eventData.mOwnerId == LinkedEntityId)
            {
                health1Image.enabled = eventData.mValue.current >= 1;
                health2Image.enabled = eventData.mValue.current >= 2;
                health3Image.enabled = eventData.mValue.current >= 3;
            }
        }
        public void OnEvent(TEntityDestroyedEvent eventData)
        {
            if (eventData.mEntityId == LinkedEntityId)
                Destroy(this.gameObject);
        }


        public override void RegisterSubscriptions(IEventManager eventManager, EntityId entityId)
        {
            IEntity linkedEntity = mWorldContext.GetEntityById(entityId);

            Player.Initialize(linkedEntity, transform.position, bulletTrailColor);

            _eventManagerSubscriptions.Add(eventManager.Subscribe<TEntityDestroyedEvent>(this));
            _eventManagerSubscriptions.Add(eventManager.Subscribe<TComponentChangedEvent<PositionComponent>>(this));
            _eventManagerSubscriptions.Add(eventManager.Subscribe<TComponentChangedEvent<HealthComponent>>(this));
        }

        public void StartMovingLeft()
        {
            var player = mWorldContext.GetEntityById(LinkedEntityId);

            if (player == null)
                return;

            player.AddComponent(new PlayerControlsComponent { moveLeft = true });
        }

        public void StartMovingRight()
        {
            var player = mWorldContext.GetEntityById(LinkedEntityId);

            if (player == null)
                return;

            player.AddComponent(new PlayerControlsComponent { moveRight = true });
        }

        public void StopMoving()
        {
            var player = mWorldContext.GetEntityById(LinkedEntityId);

            if (player == null)
                return;

            player.AddComponent(new PlayerControlsComponent { });
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.gameObject.CompareTag("Enemy"))
            {
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("EnemyBullet"))
            {
                var player = mWorldContext.GetEntityById(LinkedEntityId);
                
                if (player != null)
                    player.AddComponent<CollidedWithBulletComponent>();

                Destroy(collision.gameObject);
            }
        }

        private void OnDestroy()
        {
            foreach (var id in _eventManagerSubscriptions)
                _eventManager.Unsubscribe(id);
        }
    }
}
