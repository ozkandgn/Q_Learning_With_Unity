using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour {

    Movement move;
    Scan scan;
    Brain brain;

    List<int> roads;

    int x = 1, y = 1;

    Vector3 firstPos;

    void Start () {
        move = GetComponent<Movement>();
        scan = GetComponent<Scan>();
        brain = GetComponent<Brain>();
        roads = new List<int>();
        firstPos = transform.position;
        StartCoroutine(Loop());
    }

    IEnumerator Loop()
    {
        roads = scan.Scan_Environment();
        int[] array = brain.Q_Learning(x,y,roads);
        if (array[0] == -2)
        {
            Reset();
        }
        else
        {
            move.Move(array[0], array[1],3.9f);
            x += array[0];
            y += array[1];
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Loop());
    }

    void Reset()
    {
        int road_length;
        int count = 0;
        do
        {
            x = Random.Range(1, 9);//29
            y = Random.Range(1, 9);//29
            transform.position = firstPos - new Vector3(3.9f * (x - 1), 0, 3.9f * (y - 1));
            road_length = scan.Scan_Environment().Count;
        } while ((road_length == 4 || road_length == 1) && count++<1000);
        if (count > 900)
        {
            Debug.Log("Count!!!!");
        }
    }
}
