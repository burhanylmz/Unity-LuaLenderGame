using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tablo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI StatsTextMesh;
    [SerializeField] private GameObject SpeedLeftArrow;
    [SerializeField] private GameObject SpeedRightArrow;
    [SerializeField] private GameObject SpeedUpArrow;
    [SerializeField] private GameObject SpeedDownArrow;
    [SerializeField] private Image fuelýmage;





    private void Update()
    {
        UpdateStatsTextMesh();
    }
    private void UpdateStatsTextMesh()
    {
        //oklar
        SpeedUpArrow.SetActive(Lander.Instance.GetSpeedY() >= 0);
        SpeedDownArrow.SetActive(Lander.Instance.GetSpeedY() < 0);
        SpeedRightArrow.SetActive(Lander.Instance.GetSpeedX() >= 0);
        SpeedLeftArrow.SetActive(Lander.Instance.GetSpeedX() < 0);

        //fuel fiil
        fuelýmage.fillAmount = Lander.Instance.GetFueloran();

        StatsTextMesh.text =
        GameManager.Instance.Getlevel() + "\n" +
        GameManager.Instance.GetScore() + "\n"+
        Mathf.Round(GameManager.Instance.GetTime()) + "\n" +
        Mathf.Abs(Mathf.Round(Lander.Instance.GetSpeedX() * 10f)) + "\n" +
        Mathf.Abs(Mathf.Round(Lander.Instance.GetSpeedY()*10f));
    }
}
 