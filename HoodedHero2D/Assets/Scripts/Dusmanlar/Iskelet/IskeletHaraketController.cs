using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IskeletHaraketController : MonoBehaviour
{
    [SerializeField]
    Transform[] pozisyonlar;

    int kacinciPozisyon;

    [SerializeField]
    float iskeletHizi = 4f;

    [SerializeField]
    float beklemeSuresi = 1f;
    float bekelemeSayac;

    Transform PlayerHedef;

    Rigidbody2D rb;

    Animator anim;

    bool sinirIcindemi;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sinirIcindemi = false;
    }

    private void Start()
    {
        bekelemeSayac = beklemeSuresi;
        PlayerHedef = GameObject.Find("Player").transform; //sahnemde ismi player olan seyi bul.

        foreach (Transform pos in pozisyonlar)
        {
            pos.parent = null;
        }
    }

    private void Update()
    {

        if (PlayerHedef.GetComponent<PlayerHaraketController>().playerCanverdimi || GetComponent<IskeletHealthController>().iskeletOldumu)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            anim.SetBool("atakYapti", false);
            return;
        }


        float mesafe = Vector2.Distance(PlayerHedef.position, transform.position);

        // print(mesafe);

        if (mesafe > 4)
        {
            sinirIcindemi = false;
        }
        else
        {
            sinirIcindemi = true;
        }

        if (!sinirIcindemi) //karakter uzaktaysa yani kisaca
        {
            if (Mathf.Abs(transform.position.x - pozisyonlar[kacinciPozisyon].position.x) > 0.2f) // iskelet ile masafe arasinda uzaklik varsa
            {
                if (transform.position.x < pozisyonlar[kacinciPozisyon].position.x) // pozisyon sagda iskelet solda kaldiysa
                {
                    rb.velocity = new Vector2(iskeletHizi, rb.velocity.y); // iskelete saga gidicek derecede + x kuveeti veriyoruz
                }
                else
                {
                    rb.velocity = new Vector2(-iskeletHizi, rb.velocity.y); // tam tersi iskelet sola gitsin
                }
               // print(Mathf.Sign(rb.velocity.x));
                transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f); //Sign isaret olarak almama yariyo yani scale 1 -1 gibi degeri aliyo okuyo gibi dusun.

            }
            else //iskelet artik pos123 neyse ona ulastiysa
            {
                rb.velocity = new Vector2(0, rb.velocity.y); //x i durdur beklesin yani
                bekelemeSayac -= Time.deltaTime; // soyledigim saniye kadar burda dursun

                if (bekelemeSayac <= 0) // ve beklemesayacim artik 0 a dustugunde
                {
                    bekelemeSayac = beklemeSuresi; // sayacimi gene guncelliyorum hemen
                    kacinciPozisyon++; // pozisyonumu degistiriyorum
                    if (kacinciPozisyon >= pozisyonlar.Length)       //son pos geldikten sonra dahasi olmadigi icin sifirliyoruz.
                        kacinciPozisyon = 0;
                }
            }
        }
        else // Karakterim Sinir Icine Girerse
        {
            Vector2 yonVectoru = transform.position - PlayerHedef.position; // nereye dogru gitcegini hemen bir cikarma islemi ile anliyoruz.

            if(yonVectoru.magnitude > 1.5f && PlayerHedef != null) //yon vectorunun uzunlugunu olcuyorum(magnitude) cok fazla yaklasmasini istemiyorum cunku
            {
                if (yonVectoru.x > 0) // x + cikarsa ne demek. iskeletin x'inden playeri cikardim ve hala + geliyosa.Demek ki iskeletin x daha buyuk. bu da iskelet sag player solda demek
                {
                    rb.velocity = new Vector2(-iskeletHizi, rb.velocity.y); // o zaman iskelet sola dogru yani playera  dogru gitsin diyorum.

                }
                else
                {
                    rb.velocity = new Vector2(iskeletHizi, rb.velocity.y); //tersinde zaten saga gitsin diyorum
                }
                anim.SetBool("atakYapti", false); // uzaktaysa kapat
                transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f); // oku benim Hız degerimi + mı veriyom sana - mı ona göre scale'imi otamatik olarak cevir.
            }
            else // aradai mesafe 1.5f olursa falan demistik yukardaki if'de. Burda da iyice bana yanasirsa iskelet dursun istiyorum.
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                anim.SetBool("atakYapti", true); //yakindaysa ac
            }
        }

        anim.SetFloat("hareketHizi", Mathf.Abs(rb.velocity.x));
       
    }
}
