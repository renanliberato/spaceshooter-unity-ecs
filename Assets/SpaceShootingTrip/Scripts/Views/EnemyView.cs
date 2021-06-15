using SpaceShootingTrip.Components;
using SpaceShootingTrip.Entities;
using System.Collections.Generic;
using TinyECS.Impls;
using TinyECS.Interfaces;
using TinyECSUnityIntegration.Impls;
using UnityEngine;

namespace SpaceShootingTrip.Views
{
    public class EnemyView : BaseDynamicView, IEventListener<TComponentChangedEvent<PositionComponent>>, IEventListener<TEntityDestroyedEvent>
    {
        private readonly IList<uint> _eventManagerSubscriptions = new List<uint>();

        public void OnEvent(TComponentChangedEvent<PositionComponent> eventData)
        {
            if (eventData.mOwnerId == LinkedEntityId)
                transform.position = eventData.mValue.value;
        }

        public void OnEvent(TEntityDestroyedEvent eventData)
        {
            if (eventData.mEntityId == LinkedEntityId)
            {
                Destroy(this.gameObject);
            }
        }

        public override void RegisterSubscriptions(IEventManager eventManager, EntityId entityId)
        {
            IEntity linkedEntity = mWorldContext.GetEntityById(entityId);

            Enemy.Initialize(linkedEntity);

            _eventManagerSubscriptions.Add(eventManager.Subscribe<TComponentChangedEvent<PositionComponent>>(this));
            _eventManagerSubscriptions.Add(eventManager.Subscribe<TEntityDestroyedEvent>(this));
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("PlayerBullet"))
            {
                var player = mWorldContext.GetEntityById(LinkedEntityId);

                if (player != null)
                    player.AddComponent<CollidedWithBulletComponent>();

                Destroy(collision.gameObject);
            }
        }

        public void OnDestroy()
        {
            foreach (var id in _eventManagerSubscriptions)
                _eventManager.Unsubscribe(id);
        }
    }
}
