using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShootingTrip.Views
{
    public class InitialSceneBehaviour : MonoBehaviour
    {
        public void TransitionToSelectShootingTypeScene()
        {
            SceneManager.LoadScene(1);
        }
    }
}
