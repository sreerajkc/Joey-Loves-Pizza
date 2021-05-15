using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerControl : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool outOfBoundery=false;
    private Vector2 recentValidMousePos;

    private void Start() {
        spriteRenderer=GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        Vector2 mousePos=Input.mousePosition;
        mousePos=Camera.main.ScreenToWorldPoint(mousePos);
        transform.position=mousePos;
        if(!outOfBoundery)
        {
            recentValidMousePos=transform.position;
        }

    }

    public Vector3 GetMousePos()
    {
        return transform.position;
    }
    public Vector3 GetRecentValidMousePos()
    {
        return recentValidMousePos;
    }
    
    private void OnTriggerExit2D(Collider2D other) 
    {
        outOfBoundery=false;
        BounderyCheck(1f,false);
    }
    private void OnTriggerStay2D(Collider2D other) 
    {
        outOfBoundery=true;
        BounderyCheck(.3f,true);
    }
    private void BounderyCheck(float alpha,bool _outOfBoundery)
    {
        if(GameManager.instance.isSelected)
        {
            spriteRenderer.color=new Vector4(1,1,1,alpha);
        }
        else // if any object isn't selected
        {   
            spriteRenderer.color=new Vector4(1,1,1,1);
        }

    }
}
