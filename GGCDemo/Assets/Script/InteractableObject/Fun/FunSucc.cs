using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunSucc : MonoBehaviour
{
    // Start is called before the first frame update
    public float suckforce;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        Vector2 direction = (transform.position - target.transform.position).normalized;
        if (target.gameObject.GetComponent<Rigidbody2D>())
        {
            target.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * suckforce, ForceMode2D.Impulse);
        }
    }

}
