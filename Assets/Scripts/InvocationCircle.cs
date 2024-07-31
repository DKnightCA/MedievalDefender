    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvocationCircle : MonoBehaviour
{
    public float invocationTime;
    public float invocationUnits;
    public GameObject invocation;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= invocationTime)
        {
            Invocation();
            Destroy(gameObject);
            
        }
    }

    private void Invocation()
    {
        Instantiate(invocation, transform.position, Quaternion.identity);
    }
}
