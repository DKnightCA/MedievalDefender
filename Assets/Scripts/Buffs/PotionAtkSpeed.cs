using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PotionAtkSpeed : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float atkSpeedBoost;
    [SerializeField] private float atkSpeedBoostTime;
    [SerializeField] private GameObject player;   
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.TryGetComponent<BowWeapon>(out BowWeapon atkSpeed))
            return;
    
        atkSpeed.StartCoroutine(atkSpeed.ApplyAtkSpeedBoost(atkSpeedBoost, atkSpeedBoostTime));
        Destroy(gameObject);
    }void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
