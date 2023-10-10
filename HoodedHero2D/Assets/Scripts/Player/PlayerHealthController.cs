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
    }


    public void CaniAzaltFNC()
    {
        gecerliSaglik--;

        if (gecerliSaglik <= 0)
        {
            gecerliSaglik = 0;
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
