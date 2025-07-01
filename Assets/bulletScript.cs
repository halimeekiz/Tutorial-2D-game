using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public float speed = 3f; // Skud hastighed
    public GameObject bulletPrefab; // Prefab til skuddet
    public Transform firepoint; // Hvor skuddet affyres fra

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))  // Når mellemrumstasten trykkes
        {
            Shoot();
        }
    }

    void Shoot()
    {
       Vector3 offset = new Vector3(0f, 0.5f, 0);
    Vector3 spawnPosition = firepoint.position + offset;

    GameObject bullet = Instantiate(bulletPrefab, spawnPosition, firepoint.rotation);

    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
    if (rb != null)
    {
        rb.velocity = Vector2.up * speed; // Her sætter du hastighed opad
    }
    }
}
