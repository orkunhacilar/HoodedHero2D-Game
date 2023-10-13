using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiliciPasifYap : MonoBehaviour
{
    public GameObject kilicVurusBox;
   // public GameObject kilicSprite;

    public void KiliciKapat()
    {
        kilicVurusBox.SetActive(false);
      //  kilicSprite.SetActive(false);
    }
}
