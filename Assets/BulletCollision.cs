using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public GameObject explosionPrefab;

  private void OnCollisionEnter2D(Collision2D collision)
{
    // Instantiér eksplosionsprefab på kuglens position og rotation og gem reference til den
    GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);

    // Slet kuglen (gameObjectet som dette script sidder på)
    Destroy(gameObject);

    // Slet eksplosionsobjektet efter 1 sekund
    Destroy(explosion, 1f);
}

}
