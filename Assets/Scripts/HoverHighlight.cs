using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HoverHighlight : MonoBehaviour
{
    public Grid grid;
    public Tilemap Tilemap_grid;
    private Vector3Int coordinate;
    private Vector3Int new_coordinate;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 firstMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        coordinate = grid.WorldToCell(firstMousePos);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        new_coordinate = grid.WorldToCell(mouseWorldPos);
        if (new_coordinate != coordinate)
        {
            setTileColor(Color.yellow, new_coordinate, Tilemap_grid);
            setTileColor(Color.white, coordinate, Tilemap_grid);
        }
        //setTileColor(Color.red, coordinate, Tilemap_grid);
        coordinate = new_coordinate;
        //Debug.Log(coordinate);

    }

    void setTileColor(Color color, Vector3Int position, Tilemap tilemap)
    {
        tilemap.SetTileFlags(position, TileFlags.None);
        tilemap.SetColor(position, color);
    }

    void OnMouseEnter()
    {
        
    }

    void OnMouseExit()
    {
       
    }
}
