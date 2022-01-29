using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunPush : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D target)
    {
       //if (target.gameObject.tag != "character") {
       //     return;
       //}
       Vector2 direction = (transform.position - target.transform.position).normalized;
       if (target.gameObject.GetComponent<Rigidbody2D>())
       {
            Rigidbody2D rb = target.gameObject.GetComponent<Rigidbody2D>();
            if (rb)
            {
                Debug.Log("stop in there");
                rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(rb.velocity.x, 0), Time.deltaTime * 5);
            }
        }   
    }
}
