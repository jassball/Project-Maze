using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneOperator : MonoBehaviour
{
    public Button ExitGameButton;
    public Button enterMazeButton;
    private string currentSceneName;

    private void Start()
    {
        ExitGameButton.onClick.AddListener(QuitGame);
        enterMazeButton.onClick.AddListener(PlayGame);
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    public void PlayGame()
    {
        StartCoroutine(DelayedSceneSwitch("Level 1",1.8f));
        Debug.Log("sceneSwitch!");
    }

    public void QuitGame()
    {
        print("Quit");
        Application.Quit();
        Debug.Log("quit!");
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private IEnumerator DelayedSceneSwitch(string sceneName,float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncUnload = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while (!asyncUnload.isDone)
        {
            yield return null;
        }
    }
}
