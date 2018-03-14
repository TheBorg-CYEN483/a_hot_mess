using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendAway : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D target)
    {
        StartCoroutine(Process(target));
    }

    IEnumerator Process(Collider2D target)
    {
        target.gameObject.SetActive(false);
        Vector3 euler = transform.eulerAngles;
        euler.z = Random.Range(30f, 195f);

        yield return new WaitForSeconds(1.5f);

        target.transform.eulerAngles = euler;
        target.gameObject.GetComponent<MoveTowards>().Exiting = true;
        target.transform.position = Vector3.MoveTowards(
            new Vector3(target.transform.position.x, target.transform.position.y, 0f),
            new Vector3(500f * Mathf.Cos(euler.z * Mathf.Deg2Rad), 500f * Mathf.Sin(euler.z * Mathf.Deg2Rad), 0f),
            10f * Time.deltaTime
        );
        target.gameObject.SetActive(true);
    }
}
