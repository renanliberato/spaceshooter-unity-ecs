using SpaceShootingTrip.Components;
using System.Collections.Generic;
using TinyECS.Impls;
using TinyECS.Interfaces;
using TinyECSUnityIntegration.Impls;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SpaceShootingTrip.Views
{
    public class GameMatchStepView : BaseStaticView, IEventListener<TComponentChangedEvent<MatchStepComponent>>
    {
        public GameObject StartGameView;
        public GameObject EndGameView;
        public GameObject ControlsView;
        public Button RestartButton;

        private readonly IList<uint> _eventManagerSubscriptions = new List<uint>();

        private void Start()
        {
            Time.timeScale = 0;
        }

        public override void RegisterSubscriptions(IEventManager eventManager, EntityId entityId)
        {
            IEntity linkedEntity = mWorldContext.GetEntityById(entityId);

            linkedEntity.AddComponent(new MatchStepComponent { step = 0 });

            _eventManagerSubscriptions.Add(eventManager.Subscribe<TComponentChangedEvent<MatchStepComponent>>(this));
        }

        public void OnDestroy()
        {
            foreach (var id in _eventManagerSubscriptions)
                _eventManager.Unsubscribe(id);
        }

        public void OnEvent(TComponentChangedEvent<MatchStepComponent> eventData)
        {
            switch (eventData.mValue.step)
            {
                case MatchSteps.InMatch:
                    ControlsView.SetActive(true);
                    StartGameView.SetActive(false);
                    Time.timeScale = 1;
                    break;
                case MatchSteps.End:
                    Time.timeScale = 0;
                    ControlsView.SetActive(false);
                    EndGameView.SetActive(true);
                    break;
            }
        }

        public void StartGame()
        {
            mWorldContext.GetEntityById(LinkedEntityId).AddComponent(new MatchStepComponent { step = MatchSteps.InMatch });
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(0);
        }
    }
}
