using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BasarıUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private Button nextbutton;
    [SerializeField] private TextMeshProUGUI nextbuttontext;


    private Action nextButtonAction;
    private void Awake()
    {
        //tekrar oyna butonu
        nextbutton.onClick.AddListener(() =>
        {
            nextButtonAction();
        });
    }
    private void Start()
    {
        Lander.Instance.OnLanded += Lander_OnLanded;
        Hide();
    }
    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e) 
    {
        if (e.landingtype== Lander.LandingType.basari)
        {
            title.text = "BASARILI INIS!";
            nextbuttontext.text = "SONRAKI BOLUM";
            nextButtonAction = GameManager.Instance.gotonextlevel;
        }
        else
        {
            title.text = "<color=#ff0000>PATLADIN!!!</color>";
            nextbuttontext.text = "TEKRAR DENE";
            nextButtonAction = GameManager.Instance.retrylevel;

        }
        label.text =
            Mathf.Round(e.landingspeed*2f) + "\n" +
            Mathf.Round(e.dotVector * 100f) + "\n" +
            "x" + e.scoreMultiplier + "\n" +
            e.score;
        Show();
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide() 
    {
        gameObject.SetActive(false);
    }

}
