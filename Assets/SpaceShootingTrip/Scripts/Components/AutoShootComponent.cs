using TinyECS.Interfaces;

namespace SpaceShootingTrip.Components
{
    public class AutoShootComponent : IComponent
    {
        public float interval;
        public float timeToNextShoot;
    }
}