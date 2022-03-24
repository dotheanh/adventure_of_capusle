using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    public Vector3 spawnPosition = new Vector3(0, 0, 0);
    public float speed = 20;

    private Rigidbody rigi;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetSpawnPosition(Vector3 _spawnPosition)
    {
        rigi = GetComponent<Rigidbody>();
        this.spawnPosition = _spawnPosition;
        rigi.AddForce(this.spawnPosition * speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision hit)
    {
        //Debug.Log(hit.transform.gameObject.tag);
    }
}
