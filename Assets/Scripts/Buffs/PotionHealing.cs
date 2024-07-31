using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PotionHealing : MonoBehaviour
{
    [SerializeField] private float healing;
    [SerializeField] private GameObject player;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.TryGetComponent<VidaJugador>(out VidaJugador playerHealth))
            return;
                
        playerHealth.Healing(healing);
        Destroy(gameObject);
    }
}
