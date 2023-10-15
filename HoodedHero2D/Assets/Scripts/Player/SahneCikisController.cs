using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SahneCikisController : MonoBehaviour
{
    public string digerSahne;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHaraketController>().PlayerHareketsizYap();
            collision.GetComponent<PlayerHaraketController>().enabled = false;

            FadeController.instance.SeffafdanMataGec();

            StartCoroutine(DigerSahneyeGec());
        }
    }

    IEnumerator DigerSahneyeGec()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(digerSahne);
    }
}
