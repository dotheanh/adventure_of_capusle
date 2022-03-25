using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGraphic : MonoBehaviour
{
    public GameObject playerSpotlightPrefab;
    // Start is called before the first frame update
    void Start()
    {
        // add spotlight to object, easier to see in minimap
        GameObject child = GameObject.Instantiate(playerSpotlightPrefab);
        child.transform.SetParent(gameObject.transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
