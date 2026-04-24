using UnityEngine;
using Unity.Cinemachine;


public class cinemazoom : MonoBehaviour
{
    public static cinemazoom Instance { get; private set; }

    [SerializeField] private CinemachineCamera cinemamachineCamera;

    private float targetsize = 10f;
    public void settargetsize(float targetsize) 
    {
        this.targetsize = targetsize; 
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {  
        cinemamachineCamera.Lens.OrthographicSize =
            Mathf.Lerp( cinemamachineCamera.Lens.OrthographicSize,targetsize,Time.deltaTime *2f);
    }
    public void setnormalsize() 
    {
        settargetsize(10f);
    }
}
