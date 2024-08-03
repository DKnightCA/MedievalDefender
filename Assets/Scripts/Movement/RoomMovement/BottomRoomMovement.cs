using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomRoomMovement : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            EventManager.CameraGoDown();
            collision.transform.position = collision.transform.position += new Vector3(0, -3.5f, 0);
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            this.transform.parent.Find("TopLimit").GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }
}
