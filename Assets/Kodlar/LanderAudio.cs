using UnityEngine;

public class LanderAudio : MonoBehaviour
{
    [SerializeField] private AudioSource thrusteraudiosource;

    private Lander lander;

    private void Awake()
    {
        lander =GetComponent<Lander>();
    }
    private void Start()
    {
        lander.OnBeforeForce += LanderOnBeforeForce;
        lander.OnUpForce += LanderOnUpForce;
        lander.OnLeftForce += LanderOnLeftForce;
        lander.OnRightForce += LanderOnRightForce;

        thrusteraudiosource.Pause();

    }
    private void LanderOnLeftForce(object sender , System.EventArgs e)
     {
        if (!thrusteraudiosource.isPlaying)
        {
            thrusteraudiosource.Play();
        }
    }
    private void LanderOnRightForce(object sender, System.EventArgs e)
    {
        if (!thrusteraudiosource.isPlaying)
        {
            thrusteraudiosource.Play();
        }
    }
    private void LanderOnUpForce(object sender, System.EventArgs e)
    {
        if (!thrusteraudiosource.isPlaying)
        {
            thrusteraudiosource.Play();
        }
    }
    private void LanderOnBeforeForce(object sender, System.EventArgs e)
    {
        thrusteraudiosource.Pause();

    }
}
