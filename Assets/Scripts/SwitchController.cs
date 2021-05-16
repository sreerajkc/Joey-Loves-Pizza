using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public Sprite Pressed;
    private SpriteRenderer spriteRenderer;
    public Animator doorAnimator;
    private bool isPressed=false;
    private void Start() {
        spriteRenderer=GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player") && !isPressed)
        {
            spriteRenderer.sprite=Pressed;
            doorAnimator.SetTrigger("isPressed");
            isPressed=true;
            StartCoroutine("Disable");
        }
    }
    IEnumerator Disable()
    {
        yield return new WaitForSeconds(1f);
        transform.parent.gameObject.SetActive(false);
    }
}
