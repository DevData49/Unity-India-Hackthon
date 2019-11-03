using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;
public class BoardManager : MonoBehaviour
{
    [SerializeField]
    public class Count{
        public int minimum;
        public int maximum;
    }
    public int columns = 128;
    public int rows = 128;
    public GameObject wall;
    public GameObject bg;

    public GameObject[] requiredOptions;
    
    public GameObject[] Options;
    public Count optionCount;
    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();
    
    void InitialiseList(){
        gridPositions.Clear();
        // Debug.Log(GetComponent<ConsoleManager>().consolePos);
        int xLimit = (int)GetComponent<ConsoleManager>().consolePos.x;
        int yLimit = (int)GetComponent<ConsoleManager>().consolePos.y;
        int consoleLen = GetComponent<ConsoleManager>().rules.Length;
        bool horizontal = GetComponent<ConsoleManager>().horizontal;

        for(int x = -columns/2 + 1; x < columns/2 -1;x++ ){
            for(int y = -rows/2 + 1; y < rows/2 -1;y++ ){     
                if(x==0 && y==0){
                    continue;
                }
                if((horizontal && y*Player.gridUnit == yLimit*Player.gridUnit && x*Player.gridUnit >= xLimit*Player.gridUnit && x*Player.gridUnit<= (xLimit*Player.gridUnit + (consoleLen-1)*Player.gridUnit))){
                      Debug.Log(x*Player.gridUnit+" "+y*Player.gridUnit+" 0");
                 } else {
                      gridPositions.Add(new Vector3(x*Player.gridUnit, y*Player.gridUnit,0f));
                 }
               
            }    
        }
    }

    void BoardSetup(){
        boardHolder = new GameObject("board").transform;
        boardHolder.SetParent(GameObject.FindWithTag("gameManager").transform);
        for(int x = -columns/2 ; x < columns/2 ;x++ ){
            for(int y = -rows/2; y < rows/2 ;y++ ){
                    if(x == -columns/2  || x == columns/2-1
                    || y == -rows/2 || y == rows/2-1){
                        GameObject instance = Instantiate(wall, new Vector3(x*Player.gridUnit,y*Player.gridUnit,0f), Quaternion.identity) as GameObject;
                        instance.transform.SetParent(boardHolder);
                    }
                    else{
                        GameObject instance = Instantiate(bg, new Vector3(x*Player.gridUnit,y*Player.gridUnit,0f), Quaternion.identity) as GameObject;
                        instance.transform.SetParent(boardHolder);
                    }  
            }    
        }
    }
    Vector3 RandomPosition(){
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }
    void LayoutObjectAtRandom(GameObject[] tileArray){
        int ObjectCount = Random.Range(0, tileArray.Length-1);
        for(int i=0;i<ObjectCount;i++){
            Vector3 randomPosition = RandomPosition();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }
    public void SetupScene(){
        InitialiseList();
        BoardSetup();
        for(int i = 0; i< requiredOptions.Length ;i++){
            Instantiate(requiredOptions[i], RandomPosition(), Quaternion.identity);
        }
        LayoutObjectAtRandom(Options);
    }
   
}
