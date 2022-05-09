using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BaseUnit : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public HealthBar healthBar; //from HealthBar script
    
    public int baseDamage = 1;
    public int baseHealth = 1;
    public float attackSpeed = 1f; //attacks per second
    public float movementSpeed = 1f; //movement per second
    public int range = 1;

    protected bool dead = false;
    protected Team myTeam;
    protected Node currentNode;
    protected BaseUnit currentTarget = null;
    protected bool moving;
    protected Node destination;
    //protected List<BaseUnit> enemiesInRange = new List<BaseUnit>();

    protected bool HasEnemy => currentTarget != null;
    protected bool IsInRange => currentTarget != null && Vector3.Distance(this.transform.position, currentTarget.transform.position) <= range;
    protected bool attackDealt;

    public Node CurrentNode => currentNode;

    void Awake()
    {
        // InvokeRepeating("DamageTarget", 1.0f, attackSpeed);
        InvokeRepeating("DamageTarget", 1.0f, attackSpeed);
        InvokeRepeating("FindTarget", 0.0f, 2.0f);
        InvokeRepeating("logStatus", 2.0f, 2.0f);
        //Debug.Log("InvokeRepeating Called");
    }

    void Update()
    {
        
    }

    void logStatus(){
        Debug.Log($"My target is {currentTarget}");
        Debug.Log($"My in range bool is {IsInRange}");
        Debug.Log($"My has enemy bool is {HasEnemy}");
        Debug.Log($"My moving bool is {moving}");
    }

//    public void FindTargets()
//    // Check all enemy units to see if they're in range (except current target). If they are then add them to a list of units in range.
//    {
//        var allEnemies = UnitManager.Instance.GetEntitiesAgainst(myTeam);
//        foreach (BaseUnit e in allEnemies)
//        {
//            if (e == null)
//            {
//                Debug.Log("Null reference in allEnemies list.");
//            }
//            else if (Vector3.Distance(e.transform.position, this.transform.position) <= range)
//            {
//                e.takeDamage(baseDamage);
//            }
//        }
//    }

//    public void checkTargets()
    // loop through enemiesInRange list and check if they're still in range. If they aren't, remove from list.
//    {
//        foreach (BaseUnit e in enemiesInRange)
//        {
//            if (Vector3.Distance(e.transform.position, this.transform.position) > range) ;
//            {
//                enemiesInRange.Remove(e);
//            }
//        }
//   }

