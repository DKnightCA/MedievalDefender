using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopNavigation : MonoBehaviour
{

    public Rigidbody2D rb;
    // Start is called before the first frame update


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SceneManager.LoadScene("Shop");
        }
    }
}
