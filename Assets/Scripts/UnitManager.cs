using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance = null;
   
    public List<BaseUnit> allUnitsPrefab;

    public Sprite rangedSprite;
    public Sprite enemySprite;

    public Dictionary<Team, List<BaseUnit>> unitsByTeam = new Dictionary<Team, List<BaseUnit>>();

    public int playerUnitsCount = GameManager.playerUnitsCount;
    public int enemyUnitsCount = GameManager.enemyUnitsCount;

    public int playerUnitsLost = 0;
    public int enemyUnitsLost = 0;

    
    // only in fighting state, we check if we win or not.
    private bool fighting = false;
    // stop poping up when reaching the end of battle.
    private bool endstate = true;

    void Awake()
    {
        Instance = this;
        //Creates entries within the unitsByTeam dictionary corresponding to playerteam and enemyteam, contains list of unit objects.
        unitsByTeam.Add(Team.playerTeam, new List<BaseUnit>());
        unitsByTeam.Add(Team.enemyTeam, new List<BaseUnit>());
    }

    void Update()
    {
        //Debug.Log("check!");
        // check if we win or not
        if (fighting){
            if (endstate){
                //Debug.Log("Checking lose conditions");
                if (unitsByTeam[Team.playerTeam].Count == playerUnitsLost)
                {   
                    //Debug.Log("Lost");
                    //NEED TO LOAD APPROPRIATE SCENE AND ALSO UPDATE GAMEMANAGER UNIT POOLS.
                    Popup popup = UIController.Instance.CreatePopup();
                    popup.LosePopup(UIController.Instance.MainCanvas, "You lose", "Try again");
                    popup.transform.SetAsFirstSibling();
                    endstate = false;
                }

                else if (unitsByTeam[Team.enemyTeam].Count == enemyUnitsLost)
                {   
                    //Debug.Log("Won");
                    Popup popup = UIController.Instance.CreatePopup();
                    popup.WinPopup(UIController.Instance.MainCanvas, "You win", "Return");
                    popup.transform.SetAsFirstSibling();
                    endstate = false;
                    GameManager.playerUnitsCount -= playerUnitsLost;
                    GameManager.battlesWon += 1;
                }
            }
        }
        //TODO, NEED TO IMPLEMENT WIN/LOSS CONDITIONS, LIKELY NEED TO JUST COMPARE PLAYER UNITS LOST TO THE NUMBER OF PLAYER UNITS
        //INSTANTIATED ON THE BOARD, OR COULD CONSIDER JUST CHECKING WHETHER ALL ENTRIES IN THE UNITSBYTEAM DICTIONARY ARE NULL

    } 


        //if (playerUnitsCount == playerUnitsLost)
        //{   
        //    SceneManager.LoadScene("");
            //NEED TO LOAD APPROPRIATE SCENE AND ALSO UPDATE GAMEMANAGER UNIT POOLS.
        //}

        //else if (enemyUnitsCount == enemyUnitsLost)
        //{   
        //    Popup popup = UIController.Instance.CreatePopup();
        //    popup.Init(UIController.Instance.MainCanvas, "You lost. You got gud.", "Return");
        //    popup.transform.SetAsFirstSibling();
        //}

        //TODO, NEED TO IMPLEMENT WIN/LOSS CONDITIONS, LIKELY NEED TO JUST COMPARE PLAYER UNITS LOST TO THE NUMBER OF PLAYER UNITS
        //INSTANTIATED ON THE BOARD, OR COULD CONSIDER JUST CHECKING WHETHER ALL ENTRIES IN THE UNITSBYTEAM DICTIONARY ARE NULL
   

    void Start()
    {
        //InstantiateUnits();
        //Creates lists 
    }

    public int getRandomFormation() {
        int randomNum = UnityEngine.Random.Range(1, 7);
        return randomNum;
    }
    /*
    private void InstantiateUnits() {
        unitsByTeam.Add(Team.playerTeam, new List<BaseUnit>());
        unitsByTeam.Add(Team.enemyTeam, new List<BaseUnit>());

        int formationNumber = getRandomFormation();

        for (int i = 0; i < playerUnitsCount; i++) {
            // create new unit for player team
            int randomIdx = UnityEngine.Random.Range(0, allUnitsPrefab.Count - 1);
            BaseUnit newPlayerUnit = Instantiate(allUnitsPrefab[randomIdx]);
            unitsByTeam[Team.playerTeam].Add(newPlayerUnit);

            newPlayerUnit.Setup(Team.playerTeam, GridManager.Instance.setSpawnNode(Team.playerTeam, 0, i));
        }

        for (int i = 0; i < enemyUnitsCount; i++) {
            // create new unit for enemy team
            int randomIdx = UnityEngine.Random.Range(0, allUnitsPrefab.Count - 1);
            BaseUnit newEnemyUnit = Instantiate(allUnitsPrefab[randomIdx]);
            unitsByTeam[Team.enemyTeam].Add(newEnemyUnit);

            newEnemyUnit.Setup(Team.enemyTeam, GridManager.Instance.setSpawnNode(Team.enemyTeam, formationNumber, i));
        }
    }
    */

    public void DebugFight(){
        fighting = true;
        int formationNumber = getRandomFormation();


        for (int i = 0; i < enemyUnitsCount; i++) {
            // create new unit for enemy team
            int randomIdx = UnityEngine.Random.Range(0, allUnitsPrefab.Count - 1);
            BaseUnit newEnemyUnit = Instantiate(allUnitsPrefab[randomIdx]);
            newEnemyUnit.spriteRenderer.sprite = enemySprite;
            unitsByTeam[Team.enemyTeam].Add(newEnemyUnit);
            newEnemyUnit.Setup(Team.enemyTeam, GridManager.Instance.setSpawnNode(Team.enemyTeam, formationNumber, i));
            if (GameManager.enemyDifficulty == "Hard"){
                newEnemyUnit.baseDamage = 4;
                newEnemyUnit.baseHealth = 14;
                newEnemyUnit.baseHealth += GameManager.battlesWon;
            }
        }
    }


    public void InstantiateUnit(Vector3 mousePos, string unitType) {
        BaseUnit newPlayerUnit = Instantiate(allUnitsPrefab[0]);
        if (unitType == "ranged"){
            newPlayerUnit.range = 5;
            newPlayerUnit.baseHealth = 4;
            newPlayerUnit.baseDamage = 6;
            newPlayerUnit.attackSpeed = 2.0f;
            newPlayerUnit.spriteRenderer.sprite = rangedSprite;
        }
        unitsByTeam[Team.playerTeam].Add(newPlayerUnit);
        //Debug.Log(mousePos);
        //Debug.Log(PosToNodeIndex(mousePos));
        //Debug.Log("Trying to make new playerUnit at Node index above.");
        
        newPlayerUnit.Setup(Team.playerTeam, GridManager.Instance.selectNode(PosToNodeIndex(mousePos)));
    }

    public bool isNodeEmpty(Vector3 mousePos) {
        Node nodeToCheck = GridManager.Instance.selectNode(PosToNodeIndex(mousePos));
        return(nodeToCheck.isOccupied);
    }


    private int PosToNodeIndex(Vector3 coords) {
         Vector3 origin = new Vector3(-4, -4, 0);
         Vector3 distanceFromOrigin = origin - coords;
         int nodeIndex = (int) (Math.Abs(8 * distanceFromOrigin.x) + Math.Abs(distanceFromOrigin.y));
         return(nodeIndex);
    }
 
    public List<BaseUnit> GetEntitiesAgainst(Team against)
    {
        if (against == Team.enemyTeam)
            return unitsByTeam[Team.playerTeam];
        else
            return unitsByTeam[Team.enemyTeam];
    }

}

public enum Team //enum so read-only values, access with Team.(...) syntax
{
    playerTeam,
    enemyTeam
}