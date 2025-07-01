using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    public float speed = 2f;

    void Update()
    {
        // Flyt nedad
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Når objektet kommer udenfor bunden af skærmen, slet det
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);

            // Hvis du vil have at krabben dør, når noget når bunden,
            // kan du her tilføje event eller kalde metode til det.
        }
    }
}