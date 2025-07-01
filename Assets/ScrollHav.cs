using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollHav : MonoBehaviour
{
    public float speed = 0.1f;
    public Material mat;
    public Vector2 offset; 
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
       offset.x += speed * Time.deltaTime;
        mat.mainTextureOffset = offset; 
    }
}
