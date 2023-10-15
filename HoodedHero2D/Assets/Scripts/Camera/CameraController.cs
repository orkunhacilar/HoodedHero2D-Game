using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    PlayerHaraketController player;

    [SerializeField]
    Collider2D boundsBox;

    float halfYukseklik, halfGenislik;

   // T Vector2 sonPos;
    [SerializeField]
    Transform backgrounds;

    private void Awake()
    {
        player = Object.FindObjectOfType<PlayerHaraketController>();
    }

    private void Start()
    {
        halfYukseklik = Camera.main.orthographicSize; //kameranin ortasindan yukardaki uc sınırına olan uzaklık

        halfGenislik = halfYukseklik * Camera.main.aspect;

      // T sonPos = transform.position;


    }

    private void Update()
    {
        if (player != null)
        {
            transform.position = new Vector3(
                Mathf.Clamp(player.transform.position.x,boundsBox.bounds.min.x+halfGenislik,boundsBox.bounds.max.x-halfGenislik), //Clamp Sınırlamalar için kullandığımız fonksyon
                Mathf.Clamp(player.transform.position.y,boundsBox.bounds.min.y+halfYukseklik,boundsBox.bounds.max.y-halfYukseklik),
                transform.position.z); //Kamera Karakteri izlesin.

        }

        if (backgrounds != null)
        {
           backgrounds.transform.position = new Vector3(transform.position.x, transform.position.y+4, backgrounds.transform.position.z); // Arka plan kamera ile gelsin
        }




    }

     
    
}
