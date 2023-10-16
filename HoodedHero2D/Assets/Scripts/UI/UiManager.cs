using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    public static UiManager instance;

    [SerializeField]
    Slider playerSlider;

    [SerializeField]
    TMP_Text coinTxt;

    [SerializeField]
    Transform butonlarPanel;



    private void Awake()
    {
        instance = this;  
    }

    private void Start()
    {
        TumButonlarinAlphasiniDusur();
        butonlarPanel.GetChild(0).GetComponent<CanvasGroup>().alpha = 1f;
        PlayerHaraketController.instance.HerseyiKapatNormaliAc();
    }

    public void SlideriGuncelle(int gecerliDeger, int maxDeger)
    {
        playerSlider.maxValue = maxDeger;
        playerSlider.value = gecerliDeger;
    }

    public void CoinAdetGuncelle()
    {
        coinTxt.text = GameManager.instance.toplananCointAdet.ToString();
    }

    void TumButonlarinAlphasiniDusur()
    {
        foreach (Transform btn in butonlarPanel)
        {
            btn.gameObject.GetComponent<CanvasGroup>().alpha = 0.25f;
        }

    }

    public void NormalButonaBasildi()
    {
        TumButonlarinAlphasiniDusur();
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetComponent<CanvasGroup>().alpha = 1f; // Tikladigimiz buton icin gecerli
        PlayerHaraketController.instance.HerseyiKapatNormaliAc();
    }
    public void KilicButonaBasildi()
    {
        TumButonlarinAlphasiniDusur();
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetComponent<CanvasGroup>().alpha = 1f; // Tikladigimiz buton icin gecerli
        PlayerHaraketController.instance.HerseyiKapatKilicAc();
    }
    public void OkButonaBasildi()
    {
        TumButonlarinAlphasiniDusur();
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetComponent<CanvasGroup>().alpha = 1f; // Tikladigimiz buton icin gecerli
        PlayerHaraketController.instance.HerseyiKapatOkuAc();
    }
    public void MizrakButonaBasildi()
    {
        TumButonlarinAlphasiniDusur();
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetComponent<CanvasGroup>().alpha = 1f; // Tikladigimiz buton icin gecerli
        PlayerHaraketController.instance.HerseyiKapatMizrakAc();
    }


}
