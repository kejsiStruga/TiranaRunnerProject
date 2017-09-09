using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField]
    public bool loadScene = false;

    public Text loadingText;
    public string emriSkenes;
    public static LoadingScene _instanceA;

    public static LoadingScene InstanceA
    {
        get { return _instanceA; }
    }
    void Awake()
    {
        if (_instanceA == null)
            _instanceA = this;
        else
            Object.Destroy(this);
    }
    void Start()
    {
        loadingText.text = "Run in Tirana"; // do hiqet kur te kalohet kontrolli per touch screne
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && loadScene == false)
        {
            loadScene = true;
            loadingText.text = "Loading...";
            StartCoroutine(LoadNewScene(emriSkenes));
        }
        if (loadScene == true)
        {
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
        }
    }

    public static IEnumerator LoadNewScene( string emriSkenes)
    {
        yield return new WaitForSeconds(3);
        AsyncOperation async = Application.LoadLevelAsync(emriSkenes);
        while (!async.isDone)
        {
            yield return null;
        }
    }
}