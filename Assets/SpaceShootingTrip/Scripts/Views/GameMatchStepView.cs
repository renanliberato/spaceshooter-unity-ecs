using SpaceShootingTrip.Components;
using System;
using System.Collections.Generic;
using TinyECS.Impls;
using TinyECS.Interfaces;
using TinyECSUnityIntegration.Impls;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SpaceShootingTrip.Views
{
    public class GameMatchStepView : BaseStaticView, IEventListener<TComponentChangedEvent<MatchStepComponent>>, IEventListener<TComponentChangedEvent<MatchLevelComponent>>
    {
        public Text levelText;
        public Image timeToNextLevelBar;
        public GameObject EndGameView;
        public GameObject ControlsView;

        private readonly IList<uint> _eventManagerSubscriptions = new List<uint>();

        public override void RegisterSubscriptions(IEventManager eventManager, EntityId entityId)
        {
            IEntity linkedEntity = mWorldContext.GetEntityById(entityId);

            linkedEntity.AddComponent(new MatchLevelComponent { level = 1, timeToNextLevel = 5f, levelTimeInterval = 5f });
            linkedEntity.AddComponent(new MatchStepComponent { step = MatchSteps.InMatch });

            _eventManagerSubscriptions.Add(eventManager.Subscribe<TComponentChangedEvent<MatchStepComponent>>(this));
            _eventManagerSubscriptions.Add(eventManager.Subscribe<TComponentChangedEvent<MatchLevelComponent>>(this));
        }

        public void OnDestroy()
        {
            foreach (var id in _eventManagerSubscriptions)
                _eventManager.Unsubscribe(id);

            Time.timeScale = 1;
        }

        public void OnEvent(TComponentChangedEvent<MatchStepComponent> eventData)
        {
            switch (eventData.mValue.step)
            {
                case MatchSteps.InMatch:
                    Time.timeScale = 1;
                    break;
                case MatchSteps.End:
                    Time.timeScale = 0;
                    ControlsView.SetActive(false);
                    EndGameView.SetActive(true);
                    break;
            }
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(1);
        }

        public void OnEvent(TComponentChangedEvent<MatchLevelComponent> eventData)
        {
            levelText.text = eventData.mValue.level.ToString();

            timeToNextLevelBar.rectTransform.sizeDelta = new Vector2(
                Math.Min(170f, ((eventData.mValue.levelTimeInterval - eventData.mValue.timeToNextLevel) / eventData.mValue.levelTimeInterval) * 170f),
                timeToNextLevelBar.rectTransform.sizeDelta.y
            );
        }
    }
}
