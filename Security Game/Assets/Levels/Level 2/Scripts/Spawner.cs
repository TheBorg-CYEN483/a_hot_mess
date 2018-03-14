using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] packets;

    private float timeBetweenPackets = 3f;
    private float minX = -125f;
    private float maxX = 125f;
    private float minY = -10f;
    private float maxY = 80f;
    private bool control = true;

    void Awake()
    {
        CreatePackets();
    }

    void Shuffle(GameObject[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            GameObject temp = arr[i];
            int rand = Random.Range(i, arr.Length);
            arr[i] = arr[rand];
            arr[rand] = temp;
        }
    }

    void CreatePackets()
    {
        Shuffle(packets);
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        for (int i = 0; i < packets.Length; i++)
        {
            Vector3 temp = packets[i].transform.position;

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
            packets[i].transform.position = temp;
        }
    }
}
