using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHaraketController : MonoBehaviour
{

    public static PlayerHaraketController instance;
    
    Rigidbody2D rb;

    [SerializeField]
    GameObject normalPlayer, kilicPlayer;

    [SerializeField]
    Transform zeminKontrolNoktasi;

    [SerializeField]
    Animator NormalAnim, kilicAnim;  //animastonlarimi kontrol etmek icin aliyorum ve orda olusturdugum fonksyonlara erisip kullanacagim.

    [SerializeField]
    SpriteRenderer normalSprite, kilicSprite;

    public LayerMask zeminMaske; // Tag sectirir gibi 2 d oldugu icin layer sectiriyor bize public bir sekilde

    public float haraketHizi;
    public float ziplamaGucu;

    bool zemindemi;
    bool ikinciKezZiplasinmi;

    [SerializeField]
    float geriTepkiSuresi, geriTepkiGucu;
    float geriTepkiSayaci;

    bool yonSagdami;

    bool playerCanverdimi;

    private void Awake()
    {
        instance = this;

        rb = GetComponent<Rigidbody2D>();
        playerCanverdimi = false;
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

            normalSprite.color = new Color(normalSprite.color.r, normalSprite.color.g, normalSprite.color.b, 1f); 
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

        normalSprite.color = new Color(normalSprite.color.r, normalSprite.color.g, normalSprite.color.b, 0.5f); // karakteri dmg yerse opaklastir.
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
}
