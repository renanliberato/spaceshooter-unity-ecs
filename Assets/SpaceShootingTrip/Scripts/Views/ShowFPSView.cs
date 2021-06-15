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
            if (Time.timeScale == 0)
                return;

            deltaTime += ((Time.deltaTime/Time.timeScale) - deltaTime) * 0.1f;

            if (deltaTime > 0)
            {
                float fps = 1.0f / deltaTime;
                fpsText.text = $"FPS: {Mathf.Ceil(fps).ToString()}";
            }

            numberOfEntitiesText.text = $"Number of entities: {mWorldContext.Statistics.mNumOfActiveEntities}";
        }
    }
}
