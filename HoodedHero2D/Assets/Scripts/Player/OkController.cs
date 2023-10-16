using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OkController : MonoBehaviour
{
    [SerializeField]
    GameObject parlamaEfect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("iskeletLayer")))
        {
            if (collision.CompareTag("iskelet"))
            {
                gameObject.SetActive(false);
                Instantiate(parlamaEfect, collision.transform.position, collision.transform.rotation);
                collision.GetComponent<IskeletHealthController>().CaniAzaltFNC();
            }
        }
    }
}
