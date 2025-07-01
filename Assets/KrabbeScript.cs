using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrabbeScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float moveSpeed = 5f; // Hastighed for bevægelse til højre/venstre

    void Start()
    {
        // Her kunne man sætte startposition eller andet hvis nødvendigt
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal"); // A/D eller pil-taster
        myRigidbody.velocity = new Vector2(moveInput * moveSpeed, myRigidbody.velocity.y); // Bevar faldhastighed
    }
}
