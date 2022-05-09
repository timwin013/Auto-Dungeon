using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    void Start()
    {
        if (ObjectClick.ActiveNode <= 4)
        {
            transform.position = new Vector3(0, 0, -10);
        }

        if (5 <= ObjectClick.ActiveNode && ObjectClick.ActiveNode <= 10)
        {
            transform.position = new Vector3(0, 9, -10);
        }

        if (11 <= ObjectClick.ActiveNode && ObjectClick.ActiveNode <= 16)
        {
            transform.position = new Vector3(0, 18, -10);
        }

        if (17 <= ObjectClick.ActiveNode && ObjectClick.ActiveNode <= 22)
        {
            transform.position = new Vector3(15, 18, -10);
        }

        if (23 <= ObjectClick.ActiveNode && ObjectClick.ActiveNode <= 28)
        {
            transform.position = new Vector3(15, 9, -10);
        }

        if (29 <= ObjectClick.ActiveNode && ObjectClick.ActiveNode <= 34)
        {
            transform.position = new Vector3(15, 0, -10);
        }

        if (35 <= ObjectClick.ActiveNode && ObjectClick.ActiveNode <= 38)
        {
            transform.position = new Vector3(30, 0, -10);
        }

        if (39 <= ObjectClick.ActiveNode && ObjectClick.ActiveNode <= 43)
        {
            transform.position = new Vector3(30, 9, -10);
        }

        if (44 <= ObjectClick.ActiveNode)
        {
            transform.position = new Vector3(30, 18, -10);
        }
    }
}
