using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public int damageAmount = 100;
    //private PlayerHealth player;

    private void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")||collision.CompareTag("Enemy"))
        {
            PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
            Debug.Log("Some one hit spike");
            if (player.status != 0) {
                return;
            }
            player.status = 1;
            Vector2 forcedirection= (player.transform.position - transform.position).normalized;
            forcedirection = new Vector2(forcedirection.x, forcedirection.y);
            Debug.Log(forcedirection);
            StartCoroutine(player.GetComponent<PlayerHealth>().Knockback(0.01f, 3, forcedirection, true, damageAmount));
            //player.Damage(damageAmount);
        }//
    }
}
