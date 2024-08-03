using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopRoomMovement : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            EventManager.CameraGoUp();
            collision.transform.position = collision.transform.position += new Vector3(0, 3.5f, 0);
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            this.transform.parent.Find("BottomLimit").GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }
}
