using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private void Start()
    {

        LoadSceneAsync("StartScene");
    }




    public void LoadSceneAsync(string sceneName)
    {

        StartCoroutine(LoadSceneAsyncCoroutine(sceneName));
    }
    private IEnumerator LoadSceneAsyncCoroutine(string sceneName)
    {
        yield return new WaitForSeconds(5f);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        while (!asyncOperation.isDone)
        {

            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            Debug.Log(sceneName + ": " + (progress * 100) + "%");
            if (progress * 100 == 100)
            {

                Debug.Log("Сцена " + sceneName + " Загружена полностью.");
            }
            yield return null;
        }


    }
}
