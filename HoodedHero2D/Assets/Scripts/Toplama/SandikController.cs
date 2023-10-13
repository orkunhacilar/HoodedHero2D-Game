using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandikController : MonoBehaviour
{
    Animator anim;

    int kacinciVurus;

    [SerializeField]
    GameObject parlamaEfekti;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        kacinciVurus = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("kilicVurusBox"))
        {
            if(kacinciVurus == 0)
            {
                anim.SetTrigger("sallanma");
                Instantiate(parlamaEfekti, transform.position, transform.rotation);
            }
            else if (kacinciVurus == 1)
            {
                Instantiate(parlamaEfekti, transform.position, transform.rotation);
                anim.SetTrigger("parcalanma");
            }
            kacinciVurus++;
        }
    }
}
