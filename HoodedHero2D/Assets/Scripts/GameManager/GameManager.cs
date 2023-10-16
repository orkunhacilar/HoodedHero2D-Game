using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int toplananCointAdet;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        toplananCointAdet = 0;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UiManager.instance.PausePanelAcKapat();
        }
    }

    public void OyunCikisEkraniniAc()
    {
        UiManager.instance.BitisPaneliniAc();
    }
}
