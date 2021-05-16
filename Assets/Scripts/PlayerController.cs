using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public Sprite deathSprite,fallSprite;
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
  private void OnTriggerEnter2D(Collider2D other) {
      if(other.CompareTag("Pizza"))
        {
            other.gameObject.SetActive(false);
            int nxtLvl=SceneManager.GetActiveScene().buildIndex + 1;
            GameManager.instance.SaveGame(nxtLvl);
            SceneManager.LoadScene(nxtLvl);
            Debug.Log("You won");
        }
  }

    private void NextRound()
    {

    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        _walkAI.enabled=true;
        animator.enabled=true;
    }
    private void OnCollisionExit2D(Collision2D other) 
    {
        if(other.collider.CompareTag("Ground"))
        {
            _walkAI.enabled=false;
            animator.enabled=false;
            spriteRenderer.sprite=fallSprite;
        }
    }
}
