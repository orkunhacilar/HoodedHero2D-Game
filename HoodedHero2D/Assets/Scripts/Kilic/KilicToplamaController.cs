using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KilicToplamaController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            collision.GetComponent<PlayerHaraketController>().TakeSword();

            Destroy(gameObject);
        }
        
    }


    
}
