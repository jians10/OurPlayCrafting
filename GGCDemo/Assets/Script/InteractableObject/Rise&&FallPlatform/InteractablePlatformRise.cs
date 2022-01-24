using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePlatformRise : MonoBehaviour
{
    public float ThresholdValue=0.1f;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerControllerAdvance>())
        {
            if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.y > ThresholdValue) {
                GetComponentInParent<PlatformMovement>().RiseValue();
            }
        }
    }
}
