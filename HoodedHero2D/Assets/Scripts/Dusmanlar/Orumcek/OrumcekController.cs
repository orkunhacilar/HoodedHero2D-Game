using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrumcekController : MonoBehaviour
{
    [SerializeField]
    Transform[] pozisyonlar;

    [SerializeField]
    Slider orumcekSlider;

    public int maxSaglik;
    int gecerliSaglik;

    public float orumcekHizi;

    public float beklemeSuresi;
    float beklemeSayac;

    Animator anim;

    int kacinciPozisyon;

    Transform hedefPlayer;

    public float takipMesafesi = 5f;

    BoxCollider2D orumcekCollider;

    bool atakYapabilirmi;

    Rigidbody2D rb;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        orumcekCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        gecerliSaglik = maxSaglik;


        orumcekSlider.maxValue = maxSaglik;
        SliderGuncelle();

        atakYapabilirmi = true;
        hedefPlayer = GameObject.Find("Player").transform;
        
        foreach (Transform pos in pozisyonlar)
        {
            pos.parent = null; //pozisyonlari o objeden disari cikariyo ki orumcekle beraber pozisyonlar degismesin. // parentini null demek pos1 pos2 yi orumcek gameobjesinin disina al demek

        }
    }

    private void Update()
    {
        if (!atakYapabilirmi)
            return;

        if (beklemeSayac > 0)
        {
            //orumcek verilen noktada duruyor
            beklemeSayac -= Time.deltaTime; // zamanla o degeri geriye dogru say
            anim.SetBool("hareketEtsinmi", false);

        }
        else
        {
            if(hedefPlayer.position.x > pozisyonlar[0].position.x && hedefPlayer.position.x < pozisyonlar[1].position.x)
            {
                transform.position = Vector3.MoveTowards(transform.position, hedefPlayer.position, orumcekHizi * Time.deltaTime);

                anim.SetBool("hareketEtsinmi", true);

                if (transform.position.x > hedefPlayer.position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else if (transform.position.x < hedefPlayer.position.x)
                {
                    transform.localScale = Vector3.one;

                }
            }
            else
            {

                anim.SetBool("hareketEtsinmi", true);

                if (transform.position.x > pozisyonlar[kacinciPozisyon].position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else if (transform.position.x < pozisyonlar[kacinciPozisyon].position.x)
                {
                    transform.localScale = Vector3.one;

                }

                transform.position = Vector3.MoveTowards(transform.position, pozisyonlar[kacinciPozisyon].position, orumcekHizi * Time.deltaTime);

                if (Vector3.Distance(transform.position, pozisyonlar[kacinciPozisyon].position) < 0.1f)
                {
                    beklemeSayac = beklemeSuresi;
                    PozisyonuDegistir();
                }
            }

        }
    }

    void PozisyonuDegistir()
    {
        kacinciPozisyon++;

        if (kacinciPozisyon >= pozisyonlar.Length)
            kacinciPozisyon = 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, takipMesafesi);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (orumcekCollider.IsTouchingLayers(LayerMask.GetMask("PlayerLayer")) && atakYapabilirmi) //Orumcek collision carparsa layermaski PlayerLayer Olan birine
        {
            atakYapabilirmi = false;
            anim.SetTrigger("atakYapti");
            collision.GetComponent<PlayerHaraketController>().GeriTepkiFNC();
            collision.GetComponent<PlayerHealthController>().CaniAzaltFNC();

            StartCoroutine(YenidenAtakYapsin());
        }
    }

    IEnumerator YenidenAtakYapsin()
    {
        yield return new WaitForSeconds(1f);
       if(gecerliSaglik>0)
            atakYapabilirmi = true;
    }

    public IEnumerator GeriTepkiFNC()
    {
        atakYapabilirmi = false;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(.1f);

        gecerliSaglik--;

        SliderGuncelle();

        if (gecerliSaglik <= 0)
        {
            atakYapabilirmi = false;
            gecerliSaglik = 0;
            anim.SetTrigger("canVerdi");
            orumcekCollider.enabled = false; // box collideri kapatma
            orumcekSlider.gameObject.SetActive(false); // *
            Destroy(gameObject, 2f);
        }
        else
        {

            for(int i=0; i<5; i++)
            {
                rb.velocity = new Vector2(-transform.localScale.x + i, rb.velocity.y); // orumcegi vurduktan sonra onu geriye dogru firlatma
                yield return new WaitForSeconds(0.05f);
            }


            anim.SetBool("hareketEtsinmi", false);

            yield return new WaitForSeconds(0.25f);

            rb.velocity = Vector2.zero;
            atakYapabilirmi = true;
        }
    }

    void SliderGuncelle()
    {
        orumcekSlider.value = gecerliSaglik;
    }
}
