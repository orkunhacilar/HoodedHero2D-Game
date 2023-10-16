using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IskeletAttackController : MonoBehaviour
{
    [SerializeField]
    Transform attackPos;

    [SerializeField]
    float atakYaricap;

    [SerializeField]
    LayerMask playerLayer;

    public void AtakYapFNC() // BUNU ANIMASYONDAN GERILI SANIYEYE FONKSYON EKLIYEREK TETIKLETIYORUM
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(attackPos.position, atakYaricap, playerLayer); // eger bu cemberin icine girerse al onu playerCollidera at

        if(playerCollider!=null && !playerCollider.GetComponent<PlayerHaraketController>().playerCanverdimi) // null degilse ve yasiyosa diye bakiyoruz ( bakabiliyoruz cunku collider olarak aldik)
        {
            playerCollider.GetComponent<PlayerHaraketController>().GeriTepkiFNC();
            playerCollider.GetComponent<PlayerHealthController>().CaniAzaltFNC();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, atakYaricap); //iskelete bas pozisyondan yaricap kadar ciz .
    }
}
