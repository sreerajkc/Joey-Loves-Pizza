using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Sprite deathSprite;
    private WalkAI _walkAI;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private SelectUI select;
    
    private void Start() {
        _walkAI=GetComponent<WalkAI>();
        spriteRenderer=GetComponent<SpriteRenderer>();
        animator=GetComponent<Animator>();
        select=GetComponent<SelectUI>();
    }
    public void Dead()
    {
        _walkAI.enabled=false;
        spriteRenderer.sprite=deathSprite;
        animator.enabled=false;
        if(select.isSelected)// change sprite Alpha if selected
        {
            select.isSelected=false;
            select.ColorChange();
        }
    }
}
