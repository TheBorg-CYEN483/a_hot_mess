using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private float timeBetweenPackets = 1.5f;
    private float minX = -125f;
    private float maxX = 125f;
    private float minY = -10f;
    private float maxY = 80f;
    private bool control = true;

    void OnTriggerEnter2D(Collider2D target)
    {
        StartCoroutine(Respawn(target));
    }

    IEnumerator Respawn(Collider2D target)
    {
        target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, 5f);
        Vector3 temp = target.transform.position;

        if (control)
        {
            temp.y = maxY;
            temp.x = Random.Range(minX, maxX);
            control = !control;
        }
        else
        {
            temp.y = Random.Range(minY, maxY);
            temp.x = minX;
            control = !control;
        }

        temp.z = 0f;
        yield return new WaitForSeconds(timeBetweenPackets);
        target.transform.position = temp;
        target.gameObject.GetComponent<MoveTowards>().Exiting = false;
    }
}
