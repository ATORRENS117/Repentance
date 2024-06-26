using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpAttackTrigger : MonoBehaviour
{
    [SerializeField] private GameObject attackArea;

    public bool attacking = false;

    private float timeToAttack = 0.3f;
    private float timer = 0f;
    private float animationLength = 1; //Set this to whatever time you need for wind up animation
    private bool windUpAnimationComplete = false;
    private bool closeEnoughToAttack = false;
    private Animator anim;

    [SerializeField] GameObject enemyPathFinding;
    [SerializeField] GameObject impAttackScript;
    public ImpAttackScript impAttackScriptRef;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        
        //find the imp attack script from the child object 'ImpAttackArea'
        if (transform.Find("ImpAttackArea").GetComponent<ImpAttackScript>() != null)
        {
            impAttackScript = transform.Find("ImpAttackArea").gameObject;
            impAttackScriptRef = impAttackScript.GetComponent<ImpAttackScript>();
        }
        else
        {
            print("MISSING IMP ATTACK SCRIPT");
        }
        
        
    }
    void Update()
    {
        closeEnoughToAttack = enemyPathFinding.GetComponent<EnemyPathfinding>().attackDistance;
        if (closeEnoughToAttack)
        {
            EnemyPathfinding EnemyMovement = enemyPathFinding.GetComponent<EnemyPathfinding>();
            //ImpAttackScript ImpAtt = impAttackScript.GetComponent<ImpAttackScript>();
            EnemyMovement.EnablePreventFlipBody();
            impAttackScriptRef.EnablePreventFlipAttackArea();
            attacking = true;
            
        }
        else
        {
            attacking = false;
            EnemyPathfinding EnemyMovement = enemyPathFinding.GetComponent<EnemyPathfinding>();
            //ImpAttackScript ImpAtt = impAttackScript.GetComponent<ImpAttackScript>();
            EnemyMovement.DisablePreventFlipBody();
            impAttackScriptRef.DisablePreventFlipAttackArea();
        }
        if (attacking)
        {
            anim.SetBool("Attacking", true); 
            attackArea.SetActive(true);
        }
        else
        {
            attackArea.SetActive(false);
            anim.SetBool("Attacking", false);
        }
    }


}