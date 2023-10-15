using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAraclarContoller : MonoBehaviour
{

    [SerializeField]
    bool kilicmi, mizrakmi;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            if (collision != null && kilicmi)
            {
                collision.GetComponent<PlayerHaraketController>().TakeSword();

            }


            if (collision != null && mizrakmi)
            {

                collision.GetComponent<PlayerHaraketController>().HerseyiKapatMizrakAc(); 
            }
            Destroy(gameObject);
        }
        
    }


    
}
