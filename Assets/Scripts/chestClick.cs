using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class chestClick : MonoBehaviour
{
    void Start()
    {
        
    }

    // If click back, reloads map0
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 rayCastPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(rayCastPosition, Vector2.zero);

            if (hit.collider != null)
            {
                updateObject(hit.collider.gameObject);
            }

        }
    }

    void updateObject(GameObject go)
    {
        if (go.name == "backArrow")
        {
            Popup popup = UIController.Instance.CreatePopup();
            popup.Init(UIController.Instance.MainCanvas,"You have gained an additional 69 units!","Dismiss");
        }
    }
}
