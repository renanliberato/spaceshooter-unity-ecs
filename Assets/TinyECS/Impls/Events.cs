using System;
using TinyECS.Interfaces;


namespace TinyECS.Impls
{
    public class TNewEntityCreatedEvent: IEvent
    {
        public EntityId mEntityId;
    }

    public class TEntityDestroyedEvent: IEvent
    {
        public EntityId mEntityId;

        public string   mEntityName;
    }

    public class TNewComponentAddedEvent: IEvent
    {
        public EntityId mOwnerId;

        public Type     mComponentType;
    }


    public class TComponentChangedEvent<T>: IEvent
    {
        public EntityId mOwnerId;

        public T        mValue;
    }


    public class TComponentRemovedEvent: IEvent
    {
        public EntityId mOwnerId;

        public Type     mComponentType;
    }
}
