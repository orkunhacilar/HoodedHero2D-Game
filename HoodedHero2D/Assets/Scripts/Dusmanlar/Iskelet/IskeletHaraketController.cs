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

        if(Mathf.Abs(transform.position.x - pozisyonlar[kacinciPozisyon].position.x) > 0.2f)
        {
            if(transform.position.x < pozisyonlar[kacinciPozisyon].position.x)
            {
                rb.velocity = new Vector2(iskeletHizi, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-iskeletHizi, rb.velocity.y);
            }
            print(Mathf.Sign(rb.velocity.x));
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);

        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            bekelemeSayac -= Time.deltaTime;

            if(bekelemeSayac < 0)
            {
                bekelemeSayac = beklemeSuresi;
                kacinciPozisyon++;
                if (kacinciPozisyon >= pozisyonlar.Length)
                    kacinciPozisyon = 0;
            } 
        }
    }
}
