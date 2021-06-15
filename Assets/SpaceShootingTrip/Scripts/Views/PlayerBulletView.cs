﻿using SpaceShootingTrip.Components;
using System.Collections.Generic;
using TinyECS.Impls;
using TinyECS.Interfaces;
using TinyECSUnityIntegration.Impls;
using UnityEngine;

namespace SpaceShootingTrip.Views
{
    public class PlayerBulletView : BaseDynamicView, IEventListener<TComponentChangedEvent<PositionComponent>>, IEventListener<TEntityDestroyedEvent>
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
                Destroy(this.gameObject);
        }

        public override void RegisterSubscriptions(IEventManager eventManager, EntityId entityId)
        {
            IEntity linkedEntity = mWorldContext.GetEntityById(entityId);

            linkedEntity.AddComponent(new PositionComponent { value = new UnityEngine.Vector2(1, 5) });
            linkedEntity.AddComponent(new CollidesWithComponent { component = typeof(EnemyComponent) });
            linkedEntity.AddComponent(new VelocityComponent { value = new Vector2(0, 7) });
            linkedEntity.AddComponent(new DestroyOnLeaveScreenComponent { });

            _eventManagerSubscriptions.Add(eventManager.Subscribe<TComponentChangedEvent<PositionComponent>>(this));
            _eventManagerSubscriptions.Add(eventManager.Subscribe<TEntityDestroyedEvent>(this));
        }

        public void OnDestroy()
        {
            foreach (var id in _eventManagerSubscriptions)
                _eventManager.Unsubscribe(id);
        }
    }
}
