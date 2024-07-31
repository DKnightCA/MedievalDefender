using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PotionMoveSpeed : MonoBehaviour
{

    [SerializeField] private float moveSpeedBoost;
    [SerializeField] private float moveSpeedBoostTime;
    [SerializeField] private GameObject player;    
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement movement))
            return;  
            
        movement.StartCoroutine(movement.ApplySpeedBoost(moveSpeedBoost, moveSpeedBoostTime));
        Destroy(gameObject);
    }
}
