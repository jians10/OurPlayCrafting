using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    // Start is called before the first frame update
    public int MaxHealth=1;
    private int CurrHealth;
    public int ReviveTime;
    

    [Header("CheckPoint")]
    public Transform CheckPoint;
    void Start()
    {
        CurrHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setCheckPoint(Transform target)
    {
        CheckPoint = target;
    }
    void Death() { 
          
        
        
        
    }


    public void Damage(int damageAmount)
    {
        CurrHealth = CurrHealth - damageAmount;
        if (CurrHealth < 0) { 
        
        
        
        }



    }
}