//    public void DamageTargets()
//    // loop through enemiesInRange list and output damage.
//    {
//        attackDealt = false;
//        FindTargets();
//        foreach (BaseUnit e in enemiesInRange)
//        {
//            if (e == currentTarget)
//            {
//                e.takeDamage(baseDamage);
//                attackDealt = true;
//                Debug.Log("current target damaged");
//            }
//        if (attackDealt == false) // 
//        {
//            int index = Random.Range(0, enemiesInRange.Count);
//            BaseUnit randomEnemy = enemiesInRange[index];
//            randomEnemy.takeDamage(baseDamage);
//                Debug.Log("random target damaged");
//        }
//        }
//        checkTargets();
//    }

    public void Setup(Team team, Node currentNode)
    {
        myTeam = team;
        if(team == Team.enemyTeam) { // flip sprite by the x-axis for enemy units
            spriteRenderer.flipX = true;
        }

        this.currentNode = currentNode;
        transform.position = currentNode.worldPosition;
        currentNode.setOccupied(true);

        healthBar = Instantiate(healthBar, this.transform);
        healthBar.Setup(this.transform, baseHealth);
    }

    // find the enemy target
    public void FindTarget()
    {
        var allEnemies = UnitManager.Instance.GetEntitiesAgainst(myTeam);
        List<BaseUnit> activeEnemies = new List<BaseUnit>();
        foreach (BaseUnit enemy in allEnemies){
            if (enemy.dead == false){
                activeEnemies.Add(enemy);
                Debug.Log($"Adding {enemy} to active enemies list");
            }
        }

        float minDistance = Mathf.Infinity;
        BaseUnit entity = null;
        foreach (BaseUnit e in activeEnemies)
        {
            if (e == null)
            {
                //Debug.Log("Null reference in allEnemies list.");
            }
            else if (Vector3.Distance(e.transform.position, this.transform.position) <= minDistance)
            {
                minDistance = Vector3.Distance(e.transform.position, this.transform.position);
                entity = e;
            }
        }
        currentTarget = entity;
    }

    

    //Okay, should implement function here that then causes damage to target if they are within range. 
    //This function will have to be called every 2 seconds or something to get that repeating damange.
    //To cause damage to the target, it will have to look up the target object and then call its take damage function.
    //How exactly are we going to find the target though? It seems like the currentTarget variable does actually just store
    //A reference to this object. We could also possibly implement the damage search just by checking adjacent nodes, and damaging them
    //if they are occupied by an enemy. This probably makes more sense in case the pathfinding fucks up and we need to clear out enemies,
    //rather than just have them awkwardly staring at each other.
    //Fuck it, let's just do it the super naive way first. So we actually have an implementation.

    protected void DamageTarget(){
        var allEnemies = UnitManager.Instance.GetEntitiesAgainst(myTeam);
        if (IsInRange){
            currentTarget.takeDamage(baseDamage);
        }
        //foreach (BaseUnit e in allEnemies)
        //{
        //    try
        //    {   
        //        if (e == null){
        
        //        }
        //        else if (Vector3.Distance(e.transform.position, this.transform.position) <= range)
        //        {
        //            e.takeDamage(baseDamage);
        //            break;
        //        }
        //    }
        //    catch{
        //        //Debug.Log("Invalid enemy in list.");
        //    }
        // }
    }
    // keep moving to the enemy
    protected void GetInRange()
    {
          
        if(!moving)
        {
            Node candidateDestination = null;
            List<Node> candidates = GridManager.Instance.GetNodesCloseTo(currentTarget.CurrentNode);
            candidates = candidates.OrderBy(x => Vector3.Distance(x.worldPosition, this.transform.position)).ToList();
            for(int i = 0; i < candidates.Count;i++)
            {
                if (!candidates[i].isOccupied)
                {
                    candidateDestination = candidates[i];
                    break;
                }
            }
            if (candidateDestination == null)
                return;

            var path = GridManager.Instance.GetPath(currentNode, candidateDestination);
            if (path == null || path.Count <= 1)
                return; 

            if (path[1].isOccupied)
                return;

            path[1].setOccupied(true);
            destination = path[1];            
        }

        moving = !MoveTowards();
        if(!moving)
        {
            //Free previous node
            currentNode.setOccupied(false);
            currentNode = destination; 
        }
    }
    
    // keep moving to the enemy
    protected bool MoveTowards()
    {
        Vector3 direction = (destination.worldPosition - this.transform.position);
        if(direction.sqrMagnitude <= 0.005f)
        {
            transform.position = destination.worldPosition;
            return true;
        }

        this.transform.position += direction.normalized * movementSpeed * Time.deltaTime;
        return false;
    }

    public void takeDamage(int damage)
    {
        //Debug.Log("At least one Unit took damage");
        baseHealth -= damage; //minus damage from original health of unit
        healthBar.UpdateBar(baseHealth); //update healthbar with new health amount
        //Debug.Log($"After taking damage health was {baseHealth}");
        if (baseHealth <= 0 && !dead) //check health is above 0, if not unit dies :(
        {
            dead = true;
            //Debug.Log("One target destroyed");
            if (myTeam == Team.enemyTeam){
                UnitManager.Instance.enemyUnitsLost += 1;
            }
            else if (myTeam == Team.playerTeam){
                UnitManager.Instance.playerUnitsLost += 1;
            }
            else{
                //Debug.Log("A Unit that was neither a player or enemy unit has died. Something has defo gone wrong.");
            }
            //Debug.Log($"{UnitManager.Instance.enemyUnitsLost} enemies lost.");
            //Debug.Log($"{UnitManager.Instance.unitsByTeam[Team.enemyTeam].Count} enemies remaining.");
            //Debug.Log($"{UnitManager.Instance.playerUnitsLost}, allies lost.");
            //Debug.Log($"{UnitManager.Instance.unitsByTeam[Team.playerTeam].Count} allies remaining");
            currentNode.setOccupied(false);
            Destroy(gameObject);
            
        }
    }
}



