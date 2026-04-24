using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static float musictime;

    private AudioSource musicAudioSource;

    private void Awake()
    {
        musicAudioSource=GetComponent<AudioSource>();
        musicAudioSource.time = musictime;
    }
    private void Update()
    {
        musictime = musicAudioSource.time;
    }
}
