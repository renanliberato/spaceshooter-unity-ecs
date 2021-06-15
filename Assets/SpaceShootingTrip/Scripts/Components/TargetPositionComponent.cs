using TinyECS.Interfaces;
using UnityEngine;

namespace SpaceShootingTrip.Components
{
    public class TargetPositionComponent : IComponent
    {
        public Vector3 position;
        public float step;
    }
}
