using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batteryGun : MonoBehaviour
{
    int batteryGunDamage;
    float batteryGunRange;
    float shootDelay;
    float continuousShootDelay;
    float timer;
    float timer1;

    GameObject laserEndPoint;
    Light[] allLights;
    Light flashlight;
    bool playerHasShot;
    bool playerCanShoot;
    bool playerPendingToShoot;

    ParticleSystem[] searchGunshot;
    ParticleSystem gunshot;

    private void Start()
    {
        shootDelay = 0.4f;
        continuousShootDelay = 0.3f;
        batteryGunDamage = 20;
        batteryGunRange = 50;
        timer = 0;
        timer1 = 0;
        playerHasShot = false;
        playerCanShoot = true;
        playerPendingToShoot = false;
        laserEndPoint = GameObject.Find("laserEndPoint");

        allLights = FindObjectsOfType<Light>();
        for (int i = 0; i < allLights.Length; i++)
        {
            if (allLights[i].name == "flashlight")
            {
                flashlight = allLights[i];
            }
        }

        //searching "gunshot" particle system by name
        searchGunshot = FindObjectsOfType<ParticleSystem>();
        for (int i = 0; i < searchGunshot.Length; i++)
        {
            if (searchGunshot[i].name == "gunshot")
            {
                gunshot = searchGunshot[i];
            }
        }
    }

    public Camera cam;
    RaycastHit hitInfo;

    void Shoot()
    {
        gunshot.Play();

        if (
            Physics.Raycast(
                cam.transform.position,
                cam.transform.forward,
                out hitInfo,
                batteryGunRange
            )
        )
        {
            if (hitInfo.transform.tag == "zombieTier1")
            {
                GameObject zombieHit = hitInfo.transform.gameObject; //the specific zombie player hit
                zombieHit.GetComponent<zombieManager>().zombieHealth -= batteryGunDamage; //i do this because there are many zombieTier1s but i want to change only the value of the zombie that i hit
                zombieHit.GetComponent<zombieManager>().isZombieHit = true;
                Debug.Log(
                    zombieHit.name
                        + ": "
                        + batteryGunDamage
                        + "/"
                        + zombieHit.GetComponent<zombieManager>().zombieHealth
                );
            }
        }
    }

    [SerializeField]
    Transform gunAim;

    [SerializeField]
    LineRenderer laserSight;
    RaycastHit laserHitInfo;

    bool stopCounting;

    private void Update()
    {
        //if the player is just pressing click once
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (playerCanShoot)
            {
                Shoot();
                playerHasShot = true;
            }
            else
            {
                //if player tries to shoot before the shootdelay
                playerHasShot = false;
                playerPendingToShoot = true;
            }
        }

        if (playerHasShot)
        {
            playerCanShoot = false;

            timer += Time.deltaTime;

            if (timer >= shootDelay)
            {
                timer = 0;
                playerCanShoot = true;
                playerHasShot = false;
            }
        }

        //if the player presses click before the shootDelay time (it means that he is click spamming at least 2 times)
        if (playerPendingToShoot)
        {
            timer += Time.deltaTime;

            if (timer >= shootDelay)
            {
                Shoot();
                timer = 0;
                playerPendingToShoot = false;
                playerHasShot = true;
            }
        }

        //if player is pressing continuously click:
        if (Input.GetKey(KeyCode.Mouse0))
        {
            timer1 += Time.deltaTime;

            if (timer1 > continuousShootDelay)
            {
                Shoot();
                timer1 = 0;
            }
        }

        //if the player raised the click, it means that does not want to press it continuously - though the timer1 already started counting on the "continuously click pressing" code so i have to reset it. Else, the player will have way more clicks (in a tiny amount of time) than he should)
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            timer1 = 0;
        }
    }

    int minimumIdealLaserWidthRatio = 200;
    int maximumIdealLaserWidthRatio = 350;
    Vector3 minimumIdealDistance_ScaleRatio = new Vector3(50, 50, 50);
    Vector3 maximumIdealDistance_ScaleRatio = new Vector3(70, 70, 70);

    private void LateUpdate()
    {
        //laser sight

        if (
            Physics.Raycast(
                cam.transform.position,
                cam.transform.forward,
                out laserHitInfo,
                batteryGunRange
            )
        )
        {
            laserSight.enabled = true;
            laserEndPoint.SetActive(true);
            laserSight.SetPosition(0, gunAim.transform.position);
            laserSight.SetPosition(1, laserHitInfo.point);

            laserEndPoint.transform.position = laserHitInfo.point;

            //keep laserEndPoint in a realistic size range regarding distance

            float laser_CameraDistance = Vector3.Distance(
                cam.transform.position,
                laserHitInfo.point
            );
            Vector3 Distance_ScaleRatio = new Vector3(
                laser_CameraDistance / laserEndPoint.transform.localScale.x,
                laser_CameraDistance / laserEndPoint.transform.localScale.y,
                laser_CameraDistance / laserEndPoint.transform.localScale.z
            );
            //Debug.Log("Distance_ScaleRatio: " + Distance_ScaleRatio);

            if (
                Distance_ScaleRatio.x < minimumIdealDistance_ScaleRatio.x
                && Distance_ScaleRatio.y < minimumIdealDistance_ScaleRatio.y
                && Distance_ScaleRatio.z < minimumIdealDistance_ScaleRatio.z
            )
            {
                laserEndPoint.transform.localScale = new Vector3(
                    laser_CameraDistance / minimumIdealDistance_ScaleRatio.x,
                    laser_CameraDistance / minimumIdealDistance_ScaleRatio.y,
                    laser_CameraDistance / minimumIdealDistance_ScaleRatio.z
                );
            }
            if (
                Distance_ScaleRatio.x > maximumIdealDistance_ScaleRatio.x
                && Distance_ScaleRatio.y > maximumIdealDistance_ScaleRatio.y
                && Distance_ScaleRatio.z > maximumIdealDistance_ScaleRatio.z
            )
            {
                laserEndPoint.transform.localScale = new Vector3(
                    laser_CameraDistance / maximumIdealDistance_ScaleRatio.x,
                    laser_CameraDistance / maximumIdealDistance_ScaleRatio.y,
                    laser_CameraDistance / maximumIdealDistance_ScaleRatio.z
                );
            }

            //keep laserSight in a realistic size range regarding distance
            float Distance_ScaleLaserRatio = laser_CameraDistance / laserSight.endWidth;

            if (Distance_ScaleLaserRatio < minimumIdealLaserWidthRatio)
            {
                laserSight.endWidth = laser_CameraDistance / minimumIdealLaserWidthRatio;
            }
            if (Distance_ScaleLaserRatio > maximumIdealLaserWidthRatio)
            {
                laserSight.endWidth = laser_CameraDistance / maximumIdealLaserWidthRatio;
            }
            //Debug.Log("Distance_ScaleLaserRatio: " + Distance_ScaleLaserRatio);

            if (laser_CameraDistance < 2)
            {
                laserSight.enabled = false;
                laserEndPoint.SetActive(false);
            }
            else
            {
                laserSight.enabled = true;
                laserEndPoint.SetActive(true);
            }
        }
        else
        {
            laserSight.enabled = false;
            laserEndPoint.SetActive(false);
        }
    }
}
