using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoaController : MonoBehaviour
{
    [SerializeField]
    float boaYurumeHizi, boaKosmaHizi;

    Animator anim;
    Rigidbody2D rb;

    [SerializeField]
    float gorusMesafesi = 8f;

    [SerializeField]
    BoxCollider2D boaCollider;

    public bool oldumu;

    public LayerMask playerLayer;

    [SerializeField]
    GameObject kanamaEfekti;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        oldumu = false;
    }

    private void Update()
    {
        if (oldumu)
            return;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), gorusMesafesi, playerLayer);

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left) * gorusMesafesi, Color.green);

        transform.localScale = new Vector3(-1, 1, 1);

        if (hit.collider)
        {
            if (hit.collider.CompareTag("Player"))
            {
                rb.velocity = new Vector2(-boaKosmaHizi, rb.velocity.y);
                anim.SetBool("kossunmu", true);
            }

        }
        else
        {
            rb.velocity = new Vector2(-boaYurumeHizi, rb.velocity.y);
            anim.SetBool("kossunmu", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (boaCollider.IsTouchingLayers(LayerMask.GetMask("PlayerLayer")))
        {
            if (collision.CompareTag("Player"))
            {
                anim.SetTrigger("atakYapti");

                collision.GetComponent<PlayerHaraketController>().GeriTepkiFNC();
                collision.GetComponent<PlayerHealthController>().CaniAzaltFNC();
            }
        }
    }

    public void BoaOldu()
    {
        oldumu = true;
        anim.SetTrigger("canVerdi");
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

        Instantiate(kanamaEfekti, transform.position, transform.rotation);

        foreach (BoxCollider2D box in GetComponents<BoxCollider2D>()) // boanin icinde 2 tane box collider var diye bu sekilde kapatabiliriz.
        {
            box.enabled = false;
            
        }
        Destroy(gameObject, 3f);
    }
}
