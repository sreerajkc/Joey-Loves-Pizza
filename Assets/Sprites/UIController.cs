using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI moveNo;
    public TextMeshProUGUI LevelNo;
    
    public GameObject Instuction;

    private void Start() 
    {
        moveNo.text=GameManager.instance.maxMoves.ToString();
        SetLevelNo(PlayerPrefs.GetInt("Level"));
    }
    public void SetMoveNo(int _moveNo)
    {
        moveNo.text=_moveNo.ToString();
    }
    public void SceneLoad()
    {
        if(PlayerPrefs.GetInt("Fresher")==1)
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
        }
        else
        {   
            PlayerPrefs.SetInt("Level",1);
            Instuction.SetActive(true);
            StartCoroutine("StartGame");
        }
        
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void SetLevelNo(int lvl)
    {
        LevelNo.text=lvl.ToString();
    }
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(1);

    }
}
