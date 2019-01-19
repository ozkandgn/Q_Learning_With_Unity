using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public void Move(int new_x, int new_y, float distance = 4)
    {
        transform.Translate(new Vector3(-new_x * distance, 0, -new_y * distance));
    }
}