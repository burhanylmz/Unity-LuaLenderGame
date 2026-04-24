using UnityEngine;

public class LanderAtes : MonoBehaviour
{
    [SerializeField] private ParticleSystem LeftThrusterParticleSystem;
    [SerializeField] private ParticleSystem RightThrusterParticleSystem;
    [SerializeField] private ParticleSystem MiddleThrusterParticleSystem;
    [SerializeField] private GameObject landerExplosionVfx;


    private Lander lander;

    private void Awake()
    {
        lander = GetComponent<Lander>();

        lander.OnUpForce += Lander_OnUpForce;
        lander.OnLeftForce += Lander_OnLeftForce;
        lander.OnRightForce += Lander_OnRightForce;
        lander.OnBeforeForce += Lander_OnBeforeForce;


        SetEnabledThrusterParticleSystem(LeftThrusterParticleSystem, false);
        SetEnabledThrusterParticleSystem(MiddleThrusterParticleSystem, false);
        SetEnabledThrusterParticleSystem(RightThrusterParticleSystem, false);

    }

    private void Start()
    {
        lander.OnLanded += Lander_Onlanded;
    }
    private void Lander_Onlanded(object sender, Lander.OnLandedEventArgs e) 
    {
        switch (e.landingtype)
        {
            case Lander.LandingType.hizliinis:
            case Lander.LandingType.yanlisaci:
            case Lander.LandingType.yanlýsinisalani:
                //patlama
                Instantiate(landerExplosionVfx,transform.position,Quaternion.identity);
                gameObject.SetActive(false);
                break;
        }
    }
    private void Lander_OnBeforeForce(object sender, System.EventArgs e)
    {
        SetEnabledThrusterParticleSystem(LeftThrusterParticleSystem, false);
        SetEnabledThrusterParticleSystem(MiddleThrusterParticleSystem, false);
        SetEnabledThrusterParticleSystem(RightThrusterParticleSystem, false);
    }
    private void Lander_OnUpForce(object sender, System.EventArgs e)
    {
        SetEnabledThrusterParticleSystem(LeftThrusterParticleSystem, true);
        SetEnabledThrusterParticleSystem(MiddleThrusterParticleSystem, true);
        SetEnabledThrusterParticleSystem(RightThrusterParticleSystem, true);
    }
    private void Lander_OnLeftForce(object sender, System.EventArgs e)
    {
        SetEnabledThrusterParticleSystem(LeftThrusterParticleSystem, true);
    }

    private void Lander_OnRightForce(object sender , System.EventArgs e) 
    {
        SetEnabledThrusterParticleSystem(RightThrusterParticleSystem, true);
    }

    private void SetEnabledThrusterParticleSystem(ParticleSystem particleSystem, bool enabled)
    {
        ParticleSystem.EmissionModule emissionModule = particleSystem.emission;
        emissionModule.enabled= enabled;
    }

}
