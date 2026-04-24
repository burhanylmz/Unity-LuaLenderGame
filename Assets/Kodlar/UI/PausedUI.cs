using UnityEngine;
using UnityEngine.UI;

public class PausedUI : MonoBehaviour
{
    [SerializeField] private Button resumebutton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button anamenubutton;
    private void Awake()
    {
        resumebutton.onClick.AddListener(() => {
            GameManager.Instance.UnPauseGame();
        });
        pauseButton.onClick.AddListener(() => {
            GameManager.Instance.PauseGame();
        });
        anamenubutton.onClick.AddListener(() => {
            Time.timeScale = 1f;
            SceneLoader.LoadScene(SceneLoader.Scene.MainMenuScene);
        });

    }
    private void Start() 
    {
        GameManager.Instance.UnPauseGame();
        resumebutton.Select();
    }

}
