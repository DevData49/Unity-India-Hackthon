using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMangager : MonoBehaviour
{   
    public BoardManager boardScript;    
    void Awake()
    {
        boardScript = GetComponent<BoardManager>();
        InitGame();
    }
    void InitGame(){
        boardScript.SetupScene();
    }
}
