using TinyECS.Interfaces;

namespace SpaceShootingTrip.Components
{
    public struct AutoShootComponent : IComponent
    {
        public float interval;
        public float timeToNextShoot;
    }
}