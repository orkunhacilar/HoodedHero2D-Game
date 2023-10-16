using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHaraketController : MonoBehaviour
{

    public static PlayerHaraketController instance;
    
    Rigidbody2D rb;

    [SerializeField]
    GameObject normalPlayer, kilicPlayer, mizrakPlayer, okPlayer;

    [SerializeField]
    Transform zeminKontrolNoktasi;

    [SerializeField]
    Animator NormalAnim, kilicAnim, mizrakAnim, okAnim;  //animastonlarimi kontrol etmek icin aliyorum ve orda olusturdugum fonksyonlara erisip kullanacagim.

    [SerializeField]
    SpriteRenderer normalSprite, kilicSprite,mizrakSprite,okSprite;

    [SerializeField]
    GameObject kilicVurusBoxObje;

    public LayerMask zeminMaske; // Tag sectirir gibi 2 d oldugu icin layer sectiriyor bize public bir sekilde

    public float haraketHizi;
    public float ziplamaGucu;

    bool zemindemi;
    bool ikinciKezZiplasinmi;

    [SerializeField]
    float geriTepkiSuresi, geriTepkiGucu;
    float geriTepkiSayaci;

    bool yonSagdami;

    public bool playerCanverdimi;

    bool kiliciVurdumu;

    [SerializeField]
    GameObject atilacakMizrak;

    [SerializeField]
    Transform mizrakCikisNoktasi;

    [SerializeField]
    GameObject atilacakOk;

    [SerializeField]
    Transform okCikisNoktasi;

    bool okAtabilirmi;

    [SerializeField]
    float tirmanisHizi = 3f;

    [SerializeField]
    GameObject normalKamera, kilicKamera, okKamera, mizrakKamera;

    private void Awake()
    {
        instance = this;
        kiliciVurdumu = false;
        okAtabilirmi = true;

        rb = GetComponent<Rigidbody2D>();
        playerCanverdimi = false;

        kilicVurusBoxObje.SetActive(false);
    }

    void Update()
    {
        if (playerCanverdimi)
            return;

        if (geriTepkiSayaci <= 0)
        {
            HareketEt();
            ZiplaFNC();
            YonuDegistirFNC();

            if (normalPlayer.activeSelf)
            {
                normalSprite.color = new Color(normalSprite.color.r, normalSprite.color.g, normalSprite.color.b, 1f);
            }
            if (kilicPlayer.activeSelf)
            {
                kilicSprite.color = new Color(kilicSprite.color.r, kilicSprite.color.g, kilicSprite.color.b, 1f);
            }
            if (mizrakPlayer.activeSelf)
            {
                mizrakSprite.color = new Color(mizrakSprite.color.r, mizrakSprite.color.g, mizrakSprite.color.b, 1f);
            }
            if (okPlayer.activeSelf)
            {
                okSprite.color = new Color(okSprite.color.r, okSprite.color.g, okSprite.color.b, 1f);
            }

            if (Input.GetKeyDown(KeyCode.E) && kilicPlayer.activeSelf)
            {
                kiliciVurdumu = true;
                kilicVurusBoxObje.SetActive(true);
                SesManager.instance.SesEfektiCikar(4);
            }
            else
            {
                kiliciVurdumu = false;
            }


           if (Input.GetKeyDown(KeyCode.E)&& mizrakPlayer.activeSelf) ///////////////////
            {
                mizrakAnim.SetTrigger("mizrakAtti");
                
                Invoke("MizragiFirlat", .5f);
                SesManager.instance.SesEfektiCikar(5);
            }

           if(Input.GetKeyDown(KeyCode.E) && okPlayer.activeSelf && okAtabilirmi)
            {
                okAnim.SetTrigger("okAtti");
                StartCoroutine(OkuAzSonraAtRoutine());
                SesManager.instance.SesEfektiCikar(7);
            }

            if (okPlayer.activeSelf)
            {
                if (GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("TirmanmaLayer")))
                {
                    float h = Input.GetAxis("Vertical");

                    Vector2 tirmanisVector = new Vector2(rb.velocity.x, h * tirmanisHizi);
                    rb.velocity = tirmanisVector;
                    rb.gravityScale = 0f; // merdivenden cikarken yer cekimi olmasin karakter asaya dogru kaymasin
                    okAnim.SetBool("tirmansinmi", true);
                    okAnim.SetFloat("yukariHareketHizi", Mathf.Abs(rb.velocity.y));
                }
                else
                {
                    okAnim.SetBool("tirmansinmi", false);
                    rb.gravityScale = 1f;
                }
            }

        }
        else
        {
            geriTepkiSayaci -= Time.deltaTime;

            if (yonSagdami)
            {
                rb.velocity = new Vector2(-geriTepkiGucu, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(geriTepkiGucu, rb.velocity.y);
            }
        }

        if (normalPlayer.activeSelf)
        {
            NormalAnim.SetBool("zemindemi", zemindemi);
            NormalAnim.SetFloat("HaraketHizi", Mathf.Abs(rb.velocity.x)); //Mathf.Abs ile mutlak deger aliyorum cunku x te - ye dogruda haraket edebiliyorum.
        }
        if (kilicPlayer.activeSelf)
        {
            kilicAnim.SetBool("zemindemi", zemindemi);
            kilicAnim.SetFloat("HaraketHizi", Mathf.Abs(rb.velocity.x)); //Mathf.Abs ile mutlak deger aliyorum cunku x te - ye dogruda haraket edebiliyorum.

        }

        if (mizrakPlayer.activeSelf)
        {
            mizrakAnim.SetBool("zemindemi", zemindemi);
            mizrakAnim.SetFloat("HaraketHizi", Mathf.Abs(rb.velocity.x)); //Mathf.Abs ile mutlak deger aliyorum cunku x te - ye dogruda haraket edebiliyorum.

        }

        if (okPlayer.activeSelf)
        {
            okAnim.SetBool("zemindemi", zemindemi);
            okAnim.SetFloat("HaraketHizi", Mathf.Abs(rb.velocity.x)); //Mathf.Abs ile mutlak deger aliyorum cunku x te - ye dogruda haraket edebiliyorum.

        }

        if (kiliciVurdumu && kilicPlayer.activeSelf)
        {
            kilicAnim.SetTrigger("kiliciVurdu");
        }




    }

    void MizragiFirlat()
    {
        GameObject mizrak = Instantiate(atilacakMizrak, mizrakCikisNoktasi.position, mizrakCikisNoktasi.rotation);
        mizrak.transform.localScale = transform.localScale; // karakterle ayni local scale cunku her iki yonede firlatabilmem lazim
        mizrak.GetComponent<Rigidbody2D>().velocity = mizrakCikisNoktasi.right * transform.localScale.x * 7f;

        Invoke("HerseyiKapatNormaliAc", .1f);

        
    }

    IEnumerator OkuAzSonraAtRoutine()
    {
        okAtabilirmi = false;
        yield return new WaitForSeconds(.7f);
        OkPoolManager.instance.OkuFirlatFNC(okCikisNoktasi, this.transform);
        okAtabilirmi = true;
    }

    

    void HareketEt()
    {
        float h = Input.GetAxis("Horizontal"); //0 ve 1 arasinda deger aliyor otomatik atanmistir yon tuslari ya da a d ile deger alir
        rb.velocity = new Vector2(h * haraketHizi, rb.velocity.y);
        

        
    }

    void YonuDegistirFNC() {
        if (rb.velocity.x < 0) //eger hiz -x yonunde gidiyorsa sol button or a yani
        {
            transform.localScale = new Vector3(-1, 1, 1); //karakterin x konumunda diger tarafa flip at
            yonSagdami = false;
        }
        else if (rb.velocity.x > 0)
        {
            transform.localScale = Vector3.one; // one (1,1,1) demek zaten // sifirdan buyukse saga gidiyo yani d ya da sag buttona basiyo oraya dogru cevir dedik.
            yonSagdami = true;
        }
    }


    void ZiplaFNC()
    {                            //burdan cikar isini            //buraya carparsa
        zemindemi = Physics2D.OverlapCircle(zeminKontrolNoktasi.position, .2f, zeminMaske); //TRUE FALSE DONDU

        if(Input.GetButtonDown("Jump") && (zemindemi || ikinciKezZiplasinmi)) //GetButtonDown("Jump") demek space tusu demek.   horizantal tagi nasil a d sag golu anliyosa Jumpta space i anliyor tanimli
        {
            if (zemindemi)
            {
                ikinciKezZiplasinmi = true;
            }
            else
            {
                ikinciKezZiplasinmi = false;
            }


            rb.velocity = new Vector2(rb.velocity.x, ziplamaGucu);

            
        }

    }

    public void GeriTepkiFNC()
    {
        geriTepkiSayaci = geriTepkiSuresi;


        if (normalPlayer.activeSelf)
        {
            normalSprite.color = new Color(normalSprite.color.r, normalSprite.color.g, normalSprite.color.b, .5f);
        }
        if (kilicPlayer.activeSelf)
        {
            kilicSprite.color = new Color(kilicSprite.color.r, kilicSprite.color.g, kilicSprite.color.b, .5f);
        }

        if (mizrakPlayer.activeSelf)
        {
            mizrakSprite.color = new Color(mizrakSprite.color.r, mizrakSprite.color.g, mizrakSprite.color.b, .5f);
        }

        if (okPlayer.activeSelf)
        {
            okSprite.color = new Color(okSprite.color.r, okSprite.color.g, okSprite.color.b, .5f);
        }


        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    public void PlayerCanVerdiFNC()
    {
        rb.velocity = Vector2.zero;
        playerCanverdimi = true;

        if (normalPlayer.activeSelf)
        {
            NormalAnim.SetTrigger("canVerdi"); // Animasyonu tetikle
        }
        if (kilicPlayer.activeSelf)
        {
            kilicAnim.SetTrigger("canVerdi"); // Animasyonu tetikle
        }
        if (mizrakPlayer.activeSelf)
        {
            mizrakAnim.SetTrigger("canVerdi"); // Animasyonu tetikle
        }
        if (okPlayer.activeSelf)
        {
            okAnim.SetTrigger("canVerdi"); // Animasyonu tetikle
        }




        StartCoroutine(PlayerYokEtSahneYenile());
    }

    IEnumerator PlayerYokEtSahneYenile()
    {
        yield return new WaitForSeconds(2f);

        //  Destroy(gameObject); // BOYLE YAPARSAN SCRIPTIDE SILMIS OLUYON CUNKU ICINDE SCRIPT VAR

        GetComponentInChildren<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void HerseyiKapatKilicAc()
    {
        TumKameralariKapat();
        kilicKamera.SetActive(true);

        normalPlayer.SetActive(false);
        mizrakPlayer.SetActive(false);
        kilicPlayer.SetActive(true);
        okPlayer.SetActive(false);

    }

    public void HerseyiKapatMizrakAc()
    {
        TumKameralariKapat();
        mizrakKamera.SetActive(true);

        normalPlayer.SetActive(false);
        kilicPlayer.SetActive(false);
        mizrakPlayer.SetActive(true);
        okPlayer.SetActive(false);
    }

    public void HerseyiKapatNormaliAc()
    {
        TumKameralariKapat();
        normalKamera.SetActive(true);
       
        normalPlayer.SetActive(true);
        kilicPlayer.SetActive(false);
        mizrakPlayer.SetActive(false);
        okPlayer.SetActive(false);

    }
    public void HerseyiKapatOkuAc()
    {
        TumKameralariKapat();
        okKamera.SetActive(true);

        normalPlayer.SetActive(false);
        kilicPlayer.SetActive(false);
        mizrakPlayer.SetActive(false);
        okPlayer.SetActive(true);

    }

    void TumKameralariKapat()
    {
        normalKamera.SetActive(false);
        kilicKamera.SetActive(false);
        mizrakKamera.SetActive(false);
        okKamera.SetActive(false);
    }

    public void PlayerHareketsizYap()
    {

        if (normalPlayer.activeSelf)
        {
            rb.velocity = Vector2.zero;
            NormalAnim.SetFloat("HaraketHizi", 0f);
        }
        if (kilicPlayer.activeSelf)
        {
            rb.velocity = Vector2.zero;
            kilicAnim.SetFloat("HaraketHizi", 0f);
        }
        if (mizrakPlayer.activeSelf)
        {
            rb.velocity = Vector2.zero;
            mizrakAnim.SetFloat("HaraketHizi", 0f);
        }
        if (okPlayer.activeSelf)
        {
            rb.velocity = Vector2.zero;
            okAnim.SetFloat("HaraketHizi", 0f);
        }

    }


}
