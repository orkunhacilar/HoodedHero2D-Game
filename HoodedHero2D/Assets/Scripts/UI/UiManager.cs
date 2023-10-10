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



    private void Awake()
    {
        instance = this;  
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

}
