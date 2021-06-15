using TinyECS.Interfaces;

namespace SpaceShootingTrip.Components
{
    public class HealthComponent : IComponent
    {
        public int current;
        public int max;
    }
}