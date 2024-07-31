using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionHealthBoost : MonoBehaviour
{

    [SerializeField] private float maxHealthIncrease;
    [SerializeField] private GameObject player;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.TryGetComponent<VidaJugador>(out VidaJugador playerHealth))
            return;
                
        playerHealth.ChangeMaxHealth(maxHealthIncrease);
        Destroy(gameObject);
    }
}
