using TinyECS.Interfaces;

namespace SpaceShootingTrip.Components
{
    public class MatchLevelComponent : IComponent
    {
        public int level;
        public float timeToNextLevel;
        public float levelTimeInterval;
    }
}