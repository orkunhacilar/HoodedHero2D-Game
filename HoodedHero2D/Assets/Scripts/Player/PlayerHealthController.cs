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

        if (UiManager.instance != null)
        {
            UiManager.instance.SlideriGuncelle(gecerliSaglik, maxSaglik);
        }

    }


    public void CaniAzaltFNC()
    {
        gecerliSaglik--;
        SesManager.instance.SesEfektiCikar(3);
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
        SesManager.instance.SesEfektiCikar(8);
        if (gecerliSaglik >= maxSaglik)
            gecerliSaglik = maxSaglik;



        UiManager.instance.SlideriGuncelle(gecerliSaglik, maxSaglik);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
