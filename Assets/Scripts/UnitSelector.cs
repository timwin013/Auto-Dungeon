using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UnitSelector : MonoBehaviour
{
    public Button Button1;
    public Button Button2;
    public GameManager GameManager; 
    public Texture2D normalCursor;
    [SerializeField] private UnityEngine.UI.Text Button1Text;
    [SerializeField] private UnityEngine.UI.Text Button2Text;
    private UnityEngine.UI.Text displaytext1;
    private UnityEngine.UI.Text displaytext2;
    private bool holdingMelee;
    private bool holdingRanged;
    private int totalBaseUnits;
    [SerializeField]public Texture2D rock;
    [SerializeField]public Texture2D archer;
    public Grid grid;

    // Start is called before the first frame update
    void Start()
    {   
        Button1.onClick.AddListener(LiftMelee);
        Button2.onClick.AddListener(LiftRanged);
        holdingMelee = false;
        holdingRanged = false;
        //When Selector is instantiated, sets below variable equal to the Game Manager's unit total at that time.
        totalBaseUnits = GameManager.playerUnitsCount;
        displaytext1 = Button1Text.GetComponent<Text>();
        displaytext2 = Button2Text.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            if (holdingMelee == true)
            {
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 mouseGridPos = grid.WorldToCell(mouseWorldPos);
                if (!(UnitManager.Instance.isNodeEmpty(mouseGridPos)))
                {
                    Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.ForceSoftware);
                    //Debug.Log("Instantiate unit has been called for melee.");
                    UnitManager.Instance.InstantiateUnit(mouseGridPos, "melee");
                    holdingMelee = false;
                }
            }
        
        if (Input.GetMouseButtonDown(0))
            if (holdingRanged == true)
            {
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 mouseGridPos = grid.WorldToCell(mouseWorldPos);
                if (!(UnitManager.Instance.isNodeEmpty(mouseGridPos)))
                {
                    Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.ForceSoftware);
                    //Debug.Log("Instantiate unit has been called for ranged.");
                    UnitManager.Instance.InstantiateUnit(mouseGridPos, "ranged");
                    holdingRanged = false;
                }
            }

           

        displaytext1.text = "Melee Unit: " + totalBaseUnits;
        displaytext2.text = "Ranged Unit: " + totalBaseUnits;
        

    }

    void LiftMelee()
    {
        if (totalBaseUnits >= 1)
        {
            Cursor.SetCursor(rock, Vector2.zero, CursorMode.ForceSoftware);
            holdingMelee = true;
            totalBaseUnits -= 1;
        }
    }

    void LiftRanged()
    {
        //Debug.Log("LiftRanged called.");
        if (totalBaseUnits >= 1)
        {
            Cursor.SetCursor(archer, Vector2.zero, CursorMode.ForceSoftware);
            holdingRanged = true;
            totalBaseUnits -= 1;
        }
    }
}
