using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI moveNo;

    private void Start() 
    {
        moveNo.text=GameManager.instance.maxMoves.ToString();
    }
    public void SetMoveNo(int _moveNo)
    {
        moveNo.text=_moveNo.ToString();
    }
}
