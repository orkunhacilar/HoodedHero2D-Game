using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MizrakController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Boa") && !collision.GetComponent<BoaController>().oldumu)
        {
            Destroy(gameObject);
            collision.GetComponent<BoaController>().BoaOldu();
        }
    }
}
