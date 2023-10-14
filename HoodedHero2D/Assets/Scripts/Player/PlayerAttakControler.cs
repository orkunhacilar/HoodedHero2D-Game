using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttakControler : MonoBehaviour
{
    [SerializeField]
    BoxCollider2D kilicVurusBox;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (kilicVurusBox.IsTouchingLayers(LayerMask.GetMask("DusmanLayer")))
        {
            if (collision.CompareTag("Orumcek"))
            {
                StartCoroutine(collision.GetComponent<OrumcekController>().GeriTepkiFNC());
            }
        }
    }
}
