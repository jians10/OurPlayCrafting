using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIGround : MonoBehaviour
{
    // Start is called before the first frame update\
    public float speed;
    public float distance;
    private bool movingRight = true;
    public Transform groundDetection;
    private float damageAmount=11;
    public LayerMask ground;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundinfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance,ground);
        if (groundinfo.collider == false) {
            if (movingRight) {
                transform.eulerAngles = new Vector3(0, 180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject player = collision.gameObject;
            Vector2 forcedirection = (player.transform.position - transform.position).normalized;
            forcedirection = new Vector2(forcedirection.x, forcedirection.y);
            Debug.Log(forcedirection);
            StartCoroutine(player.GetComponent<PlayerHealth>().Knockback(0.01f, 3, forcedirection, true, damageAmount));
            //player.Damage(damageAmount);
        }//

    }





}
