using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public float threshold;
    public Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < threshold)
        {
            transform.position = new Vector3(position.x, position.y, position.z);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Platform")
        {
            transform.position = new Vector3(transform.position.x, position.y, position.z);
        }
    }
}
