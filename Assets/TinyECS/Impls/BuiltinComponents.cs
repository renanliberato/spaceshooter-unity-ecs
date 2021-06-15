using TinyECS.Interfaces;


namespace TinyECS.Impls
{
    /// <summary>
    /// class TDisposableComponent
    /// 
    /// The classure is a component-flag that makes an entity on to which it is assigned disposable one
    /// </summary>

    public class TDisposableComponent: IComponent { }


    /// <summary>
    /// class TEntityLifetimeComponent
    /// 
    /// This classure should be used in tie with TDisposableComponent to keep entity for a few extra frames
    /// </summary>

    public class TEntityLifetimeComponent: IComponent
    {
        public uint mCounter; ///< A number of frames to live
    }
}
