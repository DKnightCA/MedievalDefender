using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldActivator : MonoBehaviour
{
    public float activeTime;
    public float coolDown;
    public GameObject magicShield;
    public GameObject currentShield;
    public Image chargeBar;

    private float timerCD;
    private float timerAT;

    // Start is called before the first frame update
    void Start()
    {
        timerCD = coolDown;
        timerAT = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentShield)
        {
            timerAT += Time.deltaTime;
            chargeBar.fillAmount = 1- Mathf.Clamp(timerAT / activeTime, 0, 1);
            if(timerAT >= activeTime)
            {
                Destroy(currentShield);
                currentShield = null;
                timerCD = 0;
            }
        }
        else
        {
            timerCD += Time.deltaTime;
            chargeBar.fillAmount = Mathf.Clamp(timerCD / coolDown, 0, 1);
        }
        if(Input.GetKeyDown("2") && timerCD >= coolDown)
        {
            currentShield = Instantiate(magicShield, transform.position, Quaternion.identity);
            currentShield.transform.parent = transform.parent;
            timerAT = 0;
            timerCD = 0;
        }
    }
}
