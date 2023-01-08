using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class zombieManager : MonoBehaviour
{
    GameObject player;
    GameObject playerManager;
    GameObject zombieChild;
    GameObject bloodPS;
    GameObject bloodChild1;
    GameObject bloodChild2;

    public zombiePathfinder zombiePathFinderScript;
    public zombieAnimation zombieAnimationScript;
    playerManager playerManagerScript;
    Transform playerPosition;

    float distanceFromPlayer;
    float attackDistance;
    int zombieAttackDamage;

    public bool hasToAttack;
    public bool isDying;
    public bool isZombieHit;

    public int zombieHealth;

    private void Start()
    {
        zombieChild = transform.GetChild(1).gameObject; //zombie body
        hideFromCamera();

        player = GameObject.Find("player1");
        playerManager = GameObject.Find("playerManager");

        bloodPS = transform.GetChild(2).gameObject;
        bloodChild1 = bloodPS.transform.GetChild(0).gameObject;
        bloodChild2 = bloodPS.transform.GetChild(1).gameObject;
        getBloodFX(0); //hide

        playerManagerScript = playerManager.GetComponent<playerManager>();
        playerPosition = player.GetComponent<Transform>();
        zombieHealth = 50;
        attackDistance = 2;
        zombieAttackDamage = 100;
        isZombieHit = false;
        isDying = false;
        hasToAttack = false;
    }

    bool stopCounting = true;
    float timer = 0;

    private void Update()
    {
        distanceFromPlayer = Vector3.Distance(
            playerPosition.transform.position,
            transform.position
        );

        //setting the hasToAttack and changing speed to 0 when attacking for the animations
        if (distanceFromPlayer < attackDistance)
        {
            hasToAttack = true;
            this.zombiePathFinderScript.zombieSpeed = 0;
        }

        if (
            hasToAttack
            && zombieAnimationScript.zombieAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime
                > 1
            && !zombieAnimationScript.zombieAnimator.IsInTransition(0)
            && zombieAnimationScript.zombieAnimator.GetCurrentAnimatorStateInfo(0).IsName("attack")
        )
        { //if animation "attack" has finished.
            this.zombiePathFinderScript.zombieSpeed = this.zombiePathFinderScript.defaultZombieSpeed;
            hasToAttack = false;
        }

        if (isZombieHit)
        {
            getBloodFX(1); //show

            timer = 0;
            stopCounting = false;
            isZombieHit = false;
        }

        if (!stopCounting)
        {
            timer += Time.deltaTime;
        }

        if (timer > 0.4f)
        {
            getBloodFX(0); //hide
            stopCounting = true;
            isZombieHit = false;
            timer = 0;
        }

        if (zombieHealth <= 0)
        {
            isDying = true;
            Destroy(gameObject.GetComponent<Rigidbody>());
            Destroy(gameObject.GetComponent<CapsuleCollider>());
            zombiePathFinderScript.toggleNavMeshAgent(0);
        }
    }

    public void finishedZombieAttack()
    { //finished attacking (the receiver is the event on the animation)
        //if the player is still in the zombie attack range, he will lose some hp
        if (distanceFromPlayer < attackDistance * 2)
        {
            playerManagerScript.playerHealth -= zombieAttackDamage;
            Debug.Log("you lost " + zombieAttackDamage + " hp");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "zombieTier1")
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            ShowZombieChildOnCamera();
        }
    }

    int hideZombieLayer;

    public void hideFromCamera()
    {
        hideZombieLayer = LayerMask.NameToLayer("hideZombieTier1");

        zombieChild.layer = hideZombieLayer;
    }

    public void getBloodFX(int hide_show)
    {
        if (hide_show == 0)
        {
            int bloodFXArray = LayerMask.NameToLayer("blood");
            bloodPS.layer = bloodFXArray;
            bloodChild1.layer = bloodFXArray;
            bloodChild2.layer = bloodFXArray;
        }
        else if (hide_show == 1)
        {
            bloodPS.layer = 0; //default layer
            bloodChild1.layer = 0;
            bloodChild2.layer = 0;
        }
    }

    public void ShowZombieChildOnCamera()
    {
        zombieChild.layer = 0;
    }
}
