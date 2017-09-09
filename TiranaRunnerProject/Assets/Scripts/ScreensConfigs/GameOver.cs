using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    //ANXHELA SET NAME OF FIRST MENU
    public string menuSceneName = "LevelSelectorOldWorkingA";
    public string levelSelectionName = "LevelSelectorOldWorkingA";

    public SceneFader sceneFader;

    public void Retry()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }
    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }

    public void LevelSelection()
    {
        sceneFader.FadeTo(levelSelectionName);
        //SceneManager.LoadScene(levelSelectionName);
    }
}
