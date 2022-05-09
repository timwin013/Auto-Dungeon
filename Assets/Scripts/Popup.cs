using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Popup : MonoBehaviour
{
    [SerializeField] Button button1;
    [SerializeField] Text button1Text;
    [SerializeField] Text popupText;

    public void Init(Transform canvas, string popupMessage, string btn1txt) // "Action action" argument not required yet  
    {                                                                       // maybe put in for choosing between rewards
        popupText.text = popupMessage;                                      // with two option buttons.
        button1Text.text = btn1txt;

        // Initialise the canvas 
        transform.SetParent(canvas);  // Set as child of canvas.
        transform.localScale = Vector3.one; // set scale to that of the canvas.
        GetComponent<RectTransform>().offsetMin = Vector2.zero; // set popup to be central.
        GetComponent<RectTransform>().offsetMax = Vector2.zero;

        button1.onClick.AddListener(() =>
        {
            GameObject.Destroy(this.gameObject); //Check capitalisation here
        });

        // IF WE WANT TO PUT AN ACTION ON A SECOND BUTTON WE CAN USE:
        // button2.onClick.AddListener(() =>
        // {
        //     action();
        // });
    }

    public void WinPopup(Transform canvas, string popupMessage, string btn1txt) // "Action action" argument not required yet  
    {                                                                       // maybe put in for choosing between rewards
        popupText.text = popupMessage;                                      // with two option buttons.
        button1Text.text = btn1txt;

        // Initialise the canvas 
        transform.SetParent(canvas);  // Set as child of canvas.
        transform.localScale = Vector3.one; // set scale to that of the canvas.
        GetComponent<RectTransform>().offsetMin = Vector2.zero; // set popup to be central.
        GetComponent<RectTransform>().offsetMax = Vector2.zero;

        button1.onClick.AddListener(() =>
        {
            GameObject.Destroy(this.gameObject); //Check capitalisation here
            SceneManager.LoadScene("Map 1");

        });
    }

    public void LosePopup(Transform canvas, string popupMessage, string btn1txt) // "Action action" argument not required yet  
    {                                                                       // maybe put in for choosing between rewards
        popupText.text = popupMessage;                                      // with two option buttons.
        button1Text.text = btn1txt;

        // Initialise the canvas 
        transform.SetParent(canvas);  // Set as child of canvas.
        transform.localScale = Vector3.one; // set scale to that of the canvas.
        GetComponent<RectTransform>().offsetMin = Vector2.zero; // set popup to be central.
        GetComponent<RectTransform>().offsetMax = Vector2.zero;

        button1.onClick.AddListener(() =>
        {
            GameObject.Destroy(this.gameObject); //Check capitalisation here
            SceneManager.LoadScene("Grid");

        });

    }
}
