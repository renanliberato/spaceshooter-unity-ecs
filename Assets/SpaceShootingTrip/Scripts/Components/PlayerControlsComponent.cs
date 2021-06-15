using TinyECS.Interfaces;

namespace SpaceShootingTrip.Components
{
    public struct PlayerControlsComponent : IComponent
    {
        public bool moveLeft;
        public bool moveRight;
    }
}