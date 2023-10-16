using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttakControler : MonoBehaviour
{
    [SerializeField]
    BoxCollider2D kilicVurusBox;

    [SerializeField]
    GameObject parlamaEfekti;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (kilicVurusBox.IsTouchingLayers(LayerMask.GetMask("DusmanLayer")))
        {
            if (collision.CompareTag("Orumcek"))
            {
                if (parlamaEfekti)
                {
                    Instantiate(parlamaEfekti, collision.transform.position, Quaternion.identity); ;
                }

                StartCoroutine(collision.GetComponent<OrumcekController>().GeriTepkiFNC());
            }
        }

        if (kilicVurusBox.IsTouchingLayers(LayerMask.GetMask("DusmanLayer")))
        {
            if (collision.CompareTag("Bat"))
            {
                if (parlamaEfekti)
                {
                    Instantiate(parlamaEfekti, collision.transform.position, Quaternion.identity); // vurdugumda efekt olussun
                }
                collision.GetComponent<BatController>().CaniAzaltFNC();
            }
        }

        if (kilicVurusBox.IsTouchingLayers(LayerMask.GetMask("iskeletLayer")))
        {
            if (collision.CompareTag("iskelet"))
            {
                if (parlamaEfekti)
                {
                    Instantiate(parlamaEfekti, collision.transform.position, Quaternion.identity); // vurdugumda efekt olussun
                }
                collision.GetComponent<IskeletHealthController>().CaniAzaltFNC();
            }
        }
    }
}
