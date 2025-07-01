using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SkyMover : MonoBehaviour
{
    public float speed = 0.5f;  // Hvor hurtigt skyen skal bevæge sig

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /* Flyt skyen mod venstre hele tiden (hvert frame)
           Vector3.left svarer til (-1, 0, 0) – altså mod venstre
           Time.deltaTime gør det uafhængigt af FPS */ 
        transform.Translate(Vector3.left * speed * Time.fixedDeltaTime);

        /* Hvis skyen er for langt til venstre på skærmen (x-position < -10)
           så send den tilbage til højre (x = 10), så den kan flyve igen */
        if (transform.position.x < -10f)
        {
            transform.position = new Vector3(10f, transform.position.y, transform.position.z);
        }
    }
}
