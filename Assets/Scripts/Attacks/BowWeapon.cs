using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowWeapon : MonoBehaviour
{
    [SerializeField] private Camera camaraPrincipal;
    [SerializeField] private Vector3 posRaton;
    [SerializeField] private GameObject disparo;
    [SerializeField] private Transform disparoTransform;
    [SerializeField] private bool canShoot;
    [SerializeField] private float atkSpeed;
    private const float MAX_ATK_SPEED = 0.2f;
    private const float MIN_ATK_SPEED = 2f;
    private float timer;

    // Start is called before the first frame update
    protected void Start()
    {
        camaraPrincipal = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
        posRaton = camaraPrincipal.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotacion = posRaton - transform.position;

       // float rotZ = Mathf.Atan2(rotacion.y, rotacion.x) * Mathf.Rad2Deg;

    //    transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if(!canShoot)
        {
            timer += Time.deltaTime;
            if(timer > atkSpeed)
            {
            canShoot = true;
            timer = 0;
            }
        }
        if(Input.GetMouseButton(0) && canShoot)
        {
            canShoot = false;
            Instantiate(disparo, disparoTransform.position, Quaternion.identity);
        }
    }

    public float getAtkSpeed()
    {
        return atkSpeed;
    }

    // This changes the attack speed permanently. Not meant for temporary (de)buffs.
    public void setAtkSpeed(float newAtkSpeed)
    {
        atkSpeed = newAtkSpeed;
    }
    
    public float AddAtkSpeed(float speedAddition)
    {
        float addedAtkSpeed;
        if(this.atkSpeed - speedAddition <= MAX_ATK_SPEED)
        {
            addedAtkSpeed = this.atkSpeed - MAX_ATK_SPEED;
            this.atkSpeed = MAX_ATK_SPEED;
        } else if (this.atkSpeed + speedAddition >= MIN_ATK_SPEED)
        {
            addedAtkSpeed = MIN_ATK_SPEED - this.atkSpeed;
        } else
        {
            this.atkSpeed -= speedAddition;
            addedAtkSpeed = speedAddition;
        }
        return addedAtkSpeed;
    }

    // This changes the attack speed temporarily. It is meant for (de)buffs whose effect disappears after the time specified in boostTime
    public IEnumerator ApplyAtkSpeedBoost(float boostAmount, float boostTime)
    {
        float substractAtkSpeed = AddAtkSpeed(boostAmount);
        yield return new WaitForSeconds(boostTime);
        AddAtkSpeed(-substractAtkSpeed);
    }

}