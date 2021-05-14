using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    Vector2 colliderSize;
    public Sprite deathSprite;
    public Sprite attackSprite;
    private WalkAI _walkAI;
    private SelectUI select;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private Animator animator;
    

    public float chaseRadius=2;
    public float attackRadius=2;
    private void Start() {
        _walkAI=GetComponent<WalkAI>();
        spriteRenderer=GetComponent<SpriteRenderer>();
        boxCollider=GetComponent<BoxCollider2D>();
        animator=GetComponent<Animator>();
        select=GetComponent<SelectUI>();
    }
    private void Update()
    {
        Alerted();
    }

    private void Alerted()
    {
        if (_walkAI.hit2Dside.collider.CompareTag("Player") && _walkAI.distanceToObjectSide <= chaseRadius)
        {
            animator.SetBool("isAlert", true);
            _walkAI.walkSpeed = 4;
            if (_walkAI.distanceToObjectSide <= attackRadius)
            {
                Attack();
                
            }
        }
        else
        {
            animator.SetBool("isAlert", false);
            _walkAI.walkSpeed = 2;
        }
    }

    private void Attack()
    {
        _walkAI.enabled = false;
        animator.enabled = false;
        GameManager.instance.gameObject.SetActive(false);
        spriteRenderer.sprite = attackSprite;
        if(select.isSelected)// change sprite alpha if selected
        {
            select.isSelected=false;
            select.ColorChange();
        }
        _walkAI.hit2Dside.collider.gameObject.GetComponent<PlayerController>().Dead();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Death(other);
    }


    private void Death(Collision2D other)
    {
        if (other.collider.CompareTag("Box"))
        {
            animator.enabled = false;
            _walkAI.enabled = false;
            boxCollider.size = colliderSize;
            boxCollider.offset = new Vector2(0, (-.5f));
            spriteRenderer.sprite = deathSprite;
            StartCoroutine("DeathDuration");

        }
    }

    IEnumerator DeathDuration()
    {
        yield return new WaitForSeconds(.5f);
        gameObject.SetActive(false);
    }
}
