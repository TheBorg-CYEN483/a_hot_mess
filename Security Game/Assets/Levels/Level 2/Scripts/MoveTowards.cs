using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour
{
    public Transform routerTransform;
    private bool exiting = false;

    public bool Exiting
    {
        set { exiting = value; }
    }

    void Update()
    {
        if (transform.position.z == 0)
        {
            if (!exiting)
            {
                transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, 0f), routerTransform.position, 10f * Time.deltaTime);
                Vector3 vectorToTarget = routerTransform.position - transform.position;
                float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime * 200f);
            }
            else
            {
                transform.position = Vector3.MoveTowards(
                    new Vector3(transform.position.x, transform.position.y, 0f),
                    new Vector3(300f * Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad), 300f * Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad), 0f),
                    10f * Time.deltaTime
                );
            }
        }
    }
}
