using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   

    

    float halfYukseklik, halfGenislik;

   // T Vector2 sonPos;
    [SerializeField]
    Transform backgrounds;


    private void Start()
    {
        halfYukseklik = Camera.main.orthographicSize; //kameranin ortasindan yukardaki uc sınırına olan uzaklık

        halfGenislik = halfYukseklik * Camera.main.aspect;

      // T sonPos = transform.position;


    }

    private void Update()
    {
        

        if (backgrounds != null)
        {
           backgrounds.transform.position = new Vector3(transform.position.x, transform.position.y+4, backgrounds.transform.position.z); // Arka plan kamera ile gelsin
        }




    }

     
    
}
