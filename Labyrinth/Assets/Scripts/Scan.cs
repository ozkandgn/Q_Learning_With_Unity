using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scan : MonoBehaviour {
    
    public List<int> Scan_Environment()
    {
        List<int> roads = new List<int>();
        Ray right = new Ray(transform.position, -transform.right);
        Ray left = new Ray(transform.position, transform.right);
        Ray up = new Ray(transform.position, -transform.forward);
        Ray duwn = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (!Physics.Raycast(right, out hit, 4))
        {
            //Debug.Log("right");
            roads.Add(1);
        }
        if (!Physics.Raycast(left, out hit, 4))
        {
            roads.Add(2);
            //Debug.Log("left");
        }
        if (!Physics.Raycast(up, out hit, 4))
        {
            roads.Add(3);
            //Debug.Log("up");
        }
        if (!Physics.Raycast(duwn, out hit, 4))
        {
            roads.Add(4);
            //Debug.Log("down");
        }
        Debug.DrawRay(transform.position, -transform.right * 4, Color.red);
        Debug.DrawRay(transform.position, transform.right * 4, Color.blue);
        Debug.DrawRay(transform.position, -transform.forward * 4, Color.white);
        Debug.DrawRay(transform.position, transform.forward * 4, Color.yellow);
        return roads;
    }
}
