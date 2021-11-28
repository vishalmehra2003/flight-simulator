using System.ComponentModel;
using System.Timers;
using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Airplane : MonoBehaviour
{

    public float thrust=0f;
    public float topSpeed=25f;
    public float defaultLift;
    public float maxSpeedIncrement = 5f;
    Rigidbody rb;
    public float fallRate;
    public float dynamicLift;
    public Slider thrustSlider;
    public float glideRate;
    public bool isGrounded;
    public GameObject leftFlap;
    public GameObject rightFlap;
    float speedIncrement = 0f;
    bool isSpeedingUp;
    float prevSpeedIncrement;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        thrustSlider.onValueChanged.AddListener(delegate{ValueChangeCheck();});
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // if(rb.velocity.magnitude < topSpeed) {
        //      rb.AddRelativeForce(new Vector3(0,defaultLift,thrust)); 
        //     rb.velocity = TransformBlock.forward *thrust;

        // }
        if(isSpeedingUp)
        {thrust += speedIncrement *Time.deltaTime;}
        else{
            thrust -= maxSpeedIncrement *Time.deltaTime;

            }
            thrust = Mathf.Clamp(thrust,0f,topSpeed);



        transform.Translate(0,0,(thrust+(glideRate*20))*Time.deltaTime);
        rb.velocity = new Vector3(0f,rb.velocity.y *(fallRate*2),0);
        transform.Rotate(dynamicLift ,0,0);
        if(Input.GetKey(KeyCode.W)){
            transform.Rotate(.5f,0,0);
            leftFlap.transform.Rotate(5f,0,0);
            leftFlap.transform.localEulerAngles = new Vector3(Mathf.Clamp(leftFlap.transform.localEulerAngles.x,20 ,20),0,0);
             rightFlap.transform.Rotate(5f,0,0);
            rightFlap.transform.localEulerAngles = new Vector3(Mathf.Clamp(leftFlap.transform.localEulerAngles.x,20 ,20),0,0);
            }
            
        else{
            if(leftFlap.transform.localRotation.x>0){
                leftFlap.transform.Rotate(-5f,0,0);
            }
            else if(rightFlap.transform.localRotation.x>0){
                rightFlap.transform.Rotate(-5f,0,0);
            }


            
            
            }
        if(Input.GetKey(KeyCode.S)){
         transform.Rotate(-.5f,0,0);
            leftFlap.transform.Rotate(-5f,0,0);
            leftFlap.transform.localEulerAngles = new Vector3(Mathf.Clamp(leftFlap.transform.localEulerAngles.x,-20 ,-20),0,0);
            rightFlap.transform.Rotate(-5f,0,0);
            rightFlap.transform.localEulerAngles = new Vector3(Mathf.Clamp(leftFlap.transform.localEulerAngles.x,-20 ,-20),0,0);
            }
        else{
            if(leftFlap.transform.localRotation.x<0 &&rightFlap.transform.localRotation.x<0){
                leftFlap.transform.Rotate(5f,0,0);
                                rightFlap.transform.Rotate(5f,0,0);

            }
            if(rightFlap.transform.localRotation.x<0){
                rightFlap.transform.Rotate(5f,0,0);
            }
        
        
        }

        if(Input.GetKey(KeyCode.A)){
            
                transform.Rotate(0,0,.5f);
                
            leftFlap.transform.Rotate(5f,0,0);
            leftFlap.transform.localEulerAngles = new Vector3(Mathf.Clamp(leftFlap.transform.localEulerAngles.x,20 ,20),0,0);

        }
        else{
            if(leftFlap.transform.localRotation.x>0){
                leftFlap.transform.Rotate(-5f,0,0);
            }
        }
        

        if(Input.GetKey(KeyCode.D))
        {
        
            transform.Rotate(0,0,-.5f);
            rightFlap.transform.Rotate(5f,0,0);
            rightFlap.transform.localEulerAngles = new Vector3(Mathf.Clamp(leftFlap.transform.localEulerAngles.x,20 ,20),0,0);
            }
            else{
            if(rightFlap.transform.localRotation.x>0){
                rightFlap.transform.Rotate(-5f,0,0);
            }
            
                if(Input.GetKey(KeyCode.Q)){transform.Rotate(0,-0.5f,0);}
                }

        if(Input.GetKey(KeyCode.E)){transform.Rotate(0,.5f,0);}


        
    }

     void ValueChangeCheck(){
        speedIncrement = thrustSlider.value*maxSpeedIncrement;

         if(speedIncrement>=prevSpeedIncrement){isSpeedingUp = true;}
         else if(speedIncrement<=prevSpeedIncrement)
              {isSpeedingUp=false;}
                 prevSpeedIncrement = speedIncrement;

        dynamicLift = thrustSlider.value*defaultLift;
        fallRate = 0.5f-thrustSlider.value;
        if(!isGrounded){        
            glideRate = 1f-thrustSlider.value;
            }
    }

     void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag =="Ground"){
            isGrounded=true;
                        print("Grounded");

                        
                
            //glideRate = Mathf.Lerp(glideRate,0f,0.00001f);
            //glideRate += 10f * Time.deltaTime;
            if(glideRate>0.0001f){
            StartCoroutine(ExampleCoroutine());
                }
        }
    }

        void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag =="Ground"){
        isGrounded = false;
        }
    }
 IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.
        

        //yield on a new YieldInstruction that waits for 5 seconds.
        while(glideRate>0.001f){
            yield return new WaitForSeconds(0.05f);
              glideRate-=0.005f;
              glideRate = Mathf.Clamp(glideRate,0f,topSpeed);
        }
      
        //After we have waited 5 seconds print the time again.
        
    }



}
