using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Assets.Application
{
    public class LoadOnClick : MonoBehaviour 
    {
        public void NewGameBtn(string newGameLevel)
        {
            SceneManager.LoadScene(newGameLevel);
        }
    }
}
