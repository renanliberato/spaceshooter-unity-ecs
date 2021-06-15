using TinyECS.Interfaces;

namespace SpaceShootingTrip.Components
{
    public enum MatchSteps
    {
        Start = 0,
        InMatch = 1,
        End = 2,
    }

    public struct MatchStepComponent :IComponent
    {
        public MatchSteps step;
    }
}