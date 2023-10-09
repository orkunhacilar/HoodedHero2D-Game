using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    PlayerHaraketController player;

    private void Awake()
    {
        player = Object.FindObjectOfType<PlayerHaraketController>();
    }

    private void Update()
    {
        if (player != null)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z); //Kamera Karakteri izlesin.
        }
    }
}
