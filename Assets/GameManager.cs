using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int id=-1;
    public List<SelectUI> selectables;
    public PointerControl pointerControl;
    public static GameManager instance; 
    public bool isSelected=false,isOutOfBoundary=false;
    bool isBeingCopied=false;

    private Vector3 pastePos;
    private void Awake() 
    {
        instance=this;
        int order=0;
        foreach(SelectUI selectable in selectables)
        {
            selectable.Initialize(order);
            order++;
        }
    }

    private void Update() {
        Shortcuts();
    }

    public void Select(int _id,bool _isSelected)
    {
        id=_id;
        isSelected=_isSelected;
    }
    public void Unselect()
    {
        //isSelected=false;
        selectables[id].isSelected=false;
        selectables[id].ColorChange();
    }
    private void Shortcuts()
    {
        if (isSelected)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                if (Input.GetKeyUp(KeyCode.C))
                {
                    Copy(id);
                }
                else if (Input.GetKeyUp(KeyCode.X))
                {
                    Cut(id);
                }
                else if (Input.GetKeyUp(KeyCode.V))
                {
                    Paste(id);
                }
            }
        }
    }
    private void Paste(int id)
    {
        if(!isOutOfBoundary)
        {
            pastePos=pointerControl.GetMousePos();// to get current mouse position to paste
            if(!selectables[id].gameObject.activeInHierarchy)
            {
                Unselect();
                selectables[id].gameObject.SetActive(true);
                selectables[id].gameObject.transform.position=pastePos;
                isSelected=false;
            }
            else if(isBeingCopied)
            {
                Unselect();
                GameObject obj=Instantiate(selectables[id].gameObject,pastePos,Quaternion.identity);
                selectables.Add(obj.GetComponent<SelectUI>());
                obj.GetComponent<SelectUI>().Initialize(selectables.Count-1);
                id=selectables.Count-1;
                Unselect();
            }
        }
    }
    private void Cut(int id)
    {
        selectables[id].gameObject.SetActive(false);
        isBeingCopied=false;
    }
    private void Copy(int id)
    {
        isBeingCopied=true;
    }
        

}
