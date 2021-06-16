using SpaceShootingTrip.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShootingTrip.Views
{
    public class SelectShootingTypeSceneBehaviour : MonoBehaviour
    {
        public void SelectStraightLineShootingAndTransitionToGame()
        {
            Player.ShootingType = Enums.ShootingType.StraightLine;

            SceneManager.LoadScene(2);
        }

        public void SelectSparseDirectionalShootingAndTransitionToGame()
        {
            Player.ShootingType = Enums.ShootingType.SparseDirectional;

            SceneManager.LoadScene(2);
        }
    }
}
