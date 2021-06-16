using TinyECS.Interfaces;

namespace SpaceShootingTrip.Components
{
    public enum MatchSteps
    {
        InMatch = 1,
        End = 2,
    }

    public class MatchStepComponent :IComponent
    {
        public MatchSteps step;
    }
}