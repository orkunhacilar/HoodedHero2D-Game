using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalinanEngelController : MonoBehaviour
{
    [SerializeField]
    float donmeHizi = 200f;

    float zAngle;

    [SerializeField]
    float minZAngle = -75f;

    [SerializeField]
    float maxZAngle = 75f;

    private void Start() // Random olsun diye sagdan solami soldan sagami donsun diye yazdim.
    {
        if (Random.Range(0, 2) > 0)
            donmeHizi *= -1;
    }

    private void Update()
    {
        zAngle += Time.deltaTime * donmeHizi;
        transform.rotation = Quaternion.AngleAxis(zAngle, Vector3.forward); // 2D objesini rotation seklinde dondurme



        // BELLI POZISYONLARA GORE SOLA SAGA SALLANMASI ICIN YAZDIK.
        if (zAngle < minZAngle)
        {
            donmeHizi = Mathf.Abs(donmeHizi); // mutlak deger
        }

        if(zAngle > maxZAngle)
        {
            donmeHizi = -Mathf.Abs(donmeHizi);
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GetComponent<EdgeCollider2D>().IsTouchingLayers(LayerMask.GetMask("PlayerLayer")))
        {
            if(collision.CompareTag("Player") && !collision.GetComponent<PlayerHaraketController>().playerCanverdimi)
            {
                collision.GetComponent<PlayerHaraketController>().GeriTepkiFNC();
                collision.GetComponent<PlayerHealthController>().CaniAzaltFNC();
            }
        }
    }
}
