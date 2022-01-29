using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpike : MonoBehaviour
{
    // Start is called before the first frame update

    public int damageAmount;
    SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            player.Damage(damageAmount);
        }
        
        
        Destroy(gameObject, .2f);
    }
    //public void HideSpike()
    //{
    //    gameObject.layer = LayerMask.NameToLayer("hiddenSpike");
    //    sr.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
    //}
    //public void ShowSpike()
    //{
    //    gameObject.layer = LayerMask.NameToLayer("normalSpike");
    //    sr.maskInteraction = SpriteMaskInteraction.None;
    //}
}
