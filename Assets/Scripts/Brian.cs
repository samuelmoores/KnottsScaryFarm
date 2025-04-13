using System.Collections.Generic;
using UnityEngine;

public class Brian : MonoBehaviour
{
    public float walkSpeed;

    Animator animator;
    CharacterController controller;
    GameObject player;
    List<GameObject> floatingObjects;
    GameObject cubeField;
    GameObject cubeToChase;
    bool thunderdomeInited = false;
    float stopCooldown = -1.0f;
    float lv;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        floatingObjects = new List<GameObject>();
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
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

            float closestCubeDistance = float.PositiveInfinity;
            cubeToChase = null;

            foreach (Transform cube in cubeField.transform)
            {
                float cubeDistance = Vector3.Distance(cube.position, transform.position);
                
                if (cubeDistance < closestCubeDistance && cube.transform.position.y < 3.0f)
                {
                    closestCubeDistance = cubeDistance;
                    cubeToChase = cube.gameObject;
                }
            }

            if (cubeToChase != null)
            {
                transform.LookAt(cubeToChase.transform);
                transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y, 0.0f);

                if (closestCubeDistance > 1.0f)
                {
                    Vector3 direction = (cubeToChase.transform.position - transform.position).normalized;
                    Vector3 moveDirection = new Vector3(direction.x, 0.0f, direction.z);
                    Rigidbody rb = cubeToChase.GetComponent<Rigidbody>();
                    lv = rb.linearVelocity.magnitude;

                    if(stopCooldown < 0.0f && lv > 0.0f)
                    {
                        controller.Move(moveDirection * Time.deltaTime * walkSpeed);
                        animator.SetBool("walk", true);
                    }
                    else
                    {
                        animator.SetBool("walk", false);
                    }

                    if (lv < 2.0f && closestCubeDistance < 1.1f && stopCooldown < 0.0f)
                    {
                        stopCooldown = 5.0f;
                    }

                    if(stopCooldown > 0.0f)
                    {
                        stopCooldown -= Time.deltaTime;
                    }
                }
            }
            else
            {
                animator.SetBool("walk", false);
            }
        }
    }

    public void InitThunderdome()
    {
        thunderdomeInited = true;
    }
}
