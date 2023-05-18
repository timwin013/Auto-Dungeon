using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridLight : MonoBehaviour
{
    void Start()
    {
        if (ObjectClick.ActiveNode <= 4)
        {
            GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = Color.white;
        }

        if (5 <= ObjectClick.ActiveNode && ObjectClick.ActiveNode <= 10)
        {
            GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = Color.green;
        }

        if (11 <= ObjectClick.ActiveNode && ObjectClick.ActiveNode <= 16)
        {
            GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = Color.blue;
        }

        if (17 <= ObjectClick.ActiveNode && ObjectClick.ActiveNode <= 22)
        {
            GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = Color.cyan;
        }

        if (23 <= ObjectClick.ActiveNode && ObjectClick.ActiveNode <= 28)
        {
            GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = Color.blue;
        }

        if (29 <= ObjectClick.ActiveNode && ObjectClick.ActiveNode <= 34)
        {
            GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = Color.grey;
        }

        if (35 <= ObjectClick.ActiveNode && ObjectClick.ActiveNode <= 38)
        {
            GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = Color.yellow;
        }

        if (39 <= ObjectClick.ActiveNode && ObjectClick.ActiveNode <= 43)
        {
            GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = Color.green;
        }

        if (44 <= ObjectClick.ActiveNode)
        {
            GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = Color.red;
        }
    }
}
