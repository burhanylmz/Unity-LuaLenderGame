using UnityEngine;

public class Soundmanager : MonoBehaviour
{
    [SerializeField] private AudioClip onfuel;
    [SerializeField] private AudioClip oncoin;
    [SerializeField] private AudioClip crash;
    [SerializeField] private AudioClip success;

    private void Start()
    {
        Lander.Instance.onfuel += Lander_Onfuel;
        Lander.Instance.oncoin += Lander_Oncoin;
        Lander.Instance.OnLanded += Lander_Onlanded;
    }
    private void Lander_Onfuel(object sender,System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(onfuel, Camera.main.transform.position);

    }
    private void Lander_Oncoin(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(oncoin, Camera.main.transform.position);
    }
    private void Lander_Onlanded(object sender, Lander.OnLandedEventArgs e)
    {
        switch (e.landingtype) 
        {
            case Lander.LandingType.basari:
                AudioSource.PlayClipAtPoint(success, Camera.main.transform.position);
                break;
            default:
                AudioSource.PlayClipAtPoint(crash, Camera.main.transform.position);
                break;



        }
    }

}
