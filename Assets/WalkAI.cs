using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAI: MonoBehaviour
{
    [Header("Walking Behaviour")]
    public float walkSpeed=2;
    public float maxDistance=1f;
    public float maxDepth=2f;
    public float distanceToObjectSide;
    private float distanceToGround;
    private bool canGoRight=false;
    private bool isInAlert=false;

    public RaycastHit2D hit2Dside,hit2Ddown;
    public Transform raycastOrigin;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Walk();
    }

    private void Walk()
    {
        if(distanceToObjectSide>=maxDistance && distanceToGround <= maxDepth)
        {
            transform.Translate(transform.TransformDirection(transform.right)*walkSpeed*Time.deltaTime);
        }
        else
        {
            canGoRight=!canGoRight;
            if(canGoRight)
            {
                transform.eulerAngles=new Vector3(0,0,0);
            }
            else
            {
                transform.eulerAngles=new Vector3(0,180,0);
            }
        }
    }
    private void FixedUpdate() 
    {   
        RaycastSide();
        RaycastDown();
    }
    private void RaycastSide()// Magnitude is the direction sign
    {
        hit2Dside=Physics2D.Raycast(raycastOrigin.position,transform.TransformDirection(Vector2.right),LayerMask.NameToLayer("Collidable"));
        distanceToObjectSide=Vector2.Distance(transform.position,hit2Dside.point);
    }
    private void RaycastDown()
    {
        hit2Ddown=Physics2D.Raycast(raycastOrigin.position,transform.TransformDirection(Vector2.down));
        distanceToGround=Vector2.Distance(transform.position,hit2Ddown.point);
    }

}
