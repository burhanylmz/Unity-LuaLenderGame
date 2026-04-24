using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField] static int levelnumber=1;
    [SerializeField] private List<GameLevel> gameLevelList;
    [SerializeField] private Lander lander;
    [SerializeField] private CinemachineCamera cinemamachine;
    [SerializeField] private GameObject pausedUIPanel;
    private int score;
    private static int totalscore=0;
    private float time;
    private bool istimeractive;

    public static void resetstartdata() 
    { 
        levelnumber = 1;
        totalscore = 0;
    }

    //score yu tabloya yolladýk
    public static GameManager Instance {  get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        lander.oncoin += Lander_oncoin;
        Lander.Instance.OnLanded += Lander_OnLanded;
        Lander.Instance.OnStateChanged += Lander_OnStateChanged;

        //level sistemi
        LoadCurrentLevel();
    }
    //state degiþimi
    private void Lander_OnStateChanged(object sender, Lander.OnStateChangedEventArgs e) 
    {
        istimeractive = e.state == Lander.State.normal;
        //zoom
        if (e.state == Lander.State.normal)
        {
            cinemamachine.Target.TrackingTarget = Lander.Instance.transform;
            cinemazoom.Instance.setnormalsize();

        }
    }
    private void Update()
    {
        if (istimeractive)
        {
        time += Time.deltaTime;    
        }
    }
    //level sistemi
    private void LoadCurrentLevel() 
    {
       GameLevel gamelevel= GetGameLevel();
        GameLevel spawnedGameLevel = Instantiate(gamelevel, Vector3.zero, Quaternion.identity);
        Lander.Instance.transform.position = spawnedGameLevel.GetLanderStartPosition();
        //zoom
        cinemamachine.Target.TrackingTarget = spawnedGameLevel.GetCameraStartTargetTransform();
        cinemazoom.Instance.settargetsize(spawnedGameLevel.zoomout());
    }
    private GameLevel GetGameLevel()
    {
        foreach (GameLevel gameLevel in gameLevelList)
        {
            if (gameLevel.GetLevelNumber() == levelnumber)
            {
                return gameLevel;
            }
        }
        return null;
    }

    private void Lander_OnLanded(object sender , Lander.OnLandedEventArgs e) 
    {
        AddScore(e.score);
    } 
    private void Lander_oncoin(object sender, System.EventArgs e) 
    {
        AddScore(500);
    }
    public void AddScore(int addscoreamount) 
    {
        score += addscoreamount;
        Debug.Log(score);   
    }
    //TABLO 
    public int GetScore() 
    {
        return score;
    }
    public float GetTime() 
    {
        return time;
    }

    public int gettotalscore()
    {
        return totalscore;
    }
    //retry nextlevel
    public void gotonextlevel()
    {
        levelnumber++;
        totalscore += score;
        if (GetGameLevel() == null)
        {
            SceneLoader.LoadScene(SceneLoader.Scene.GameOverScene); 
        }
        else
        {
            SceneLoader.LoadScene(SceneLoader.Scene.GameScene);   
        }
    }
    public void retrylevel()
    {
        SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
    }
    public int Getlevel()
    { 
        return levelnumber;
    }

    //duraklama
    public void PauseGame()
    {
        Time.timeScale = 0f;
        pausedUIPanel.SetActive(true);
        
    }
    public void UnPauseGame()
    {
        Time.timeScale = 1f;
        pausedUIPanel.SetActive(false);

    }
}

