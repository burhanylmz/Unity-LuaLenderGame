using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playbutton;
    [SerializeField] private Button quitbutton;

    private void Awake()
    {
        playbutton.onClick.AddListener(() => {
            GameManager.resetstartdata();
            SceneLoader.LoadScene(SceneLoader.Scene.GameScene);

        });
        quitbutton.onClick.AddListener(() => {
            Application.Quit();
        });
    }
    private void Start()
    {
        playbutton.Select();
    }

}
