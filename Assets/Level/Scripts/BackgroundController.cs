using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private float startPos, lenght;
    public float cameraLookAheadOffset; // Valor do lookahead do cinemachine para compensar no infinite scroll
    public GameObject cam;
    public float parallaxEffect;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = cam.transform.position.x * parallaxEffect;
        float movement = cam.transform.position.x * (1 - parallaxEffect);

        ParallaxBackground(distance, movement);
    }

    /// <summary>
    /// Faz o movimento do background com o efeito de parallax, e também mantem o background infinito
    /// </summary>
    /// <param name="distance"></param>
    /// <param name="movement"></param>
    private void ParallaxBackground(float distance, float movement)
    {
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);
        
        if (movement > startPos + lenght * (1 - cameraLookAheadOffset))
        {
            startPos += lenght;
        }
        else if (movement < startPos - lenght * (1 - cameraLookAheadOffset))
        {
            startPos -= lenght;
        }
    }
}
