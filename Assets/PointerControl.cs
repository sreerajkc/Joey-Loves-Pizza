using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerControl : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Start() {
        spriteRenderer=GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        Vector2 mousePos=Input.mousePosition;
        mousePos=Camera.main.ScreenToWorldPoint(mousePos);
        transform.position=mousePos;
    }

    public Vector3 GetMousePos()
    {
        return transform.position;
    }
    
    private void OnTriggerExit2D(Collider2D other) 
    {
        BounderyCheck(1f,false);
    }
    private void OnTriggerStay2D(Collider2D other) 
    {
        BounderyCheck(.3f,true);
    }
    private void BounderyCheck(float alpha,bool outOfBoundery)
    {
        if(GameManager.instance.isSelected)
        {
            GameManager.instance.isOutOfBoundary=outOfBoundery;
            spriteRenderer.color=new Vector4(1,1,1,alpha);
        }
        else // if any object isn't selected
        {   
            spriteRenderer.color=new Vector4(1,1,1,1);
        }

    }
}
