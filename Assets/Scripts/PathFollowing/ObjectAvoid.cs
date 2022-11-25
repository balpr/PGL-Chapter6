using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAvoid : MonoBehaviour
{
    public float objectRadius = 1.2f;
    public float speed = 10f;

    public float force = 50f;
    public float minimumDistToAvoid = 10f;
    public float targetReachedRadius = 3f;

    private float curSpeed;
    private Vector3 targetPoint;

    // Start is called before the first frame update
    void Start()
    {
        targetPoint = Vector3.zero;
    }

    void OnGUI()
    {
        GUILayout.Label("Click sembarang");
    }
    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(
            Input.mousePosition
        );

        if(Input.GetMouseButtonDown(0) &&
            Physics.Raycast(ray, out var hit, 100f))
            {
                targetPoint = hit.point;
            }

        Vector3 dir = (targetPoint - transform.position);
        dir.Normalize();

        AvoidObstacles(ref dir);

        if(Vector3.Distance(targetPoint,
            transform.position) < targetReachedRadius)
            return;
        
        curSpeed = speed * Time.deltaTime;

        var rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 5f * Time.deltaTime);

        transform.position += transform.forward * curSpeed;
        transform.position = new Vector3(
            transform.position.x, 0, transform.position.z
        );
    }

    public void AvoidObstacles(ref Vector3 dir)
    {
        int layerMask = 1 << 8;

        if (Physics.SphereCast(transform.position,
            objectRadius, transform.forward, out var hit,
            minimumDistToAvoid, layerMask))
        {
            Vector3 hitNormal = hit.normal;
            hitNormal.y = 0.0f;

            dir = transform.forward + hitNormal * force;
        }
    }
}
