using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour, IPointerClickHandler
{
    private GameSettings _settings;
    [SerializeField] private LoadGameWindow _loadGameWindow;
    private CanvasGroup _group;
    private bool _visible = false;
    private SaveLoadEngine _saveLoadEngine;


    public static Color32 H1 = new(164, 136, 48, 255);
    public static FontStyles FONTSTYLEH1 = FontStyles.Bold;
    public static Color32 P = new(255, 255, 255, 255);



    public void Initialized(GameSettings settings)
    {
        _settings = settings;
        _loadGameWindow ??= FindFirstObjectByType<LoadGameWindow>();
        _group = GetComponent<CanvasGroup>();
        _group.blocksRaycasts = false;
        _group.interactable = false;
        _group.alpha = 0f;
    }

    private void Start()
    {   
        _group = GetComponent<CanvasGroup>();
        //_group.blocksRaycasts = false;
        //_group.interactable = false;
        //_group.alpha = 0f;

        _saveLoadEngine = new SaveLoadEngine();
        string[] path = _saveLoadEngine.GetAllSaveFiles();
        _loadGameWindow.Initialized(path);
    }
    /// <summary>
    /// Активность меню , показать - скрыть
    /// </summary>
    public void GameMenuStatus()
    {
        Time.timeScale = _visible ? 1 : 0;
        _visible = !_visible;
        _group.alpha = _visible ? 1 : 0;
        _group.blocksRaycasts = _visible;
        _group.interactable = _visible;

    }

    public void NewGame() { LoadSceneAsync("LoadGame"); }
    public void Resume()
    {
        GameMenuStatus();
    }

    public void SaveGame()
    {

        _settings.SaveGame();
        string[] path = _saveLoadEngine.GetAllSaveFiles();
        _loadGameWindow.Initialized(path);
        GameMenuStatus();


    }
    public void OpenLoadGameWindows()
    {


        _loadGameWindow.LoadWindowStatus();



    }


    public void ExitGame()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE_WIN && !UNITY_EDITOR
    Application.Quit();
#endif
    }



    public void LoadSceneAsync(string sceneName)
    {

        StartCoroutine(LoadSceneAsyncCoroutine(sceneName));
    }
    private IEnumerator LoadSceneAsyncCoroutine(string sceneName)
    {

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

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;

        if (clickedObject != null)
        {

            LoadGameItem itemData = clickedObject.GetComponent<LoadGameItem>();
            if (itemData != null)
            {
                
                LoadGame(itemData.GetPath);

            }
        }
    }
    private void LoadGame(string path)
    {

        GameState.Instance.LoadedGameData = _saveLoadEngine.LoadData(path);
        GameState.Instance.IsLoading = true;
        LoadSceneAsync("LoadGame");

    }

   

}


