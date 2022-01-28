using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public static int playerHP = 5;
    public TextMeshProUGUI playerHPText;
    public static bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        playerHP = 10;
    }

    // Update is called once per frame
    void Update()
    {
        //update UI
        playerHPText.text = playerHP.ToString();

        if (isDead)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public static void Damage(int damageAmount)
    {
        //when get damaged reduce health
        playerHP -= damageAmount;
        //health below zero -> die
        if (playerHP <= 0)
        {
            isDead = true;
        }
    }
}
