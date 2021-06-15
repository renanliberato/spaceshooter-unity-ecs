using TinyECS.Interfaces;

namespace SpaceShootingTrip.Components
{
    public struct HealthComponent : IComponent
    {
        public int current;
        public int max;
    }
}