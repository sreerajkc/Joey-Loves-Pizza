using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectUI : MonoBehaviour
{
    [SerializeField]
    int id;
    public SpriteRenderer sprite;
    public bool isSelected=false;    
    public void Initialize(int _id)
    {
        id=_id;
    }
    public void OnMouseDown()
    {
        if(!GameManager.instance.isSelected)
        {
            isSelected=true;
        }
        else if(GameManager.instance.id==id)
        {   
            isSelected=false;
        }
        else if(GameManager.instance.isSelected && GameManager.instance.id!=id)
        {
            GameManager.instance.Unselect();
            isSelected=true;
        }
        ColorChange();
        GameManager.instance.Select(id, isSelected);
    }
    public void ColorChange()
    {
        if (isSelected)
        {
            sprite.color = new Vector4(1, 1, 1, .5f);
        }
        else
        {
            sprite.color = new Vector4(1, 1, 1, 1);
        }
    }
}
