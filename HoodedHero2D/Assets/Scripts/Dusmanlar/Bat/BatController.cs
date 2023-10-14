using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{

    [SerializeField]
    float takipMesafesi = 8f;

    [SerializeField]
    float batHiz;

    [SerializeField]
    Transform hedefPlayer;

    [SerializeField]
    GameObject iksirPrefab;

    Animator anim;
    Rigidbody2D rb;

    BoxCollider2D batCollider;

    public float atakYapmaSureri;
    float atakYapmaSayac;
    float mesafe;

    Vector2 haraketYonu;

   
    public int maxSaglik;

    public int gecerliSaglik;

    private void Awake()
    {
        hedefPlayer = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        batCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        gecerliSaglik = maxSaglik;
    }

    private void Update()
    {
        if (atakYapmaSayac < 0)
        {

            if(hedefPlayer && gecerliSaglik>0 && !PlayerHaraketController.instance.playerCanverdimi)
            {
                mesafe = Vector2.Distance(transform.position, hedefPlayer.position);

                if (mesafe < takipMesafesi)
                {
                    anim.SetTrigger("ucusaGecti");

                    haraketYonu = hedefPlayer.position - transform.position; //yarasanin adami takip etmesi icin


                    // YARASANIN TAKIP EDERKENKI SCALE DEGERINI ( YON ) DEGISTIRME 
                    if (transform.position.x > hedefPlayer.position.x)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    else if (transform.position.x < hedefPlayer.position.x)
                    {
                        transform.localScale = Vector3.one;
                    }



                    rb.velocity = haraketYonu * batHiz; //takip etme hizi
                }
            }

            
        }
        else
        {
            atakYapmaSayac -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (batCollider.IsTouchingLayers(LayerMask.GetMask("PlayerLayer")))
        {
            if (collision.CompareTag("Player"))
            {
                rb.velocity = Vector2.zero;
                atakYapmaSayac = atakYapmaSureri;
                anim.SetTrigger("atakYapti");

                collision.GetComponent<PlayerHaraketController>().GeriTepkiFNC();
                collision.GetComponent<PlayerHealthController>().CaniAzaltFNC();

            }
        }
    }

    public void CaniAzaltFNC()
    {
        gecerliSaglik--;
        atakYapmaSayac = atakYapmaSureri;

        rb.velocity = Vector2.zero;

        if(gecerliSaglik <= 0)
        {
            gecerliSaglik = 0;
            batCollider.enabled = false;

            Instantiate(iksirPrefab, transform.position, Quaternion.identity);

            anim.SetTrigger("canVerdi");
            Destroy(gameObject, 3f);
        }
    }



    private void OnDrawGizmosSelected() // tanimli metod
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, takipMesafesi);
      
       
    }



}
