using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToplamaManager : MonoBehaviour
{

    [SerializeField]
    bool coinmi;


    [SerializeField]
    GameObject coinEfekt;

    bool toplandimi;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !toplandimi)
        {
            toplandimi = true;

            GameManager.instance.toplananCointAdet++;

            UiManager.instance.CoinAdetGuncelle();

            Destroy(gameObject);
            Instantiate(coinEfekt, transform.position, Quaternion.identity);
        }
    }

}
