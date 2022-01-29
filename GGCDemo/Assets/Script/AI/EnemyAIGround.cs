using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIGround : MonoBehaviour
{
    // Start is called before the first frame update\
    public float speed;
    public float distance;
    public bool movingRight = true;
    public Transform groundDetection;
    private float damageAmount=11;
    private Rigidbody2D rb;
    public LayerMask ground;
    public Transform groundDetect;
    public Transform posLeft, posRight;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        //if()
        RaycastHit2D groundinfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance, ground);
        RaycastHit2D grounddetect = Physics2D.Raycast(groundDetect.position, Vector2.down, 2f, ground);
        
        if (Vector2.Distance(transform.position, posLeft.position)<1) {
            movingRight=true;
            transform.eulerAngles = new Vector3(0, 0, 0);

        }
        if (Vector2.Distance(transform.position,posRight.position)<1)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            movingRight =false;
        }

        if (grounddetect)
        {
            if (groundinfo.collider == false)
            {
                if (movingRight)
                {
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
    void Flip()
    {
        movingRight = !movingRight;
        //transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
        //facingRight = !facingRight;
        Vector3 Scalar = transform.localScale;
        Scalar.x *= -1;
        transform.localScale = Scalar;
        //CreateDust();
    }





}
