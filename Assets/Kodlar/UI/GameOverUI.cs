using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button mainmenubutton;
    [SerializeField] private TextMeshProUGUI scoretext;

    private void Awake()
    {
        mainmenubutton.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(SceneLoader.Scene.MainMenuScene);
        });
    }
    private void Start()
    {
        scoretext.text = "OYUN SONU SKORU:" + GameManager.Instance.gettotalscore().ToString();
    }
}
