using SpaceShootingTrip.Components;
using System.Collections.Generic;
using TinyECS.Impls;
using TinyECS.Interfaces;
using TinyECSUnityIntegration.Impls;
using UnityEngine;

namespace SpaceShootingTrip.Views
{
    public class BulletView : BaseDynamicView, IEventListener<TComponentChangedEvent<PositionComponent>>, IEventListener<TComponentChangedEvent<GameObjectTag>>, IEventListener<TComponentChangedEvent<TrailColor>>, IEventListener<TEntityDestroyedEvent>
    {
        public ParticleSystem particleSystem;

        private readonly IList<uint> _eventManagerSubscriptions = new List<uint>();

        public void OnEvent(TComponentChangedEvent<PositionComponent> eventData)
        {
            if (eventData.mOwnerId == LinkedEntityId)
                transform.position = eventData.mValue.value;
        }

        public void OnEvent(TComponentChangedEvent<GameObjectTag> eventData)
        {
            if (eventData.mOwnerId == LinkedEntityId)
                gameObject.tag = eventData.mValue.tag;
        }

        public void OnEvent(TComponentChangedEvent<TrailColor> eventData)
        {
            if (eventData.mOwnerId == LinkedEntityId)
            {
                var main = particleSystem.main;

                main.startColor = new ParticleSystem.MinMaxGradient
                {
                    color = eventData.mValue.color,
                };
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

            linkedEntity.AddComponent(new PositionComponent { value = new UnityEngine.Vector2(transform.position.x, transform.position.y) });
            linkedEntity.AddComponent(new DirectionalVelocityComponent { up = 3 });
            linkedEntity.AddComponent(new RotationComponent { angle = transform.rotation.eulerAngles.z });
            linkedEntity.AddComponent(new DestroyOnLeaveScreenComponent { limit = 1f });

            _eventManagerSubscriptions.Add(eventManager.Subscribe<TComponentChangedEvent<PositionComponent>>(this));
            _eventManagerSubscriptions.Add(eventManager.Subscribe<TComponentChangedEvent<GameObjectTag>>(this));
            _eventManagerSubscriptions.Add(eventManager.Subscribe<TComponentChangedEvent<TrailColor>>(this));
            _eventManagerSubscriptions.Add(eventManager.Subscribe<TEntityDestroyedEvent>(this));
        }

        public void OnDestroy()
        {
            foreach (var id in _eventManagerSubscriptions)
                _eventManager.Unsubscribe(id);
        }
    }
}
