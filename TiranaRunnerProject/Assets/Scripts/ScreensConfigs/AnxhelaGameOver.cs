using UnityEngine;
using UnityEngine.SceneManagement;

public class AnxhelaGameOver : MonoBehaviour
{
    public string levelSelectionName = "LevelSelectorOldWorking";

    public SceneFader sceneFader;

    public void LevelSelection()
    {
        Debug.Log("fjkdsjfsdkjf");
        sceneFader.FadeTo(levelSelectionName);
        //SceneManager.LoadScene(levelSelectionName);
    }
}
