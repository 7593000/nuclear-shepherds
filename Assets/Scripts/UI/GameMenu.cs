using UnityEngine;

public class GameMenu : MonoBehaviour
{
    private GameSettings _settings;
    private CanvasGroup _group;
    private bool _visible = false;

    public void Initialized(GameSettings settings)
    {
        _settings = settings;

    }

    private void Start()
    {
        _group = GetComponent<CanvasGroup>();
        _group.blocksRaycasts = false;
        _group.interactable = false;
        _group.alpha = 0f;
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

    public void NewGame() { }
    public void Resume()
    {
        GameMenuStatus();
    }
    public void SaveGame() {
        _settings.SaveGame();
    }
    public void LoadGame() { }
    public void ExitGame()
    {
         
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE_WIN && !UNITY_EDITOR
    Application.Quit();
#endif
        }

    }
