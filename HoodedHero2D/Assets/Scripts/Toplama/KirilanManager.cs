using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KirilanManager : MonoBehaviour
{
    [SerializeField]
    bool sandikmi, korkulukmu;

    Animator anim;

    int kacinciVurus;

    [SerializeField]
    GameObject parlamaEfekti;


    [SerializeField]
    GameObject coinPrefab;

    Vector2 patlamaMiktari = new Vector2(1, 4);

    private void Awake()
    {
        anim = GetComponent<Animator>();
        kacinciVurus = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("kilicVurusBox"))
        {
            if (sandikmi)
            {

                if (kacinciVurus == 0)
                {
                    anim.SetTrigger("sallanma");
                    Instantiate(parlamaEfekti, transform.position, transform.rotation);
                }
                else if (kacinciVurus == 1)
                {
                    Instantiate(parlamaEfekti, transform.position, transform.rotation);

                    GetComponent<BoxCollider2D>().enabled = false;
                    anim.SetTrigger("parcalanma");

                    for (int i = 0; i < 3; i++) // Kasa icindeki altinlari saga sola sicratmak icin yazdim
                    {
                        Vector3 rageleVector = new Vector3(transform.position.x + (i - 1), transform.position.y, transform.position.z);

                        GameObject coin = Instantiate(coinPrefab, transform.position, transform.rotation);

                        coin.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

                        coin.GetComponent<Rigidbody2D>().velocity = patlamaMiktari * new Vector2(Random.Range(1, 2), transform.localScale.y + Random.Range(0, 2));
                    }
                }
                kacinciVurus++;
            }

            if (korkulukmu)
            {

                if (kacinciVurus == 0)
                {
                    
                    Instantiate(parlamaEfekti, transform.position, transform.rotation);
                }
                else if (kacinciVurus == 1)
                {
                    Instantiate(parlamaEfekti, transform.position, transform.rotation);

                    GetComponent<BoxCollider2D>().enabled = false;
                   

                    for (int i = 0; i < 3; i++) // Kasa icindeki altinlari saga sola sicratmak icin yazdim
                    {
                        Vector3 rageleVector = new Vector3(transform.position.x + (i - 1), transform.position.y, transform.position.z);

                        GameObject coin = Instantiate(coinPrefab, transform.position, transform.rotation);

                        coin.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

                        coin.GetComponent<Rigidbody2D>().velocity = patlamaMiktari * new Vector2(Random.Range(1, 2), transform.localScale.y + Random.Range(0, 2));
                    }
                    Destroy(gameObject);
                }
                kacinciVurus++;
            }


        }
    }
}
