using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    public Terrain terrain; // Reference to the terrain object in the scene
    public GameObject objectToSpawn; // Reference to the prefab you want to spawn
    

    void Start()
    {
        for(int i = 0; i < 200; i++)
        {
            SpawnObject();
        }
        
    }

    void SpawnObject()
    {
        // Random position within spawn area on terrain 
        Vector3 randomPosition = new Vector3(
          Random.Range(terrain.transform.position.x - terrain.terrainData.size.x / 2, terrain.transform.position.x + terrain.terrainData.size.x / 2),
          terrain.transform.position.y + 10f, // Start raycast from above terrain
          Random.Range(terrain.transform.position.z - terrain.terrainData.size.z / 2, terrain.transform.position.z + terrain.terrainData.size.z / 2)
        );

        RaycastHit hit;
        if (Physics.Raycast(randomPosition, Vector3.down, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            // Spawn the object at the adjusted position on the terrain
            Instantiate(objectToSpawn, hit.point, Quaternion.identity);
        }
        else
        {
            Debug.Log("No valid spawn location found on terrain.");
        }
    }

}
