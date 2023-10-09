using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHaraketController : MonoBehaviour
{

    
    Rigidbody2D rb;

    [SerializeField]
    Transform zeminKontrolNoktasi;

    [SerializeField]
    Animator anim;  //animastonlarimi kontrol etmek icin aliyorum ve orda olusturdugum fonksyonlara erisip kullanacagim.

    public LayerMask zeminMaske; // Tag sectirir gibi 2 d oldugu icin layer sectiriyor bize public bir sekilde

    public float haraketHizi;
    public float ziplamaGucu;

    bool zemindemi;
    bool ikinciKezZiplasinmi;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HareketEt();
        ZiplaFNC();

        anim.SetBool("zemindemi", zemindemi);
        anim.SetFloat("HaraketHizi", Mathf.Abs(rb.velocity.x)); //Mathf.Abs ile mutlak deger aliyorum cunku x te - ye dogruda haraket edebiliyorum.
    }

    void HareketEt()
    {
        float h = Input.GetAxis("Horizontal"); //0 ve 1 arasinda deger aliyor otomatik atanmistir yon tuslari ya da a d ile deger alir
        rb.velocity = new Vector2(h * haraketHizi, rb.velocity.y);

        if (rb.velocity.x < 0) //eger hiz -x yonunde gidiyorsa sol button or a yani
        {
            transform.localScale = new Vector3(-1, 1, 1); //karakterin x konumunda diger tarafa flip at
        }else if (rb.velocity.x > 0)
        {
            transform.localScale = Vector3.one; // one (1,1,1) demek zaten // sifirdan buyukse saga gidiyo yani d ya da sag buttona basiyo oraya dogru cevir dedik.
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
}