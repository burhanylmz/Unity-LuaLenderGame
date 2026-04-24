using UnityEngine;

public class GameLevel : MonoBehaviour
{
    [SerializeField] private int levelnumber;
    [SerializeField] private Transform landerstartpositiontransform;
    [SerializeField] private Transform camerastart;
    [SerializeField] private float zoomout1;



    public int GetLevelNumber()
    {
        return levelnumber;
    }
    //zoom
    public Vector3 GetLanderStartPosition() 
    {
        return landerstartpositiontransform.position;
    }
    public Transform GetCameraStartTargetTransform() 
    {
        return camerastart;
    }
    public float zoomout()
    {
        return zoomout1;
    }
}
