using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{

    public static PlayerHealthController instance;

    public int maxSaglik, gecerliSaglik;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gecerliSaglik = maxSaglik;

        UiManager.instance.SlideriGuncelle(gecerliSaglik, maxSaglik);
   
    }


    public void CaniAzaltFNC()
    {
        gecerliSaglik--;
        UiManager.instance.SlideriGuncelle(gecerliSaglik, maxSaglik);

        if (gecerliSaglik <= 0)
        {
            gecerliSaglik = 0;
            //  gameObject.SetActive(false);

            PlayerHaraketController.instance.PlayerCanVerdiFNC();
        }
    }

    public void CaniArtirFNC()
    {
        gecerliSaglik++;

        if (gecerliSaglik >= maxSaglik)
            gecerliSaglik = maxSaglik;



        UiManager.instance.SlideriGuncelle(gecerliSaglik, maxSaglik);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
