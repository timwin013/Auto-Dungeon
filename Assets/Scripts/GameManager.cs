using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
   
    public List<BaseUnit> allUnitsPrefab;

    Dictionary<Team, List<BaseUnit>> unitsByTeam = new Dictionary<Team, List<BaseUnit>>();

    // List<BaseUnit> teamEntities = new List<BaseUnit>(); //lists for team and enemy entities 
    // List<BaseUnit> enemyEntities = new List<BaseUnit>();

    public static int playerUnitsCount = 1;
    public static int enemyUnitsCount = 6;
    public static string enemyDifficulty = "Easy";
    public static int battlesWon = 0;


    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
         if (Instance == null)
             Instance = null;
         else if (Instance != null)
             Destroy(gameObject);

        //InstantiateUnits();
    }

    // Currently a hacky solution to get 'randomness'.
    // Must update the second arg to .Range every time new formation is added
    public int getRandomFormation() {
        int randomNum = Random.Range(1, 7);
        return randomNum;
    }

    public void InstantiateUnits() {
        unitsByTeam.Add(Team.playerTeam, new List<BaseUnit>());
        unitsByTeam.Add(Team.enemyTeam, new List<BaseUnit>());

        int formationNumber = getRandomFormation();

        // Spawn player loop can be erased once drag-and-drop is implemented
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
}

/*
public enum Team //enum so read-only values, access with Team.(...) syntax
{
    playerTeam,
    enemyTeam
}
*/
public enum GameState
{
    map,
    grid
}