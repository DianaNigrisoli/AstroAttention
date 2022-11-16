using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    //private CharacterController controller; 
    private Rigidbody controller;
    public int score;

    private int desiredLane = 0; // -1:left 0:middle, 1:right
    public float laneDistance; //the distance between lane - to fix in Unity env

    private Vector3 direction;
    public float forwardSpeed=5f; 
    
    // Start is called before the first frame update
    // void Awake()
    // {
    //     rb = GetComponent<Rigidbody>();
    // }

    private void Start()
    {
        controller = GetComponent<Rigidbody>(); 
        
    }

    // Update is called once per frame
    void Update()
    {
        direction.z = forwardSpeed;
        
        // Check lane position 
       if (Input.GetKeyDown(KeyCode.RightArrow))
       {
           desiredLane++;
           if (desiredLane == 2)
               desiredLane = 1;
       }
       if (Input.GetKeyDown(KeyCode.LeftArrow))
       {
           desiredLane--;
           if (desiredLane == -2)
               desiredLane = -1;
       }
       
       // Calculate new position
       Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

       if (desiredLane == -1)
       {
           targetPosition += Vector3.left * laneDistance; 
           
       } else if (desiredLane == 1)
       {
           targetPosition += Vector3.right * laneDistance; 
       }

       transform.position = targetPosition; 
    }

    private void FixedUpdate()
    {
        controller.MovePosition(transform.position + direction*Time.fixedDeltaTime);
        
    }
    


    // private void OnCollisionEnter(Collision other)
    // {
    //     if (other.collider.CompareTag("Pumpkin"))
    //     {
    //         score++;
    //         AudioManager.instance.PlayCorrect();
    //         Destroy(other.gameObject);
    //     }
    //     else if (other.collider.CompareTag("FinishLine"))
    //     {
    //         Win();
    //     }
    // }

    // private void Win()
    // {
    //     GameManager.instance.score = this.score;
    //     SceneManager.LoadScene("FinalScene");
    // }
}
