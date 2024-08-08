using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightRoomMovement : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            EventManager.CameraGoRight();
            collision.transform.position = collision.transform.position += new Vector3(2, 0, 0);
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            this.transform.parent.Find("LeftLimit").GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }
}
