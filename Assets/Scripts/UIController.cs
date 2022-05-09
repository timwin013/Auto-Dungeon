using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public static UIController Instance;

    public Transform MainCanvas;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
        {
            GameObject.Destroy(this.gameObject); // If we already have an instance
            return;
        }

        Instance = this; // Create instance otherwise
    }

    public Popup CreatePopup()
    {
        GameObject popUpGo = Instantiate(Resources.Load("UI/Popup") as GameObject); // check this pathway to popup game object
        return popUpGo.GetComponent<Popup>();
    }

}
