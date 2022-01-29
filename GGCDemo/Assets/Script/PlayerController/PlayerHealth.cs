using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int playerHP = 5;
    public float invincibleTime = 2f;
    private float invincibleTimer = 0;
    //public TextMeshProUGUI playerHPText;
    public static bool isDead;
    private Rigidbody2D rb;
    public int status = 0;
    public Animator myanimator;
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        playerHP = 10;
        rb = GetComponent<Rigidbody2D>();
        status = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //update UI
        //playerHPText.text = playerHP.ToString();

        if (status==2)
        {
            if (invincibleTimer < Time.time) {
                status = 0;
            }            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void Damage(int damageAmount)
    {
        //when get damaged reduce health
        playerHP -= damageAmount;
        myanimator.Play("DemoHurt");
        status = 2;
        invincibleTimer = Time.time + invincibleTime;
        //health below zero -> die
    }

    public void CheckDeath() {

        if (playerHP <= 0) {
            if (gameObject.tag == "Player")
            {
                //isDead = true;
                transform.position = CheckpointSystem.instance.lastCheckPointPos;
                playerHP = 10;
            }
            else {

                Destroy(gameObject);
            }
        }
        

    }


    public  IEnumerator Knockback(float knockDur, float knockbackPwr, Vector2 knockbackDir, bool withdamage, float damage)
    {
        status = 1;
        float timer = 0;
        while (knockDur > timer)
        {
            timer += Time.deltaTime;
            //rb.AddForce(new Vector2(knockbackDir.x * knockbackPwr, knockbackDir.y * knockbackPwr), ForceMode2D.Impulse);
            rb.velocity = new Vector2(knockbackDir.x * knockbackPwr, knockbackDir.y * knockbackPwr);
        }
        status = 2;
        Damage((int)damage);
        yield return 0;
    }
}
