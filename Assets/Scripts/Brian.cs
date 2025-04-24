using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Brian : MonoBehaviour
{
    public float walkSpeed;
    public AudioClip[] footstepSound;
    public float footstepVolume;
    public int numCubes;
    public Vector3 exitLocation;
    public GameObject exitMeshes;

    Animator animator;
    NavMeshAgent agent;
    GameObject player;
    List<GameObject> floatingObjects;
    GameObject cubeField;
    GameObject cubeToChase;
    bool thunderdomeInited = false;
    float stopCooldown = -1.0f;
    float lv;
    int numCubesEaten = 0;
    bool full = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        exitMeshes.SetActive(false);

        floatingObjects = new List<GameObject>();
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        cubeField = GameObject.Find("CubeField");

        foreach (Transform child in cubeField.transform)
        {
            GameObject floatingObject = child.gameObject;
            floatingObjects.Add(floatingObject);
        }

        cubeToChase = floatingObjects[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(thunderdomeInited)
        {
            transform.position = new Vector3(transform.position.x, 0.495f, transform.position.z);
            transform.LookAt(agent.destination);
            transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y, 0.0f);

            
            float closestCubeDistance = float.PositiveInfinity;
            cubeToChase = null;

            if(!full)
            {
                foreach (Transform cube in cubeField.transform)
                {
                    float cubeDistance = Vector3.Distance(cube.position, transform.position);
                
                    if (cubeDistance < closestCubeDistance && cube.transform.position.y < 3.0f)
                    {
                        closestCubeDistance = cubeDistance;
                        cubeToChase = cube.gameObject;
                    }
                }
            }

            if (cubeToChase != null)
            {
                if (closestCubeDistance > 1.0f)
                {
                    Rigidbody rb = cubeToChase.GetComponent<Rigidbody>();

                    lv = rb.linearVelocity.magnitude;

                    if(stopCooldown < 0.0f && lv > 0.1f)
                    {
                        agent.destination = cubeToChase.transform.position;
                        animator.SetBool("walk", true);
                    }
                    else
                    {
                        animator.SetBool("walk", false);
                    }

                    if (lv < 2.0f && closestCubeDistance < 1.1f && stopCooldown < 0.0f)
                    {
                        stopCooldown = 5.0f;
                        cubeToChase.GetComponent<FloatingObject>().InitializeTheGrowth();
                        cubeToChase = null;
                        numCubesEaten++;
                        
                        if (numCubesEaten > numCubes)
                        {
                            full = true;
                            cubeToChase = null;
                            stopCooldown = -1;
                            exitMeshes.SetActive(true);
                        }
                    }

                    if(stopCooldown > 0.0f)
                    {
                        stopCooldown -= Time.deltaTime;
                    }

                    if(full)
                    {
                        agent.destination = exitLocation;
                    }
                }
            }
            else if (!full)
            {
                animator.SetBool("walk", false);
            }

            if (full)
            {
                Debug.Log(Vector3.Distance(transform.position, agent.destination));
                animator.SetBool("walk", Vector3.Distance(transform.position, agent.destination) > 1);
            }
        }
    }

    public void InitThunderdome()
    {
        thunderdomeInited = true;
    }

    float Remap(float value)
    {
        float fromMin = 2.0f;
        float fromMax = 25.0f;
        float toMin = 0.15f;
        float toMax = 0f;

        return toMin + (toMax - toMin) * ((value - fromMin) / (fromMax - fromMin));
    }

    public void FootstepBrian()
    {
        float distanceVolume = Remap(Vector3.Distance(transform.position, player.transform.position));
        footstepVolume = distanceVolume;
        SoundManager.instance.PlaySound(footstepSound[Random.Range(0, 8)], gameObject.transform, footstepVolume);
    }
}
