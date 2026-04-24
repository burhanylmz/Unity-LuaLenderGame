using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lander : MonoBehaviour
{
    //baþka taraflardan lander.diyerek ulaþmak için
    public static Lander Instance { get; private set; }

    //Ateþleme
    public event EventHandler OnUpForce;
    public event EventHandler OnLeftForce;
    public event EventHandler OnRightForce;
    public event EventHandler OnBeforeForce;
    public event EventHandler oncoin;
    public event EventHandler onfuel;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }
    //landeddaki score ile game managerdaki score birleþtirmek
    public event EventHandler<OnLandedEventArgs> OnLanded;
    public class OnLandedEventArgs : EventArgs {
        public LandingType landingtype;
        public int score;
        public float dotVector;
        public float landingspeed;
        public float scoreMultiplier;
    }
    //baþarý tablosu
    public enum LandingType 
    {
        basari,
        yanlýsinisalani,
        yanlisaci,
        hizliinis
    }
    //gameover restart
    public enum State
    {
        waitingtostart,
        normal,
        gameover
    }


    private Rigidbody2D rb;
    public float yakitmiktari;
    public float maxyakitmiktari=10f;
    private State state;

    private void Awake()
    {
        Instance = this;

        yakitmiktari = maxyakitmiktari;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;

        state= State.waitingtostart;
    }

    private void FixedUpdate()
    {
        OnBeforeForce?.Invoke(this, EventArgs.Empty);

        switch (state) 
        {
            default:
                case State.waitingtostart:
                if (Keyboard.current.upArrowKey.isPressed ||
                    Keyboard.current.leftArrowKey.isPressed ||
                    Keyboard.current.rightArrowKey.isPressed)
                {
                    rb.gravityScale = 0.7f;
                    setstate(State.normal);
                }
                break;
                case State.normal:
                //yakýt kontrol
                if (yakitmiktari <= 0)
                {
                    return;
                }
                if (Keyboard.current.upArrowKey.isPressed||
                    Keyboard.current.leftArrowKey.isPressed||
                    Keyboard.current.rightArrowKey.isPressed)
                {
                    yakittüketimi();
                }
                //Yönlendirme
                if (Keyboard.current.upArrowKey.isPressed)
                {
                    float force = 700f;
                    rb.AddForce(force * transform.up * Time.deltaTime);
                    OnUpForce?.Invoke(this, EventArgs.Empty);
                }

                if (Keyboard.current.leftArrowKey.isPressed)
                {
                    float turnspeed = +100f;
                    rb.AddTorque(turnspeed * Time.deltaTime);
                    OnLeftForce?.Invoke(this, EventArgs.Empty);

                }

                if (Keyboard.current.rightArrowKey.isPressed)
                {
                    float turnspeed = -100f;
                    rb.AddTorque(turnspeed * Time.deltaTime);
                    OnRightForce?.Invoke(this, EventArgs.Empty);
                }
                break;
                case State.gameover:
                break;
        }



    }
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
     //piste iniþ kontrolü
        if (!collision2D.gameObject.TryGetComponent(out InisPist ýnisPist))
        {
            Debug.Log("araziye inildi");
            OnLanded?.Invoke(this, new OnLandedEventArgs
            {
                landingtype = LandingType.yanlýsinisalani,
                dotVector = 0f,
                landingspeed = 0f,
                scoreMultiplier = 0,
                score = 0,
            });
            setstate(State.gameover);
            return;
        }

     //iniþ hýzý kontrol
        float yumusakinisbuyuklugu = 4f;
        float bagilhizbuyuklugu = collision2D.relativeVelocity.magnitude;
        if (bagilhizbuyuklugu > yumusakinisbuyuklugu)
        {
            Debug.Log("Sert iniþ");
            OnLanded?.Invoke(this, new OnLandedEventArgs
            {
                landingtype = LandingType.hizliinis,
                dotVector = 0f,
                landingspeed = bagilhizbuyuklugu,
                scoreMultiplier =0,
                score = 0,
            });
            setstate(State.gameover);
            return;
        }

    //iniþ açýsý kontrol

        float dotVector = Vector2.Dot(Vector2.up, transform.up);
        float minDotVector = .90f;
        if (dotVector<minDotVector)
        {
            Debug.Log("Lander egik açýyla indi");
            OnLanded?.Invoke(this, new OnLandedEventArgs
            {
                landingtype = LandingType.yanlisaci,
                dotVector = dotVector,
                landingspeed = bagilhizbuyuklugu,
                scoreMultiplier = 0,
                score = 0,
            });
            setstate(State.gameover);
            return;
        }

        Debug.Log("Baþarýlý Yumuþak Ýniþ");

     // puanlama
        float maxacýpuani = 100;
        float vektörcarpani = 10f;
        float inisacipuani = maxacýpuani - Mathf.Abs(dotVector - 1f) * vektörcarpani * maxacýpuani;

        float maxinishizipuani = 100;
        float inishizipuani = (yumusakinisbuyuklugu - bagilhizbuyuklugu) * maxinishizipuani;

        Debug.Log("iniþ aci puaný:" + inisacipuani);
        Debug.Log("inis hiz puaný" + inishizipuani);

        //Genel skore
        int score = Mathf.RoundToInt(inisacipuani + inishizipuani) * ýnisPist.scoreMultiplier;
        Debug.Log("Genel score" + score);
        //gamemanager'a yolladýk
        OnLanded?.Invoke(this, new OnLandedEventArgs
        { landingtype = LandingType.basari,
            dotVector = dotVector,
            landingspeed = bagilhizbuyuklugu,
            scoreMultiplier = ýnisPist.scoreMultiplier,
            score = score,
        });
        setstate(State.gameover);
    }

    //Yakýt doldurma
    private void OnTriggerEnter2D(Collider2D collider2d)
    {
        if (collider2d.gameObject.name == "Fuel")
        {
            float yakýteklemek = 10f;
            yakitmiktari += yakýteklemek;
            if (yakitmiktari>maxyakitmiktari)
            {
                yakitmiktari = maxyakitmiktari;
            }
            onfuel?.Invoke(this, EventArgs.Empty);
            Destroy(collider2d.gameObject);
        }
        if (collider2d.gameObject.TryGetComponent(out coin coin))
        {
            oncoin?.Invoke(this, EventArgs.Empty);
            coin.DestroySelf();
        }
    }
    //DURUM AYARLAMA
    private void setstate(State state)
    {
        this.state = state;
        OnStateChanged?.Invoke(this,new OnStateChangedEventArgs {state = state});
    }
    //yakýttüketimi
    private void yakittüketimi() 
    {
        float yakittüketimmiktari = 1f;
        yakitmiktari -= yakittüketimmiktari * Time.deltaTime;
    }

    //TABLO
    public float GetSpeedX()
    {
        return rb.linearVelocityX;
    }
    public float GetSpeedY()
    {
        return rb.linearVelocityY;
    }
    public float GetFuel() 
    {
        return yakitmiktari;
    }
    public float GetFueloran() 
    {
        return yakitmiktari / maxyakitmiktari;
    }

}
