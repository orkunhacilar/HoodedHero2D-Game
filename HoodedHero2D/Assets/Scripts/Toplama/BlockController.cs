using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public Transform altPoint;

    Animator anim;

    Vector3 hareketYonu = Vector3.up;
    Vector3 orjinalPos;
    Vector3 animPos;

    public LayerMask playerLayer;

    bool animasyonBaslasinmi;
    bool hareketEtsinmi = true;

    public GameObject coinPrefab;
    Vector3 coinPos;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        orjinalPos = transform.position;
        animPos = transform.position;
        animPos.y += .15f;

        coinPos = transform.position;
        coinPos.y += 1f; // blogun icinde coini 1f kadar y de yukari cikar
    }

    private void Update()
    {
        CarpismayiKontrolEt();
        AnimasyonuBaslat();
    }

    void CarpismayiKontrolEt()
    {
        if (hareketEtsinmi)
        {
            RaycastHit2D hit = Physics2D.Raycast(altPoint.position, Vector2.down, .1f, playerLayer); //isin yollama

            // Debug.DrawRay(altPoint.position, Vector2.down, Color.green); //ISINI YAZDIRMA GIBI KONTROL ETME SCENE EKRANINDAN BAKILIR !

            if (hit && hit.collider.gameObject.tag == "Player")
            {
                anim.Play("Mat");
                animasyonBaslasinmi = true;
                hareketEtsinmi = false;

                Instantiate(coinPrefab, coinPos, Quaternion.identity); // yarat dedik bu nesneyi bu pozisyonda  // Quaternion.identity -> rotasyonsuz yarat


            }
        }
    }

    void AnimasyonuBaslat()
    {
        if (animasyonBaslasinmi){
            transform.Translate(hareketYonu * Time.smoothDeltaTime); // nesneleri yavas sekilde yukarı kaldırır sen kafa atınca

            if (transform.position.y >= animPos.y)
            {
                hareketYonu = Vector3.down;
            } else if (transform.position.y <= orjinalPos.y)
            {
                animasyonBaslasinmi = false;
            }
        }


    }

}
