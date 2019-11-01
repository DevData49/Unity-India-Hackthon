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
      
        public Count(int min, int max){
            minimum = min;
            maximum = max;
          
        }
    }
    public int columns = 128;
    public int rows = 128;
    public GameObject wall;
    public GameObject bg;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    void InitialiseList(){
        gridPositions.Clear();
        for(int x = -columns/2 + 4; x < columns/2 -4;x++ ){
            for(int y = -rows/2 + 4; y < rows/2 -4;y++ ){
                gridPositions.Add(new Vector3(x,y,0f));
            }    
        }
    }

    void BoardSetup(){
        boardHolder = new GameObject("board").transform;
        boardHolder.SetParent(GameObject.FindWithTag("gameManager").transform);
        for(int x = -columns/2 ; x < columns/2 ;x++ ){
            for(int y = -rows/2; y < rows/2 ;y++ ){
                // if(x == -columns/2 || x== -columns/2+1 
                //     || x== columns/2-2 || x == columns/2-1
                //     || y == -rows/2 || y == -rows/2 +1
                //     || y == rows/2-2 || y== rows/2-1){
                    if(x == -columns/2  || x == columns/2-1
                    || y == -rows/2 || y == rows/2-1){
                        GameObject instance = Instantiate(wall, new Vector3(x*Block.gridUnit,y*Block.gridUnit,0f), Quaternion.identity) as GameObject;
                        instance.transform.SetParent(boardHolder);
                    }
                    else{
                        GameObject instance = Instantiate(bg, new Vector3(x*Block.gridUnit,y*Block.gridUnit,0f), Quaternion.identity) as GameObject;
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
    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum){
        int ObjectCount = Random.Range(minimum, maximum+1);
        for(int i=0;i<ObjectCount;i++){
            Vector3 randomPosition = RandomPosition();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }
    public void SetupScene(){
        BoardSetup();
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
