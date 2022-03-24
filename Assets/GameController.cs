using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public KeyItem keyItem1, keyItem2, keyItem3;
    // Start is called before the first frame update
    void Start()
    {
        Screen.autorotateToLandscapeLeft = true;
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        // spawn key items to the forest
        KeyItem[] keyItems = { keyItem1, keyItem2, keyItem3 };
        foreach(KeyItem keyItem in keyItems)
        {
            float min = 230f;
            float max = 250f;
            int randomPositiveX = (Random.Range(0f, 1f) > 0.5f) ? 1 : -1;
            int randomPositiveZ = (Random.Range(0f, 1f) > 0.5f) ? 1 : -1;
            Vector3 spawnPosition = new Vector3(randomPositiveX*Random.Range(min, max), 
                                                max, 
                                                randomPositiveZ*Random.Range(min, max));
            Debug.Log("spawnPosition " + spawnPosition.x + ", " + spawnPosition.y + ", " + spawnPosition.z);
            keyItem.SetSpawnPosition(spawnPosition);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
