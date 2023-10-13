using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrumcekController : MonoBehaviour
{
    [SerializeField]
    Transform[] pozisyonlar;

    public float orumcekHizi;

    public float beklemeSuresi;
    float beklemeSayac;

    Animator anim;

    int kacinciPozisyon;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        foreach (Transform pos in pozisyonlar)
        {
            pos.parent = null; //pozisyonlari o objeden disari cikariyo ki orumcekle beraber pozisyonlar degismesin. // parentini null demek pos1 pos2 yi orumcek gameobjesinin disina al demek

        }
    }

    private void Update()
    {
        if (beklemeSayac > 0)
        {
            //orumcek verilen noktada duruyor
            beklemeSayac -= Time.deltaTime; // zamanla o degeri geriye dogru say
            anim.SetBool("hareketEtsinmi", false);

        }
        else
        {
            anim.SetBool("hareketEtsinmi", true);

            if (transform.position.x > pozisyonlar[kacinciPozisyon].position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if(transform.position.x < pozisyonlar[kacinciPozisyon].position.x)
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

    void PozisyonuDegistir()
    {
        kacinciPozisyon++;

        if (kacinciPozisyon >= pozisyonlar.Length)
            kacinciPozisyon = 0;
    }
}
