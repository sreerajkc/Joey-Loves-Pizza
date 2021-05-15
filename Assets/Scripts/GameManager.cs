using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    [HideInInspector]
    public int id=-1;
    public int maxMoves=3;
    [HideInInspector]
    public List<SelectUI> selectables;
    [Header("Scripts")]
    public PointerControl pointerControl;
    public UIController uImanager;
    [HideInInspector]
    public static GameManager instance;
    [HideInInspector] 
    public bool isSelected=false,isOutOfBoundary=false;
    bool isBeingCopied=false,isCut=false;

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
    private void Start() {
        Cursor.visible=false;
    }

    private void Update() {
        Debug.Log(maxMoves);
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
        if (isSelected && maxMoves>0)
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
                CutOperation(id);
            }
            else if(isBeingCopied && selectables[id].canBeReplicated && maxMoves>=2)
            {
                MoveDecrement(2);
                Unselect();
                GameObject obj=Instantiate(selectables[id].gameObject,pastePos,Quaternion.identity);
                selectables.Add(obj.GetComponent<SelectUI>());
                obj.GetComponent<SelectUI>().Initialize(selectables.Count-1);
                id=selectables.Count-1;
                Unselect();
            }
        }
    }

    private void CutOperation(int id)
    {
        MoveDecrement(1);
        Unselect();
        selectables[id].gameObject.SetActive(true);
        selectables[id].gameObject.transform.position = pastePos;
        isSelected = false;
        isCut = true;
    }

    private void Cut(int id)
    {
        if(maxMoves>0)
        {
            selectables[id].gameObject.SetActive(false);
            isBeingCopied=false;
            isCut=false;
            StartCoroutine("AutoPaste");
        }
    }
    private void Copy(int id)
    {
        isBeingCopied=true;
    }

    IEnumerator AutoPaste()
    {
        yield return new WaitForSeconds(5f);
        if(isSelected && !isCut)
        {
            pastePos=pointerControl.GetRecentValidMousePos();
            CutOperation(id);
        }
        
        
    }
    private void MoveDecrement(int _moveNo)
    {
        maxMoves-=_moveNo;
        uImanager.SetMoveNo(maxMoves);

    }
        


}
