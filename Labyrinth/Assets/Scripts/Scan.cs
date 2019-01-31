using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scan : MonoBehaviour {
    
    public List<int[]> Scan_Environment()
    {
        List<int[]> roads = new List<int[]>();
        Ray right = new Ray(transform.position, -transform.right);
        Ray left = new Ray(transform.position, transform.right);
        Ray up = new Ray(transform.position, -transform.forward);
        Ray duwn = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        int[] array = new int[2];

        if (!Physics.Raycast(right, out hit, 4))
        {
            //Debug.Log("right");
            array = new int[2];
            array[0] = 1;
            array[1] = 0;
            roads.Add(array);
        }
        if (!Physics.Raycast(left, out hit, 4))
        {
            array = new int[2];
            array[0] = -1;
            array[1] = 0;
            roads.Add(array);
            //Debug.Log("left");
        }
        if (!Physics.Raycast(up, out hit, 4))
        {
            array = new int[2];
            array[0] = 0;
            array[1] = 1;
            roads.Add(array);
            //Debug.Log("up");
        }
        if (!Physics.Raycast(duwn, out hit, 4))
        {
            array = new int[2];
            array[0] = 0;
            array[1] = -1;
            roads.Add(array);  
            //Debug.Log("down");
        }
        Debug.DrawRay(transform.position, -transform.right * 4, Color.red);
        Debug.DrawRay(transform.position, transform.right * 4, Color.blue);
        Debug.DrawRay(transform.position, -transform.forward * 4, Color.white);
        Debug.DrawRay(transform.position, transform.forward * 4, Color.yellow);
        return roads;
    }
}
