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
        Color packetColor = target.gameObject.GetComponent<SpriteRenderer>().color;
        Color textColor = target.gameObject.GetComponentInChildren<TextMesh>().color;
        target.gameObject.GetComponent<SpriteRenderer>().color = new Color(packetColor.r, packetColor.g, packetColor.b, 0);
        target.gameObject.GetComponentInChildren<TextMesh>().color = new Color(textColor.r, textColor.g, textColor.b, 0);
        Vector3 euler = transform.eulerAngles;
        euler.z = Random.Range(30f, 195f);

        if (euler.z > 90)
        {
            target.GetComponentInChildren<TextMesh>().transform.localRotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            target.GetComponentInChildren<TextMesh>().transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        yield return new WaitForSeconds(1.5f);

        target.transform.eulerAngles = euler;
        target.gameObject.GetComponent<MoveTowards>().Exiting = true;
        target.transform.position = Vector3.MoveTowards(
            new Vector3(target.transform.position.x, target.transform.position.y, 0f),
            new Vector3(500f * Mathf.Cos(euler.z * Mathf.Deg2Rad), 500f * Mathf.Sin(euler.z * Mathf.Deg2Rad), 0f),
            10f * Time.deltaTime
        );
        target.gameObject.GetComponent<SpriteRenderer>().color = new Color(packetColor.r, packetColor.g, packetColor.b, 255);
        target.gameObject.GetComponentInChildren<TextMesh>().color = new Color(textColor.r, textColor.g, textColor.b, 255);
    }
}
