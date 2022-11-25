using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollow : MonoBehaviour
{
    public Path path;
    [Range (1f, 50f)]
    public float speed = 10f;

    [Range (1f, 1000f)]
    public float steeringInertia = 100f;

    public bool isLooping = true;
    public float waypointRadius = 1f;

    private float curSpeed;

    private int curPathIndex = 0;
    private float pathLength;
    private Vector3 targetPoint;

    Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        pathLength = path.Length;
        velocity = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        curSpeed = speed * Time.deltaTime;

        targetPoint = path.GetPoint(curPathIndex);

        if(Vector3.Distance(transform.position, targetPoint) 
            < waypointRadius)
        {
            if(curPathIndex < pathLength -1) 
                curPathIndex++;
            else if(isLooping) 
                curPathIndex = 0;
            else 
                return;
        }

        if (curPathIndex >= pathLength) return;

        if(curPathIndex >= pathLength-1 && !isLooping)
            velocity += Steer(targetPoint, true);
        else
            velocity += Steer(targetPoint);

        transform.position += velocity;

        transform.rotation = Quaternion.LookRotation(velocity);
    }

    public Vector3 Steer(Vector3 target, bool bFinalPoint = false)
    {
        Vector3 desiredVelocity = 
            (target - transform.position);
        float dist = desiredVelocity.magnitude;

        desiredVelocity.Normalize();

        if(bFinalPoint && dist < waypointRadius)
            desiredVelocity *= 
            curSpeed * (dist/ waypointRadius);
        else 
            desiredVelocity *= curSpeed;

        Vector3 steeringForce = desiredVelocity - velocity;

        return steeringForce/ steeringInertia;
    }
}
