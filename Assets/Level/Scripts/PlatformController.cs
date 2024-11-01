using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Transform platform;
    public Transform startPoint;
    public Transform endPoint;

    [SerializeField] private float speed = 1.5f;

    private int direction = 1;
   

    // Update is called once per frame
    void Update()
    {
        Vector2 target = CurrentMovementTarget();

        platform.position = Vector2.Lerp(platform.position, target, speed * Time.deltaTime);

        //Muda a direção do movimento quando chegar no target
        float distance = (target - (Vector2)platform.position).magnitude;

        if (distance <= 0.1f)
        {
            direction *= -1;
        }
    }

    private Vector2 CurrentMovementTarget()
    {
        Vector2 pos = direction == 1 ? startPoint.position : endPoint.position;

        return pos;
    }

    private void OnDrawGizmos()
    {
        if (platform != null && startPoint != null && endPoint != null)
        {
            Gizmos.DrawLine(platform.position, startPoint.position);
            Gizmos.DrawLine(platform.position, endPoint.position);
        }
    }

}
