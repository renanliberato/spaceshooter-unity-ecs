using SpaceShootingTrip.Components;
using TinyECS.Interfaces;
using UnityEngine;

namespace SpaceShootingTrip.Systems
{
    public class PlayerInputSystem : IUpdateSystem
    {
        protected IWorldContext mWorldContext;

        public PlayerInputSystem(IWorldContext worldContext)
        {
            mWorldContext = worldContext;
        }

        public void Update(float deltaTime)
        {
            var player = mWorldContext
                .GetSingleEntityWithAll(typeof(Components.PlayerComponent));

            if (player == null)
                return;

            var playerinput = player.GetComponent<PlayerControlsComponent>();

            var xSpeed = (playerinput.moveLeft ? -1 : playerinput.moveRight ? 1 : 0) * 4;
            var pos = player.GetComponent<PositionComponent>();
            if (xSpeed > 0 && pos.value.x >= 1.75)
            {
                player.AddComponent(new VelocityComponent { value = new Vector2(0, 0) });
                return;
            }
            
            if (xSpeed < 0 && pos.value.x <= -1.75)
            {
                player.AddComponent(new VelocityComponent { value = new Vector2(0, 0) });
                return;
            }

            player.AddComponent(new VelocityComponent { value = new Vector2(xSpeed, 0) });
        }
    }
}
