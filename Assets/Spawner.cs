using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefabs; // Alle dine prefabs (fish, treasure, bomb osv.)
    public float spawnInterval = 1.5f; // Hvor tit der spawnes (i sekunder)
    public float speed = 2f; // Hvor hurtigt objekterne bevæger sig mod venstre

   void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            SpawnObject();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnObject()
    {
        // Vælg tilfældig prefab
        int prefabIndex = Random.Range(0, prefabs.Length);
        GameObject prefabToSpawn = prefabs[prefabIndex];

        // Tilfældig X-position (tilpas interval så objekter rammer scenen)
        float xPos = Random.Range(-8f, 8f);

        // Fast Y-position (ovenfor synligt område)
        float yPos = 6f;

        Vector3 spawnPos = new Vector3(xPos, yPos, 0);

        // Instantiate objekt
        GameObject obj = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

        // Tilføj script der bevæger objektet nedad
        obj.AddComponent<MoveDown>().speed = speed;
    }
}