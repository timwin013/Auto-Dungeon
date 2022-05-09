using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitPoolDisplay : MonoBehaviour
{
    public UnityEngine.UI.Text CanvasText;
    [SerializeField] private GameManager GameManager;
    private UnityEngine.UI.Text displaytext;
    private int BaseUnits;

    // Start is called before the first frame update
    void Start()
    {
        displaytext = CanvasText.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        BaseUnits = GameManager.playerUnitsCount;
        //Text displaytext = canvastext.GetComponent<Text>();
        displaytext.text = "Base Units: " + BaseUnits;
    }
}
