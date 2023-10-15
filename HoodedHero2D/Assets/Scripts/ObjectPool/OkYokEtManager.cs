using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OkYokEtManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ok"))
        {
            collision.gameObject.SetActive(false);
        }
    }
}
