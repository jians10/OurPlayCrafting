using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePlatformFall : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerControllerAdvance Player;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.GetComponent<PlayerControllerAdvance>())
        {
            Debug.Log("Onit");
            Player = collision.gameObject.GetComponent<PlayerControllerAdvance>();
            Player.currPlatform = GetComponentInParent<PlatformMovement>();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerControllerAdvance>())
        {
            Player.currPlatform = null;
        }
    }
}
