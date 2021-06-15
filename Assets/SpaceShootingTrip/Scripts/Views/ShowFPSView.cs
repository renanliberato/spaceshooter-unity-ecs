using TinyECS.Interfaces;
using TinyECSUnityIntegration.Impls;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShootingTrip.Views
{
    public class ShowFPSView : BaseStaticView
    {
        public Text fpsText;
        public Text numberOfEntitiesText;
        public float deltaTime;

        public override void RegisterSubscriptions(IEventManager eventManager, EntityId entityId)
        {
        }

        void Update()
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;
            fpsText.text = $"FPS: {Mathf.Ceil(fps).ToString()}";

            numberOfEntitiesText.text = $"Number of entities: {mWorldContext.Statistics.mNumOfActiveEntities}";
        }
    }
}
