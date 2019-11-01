using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour
{
    // Start is called before the first frame update
    public static Guide instance;
    private bool selected;
    [SerializeField]
    private bool overlapping;
    [SerializeField]
    private Player player;
    [SerializeField]
    //private float lastOverlapOutTime = 0;
    

    // Update is called once per frame
    private void Start() {
        
        if(instance == null){
            instance = this;
        }
        gameObject.SetActive(false);  
    }
    void Update()
    {
        if(selected == true ){
           Vector2 pos =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position =  new Vector2(pos.x,pos.y);
            player.setPosition(new Vector3(Mathf.Round(transform.position.x/Player.gridUnit)*Player.gridUnit, Mathf.Round(transform.position.y/Player.gridUnit)*Player.gridUnit,0),3); 
            // if(!overlapping){
            //     selectedBlock.setPosition(new Vector3(Mathf.Round(cursorPos.x/Block.gridUnit)*Block.gridUnit, Mathf.Round(cursorPos.y/Block.gridUnit)*Block.gridUnit,0),3); 
            // }   
        }
        if(Input.GetMouseButtonUp(0)){
                //lastOverlapOutTime = 0;
                gameObject.SetActive(false);
                // overlapping = false;
                selected = false;
                //selectedBlock.changeTag("block");
                player = null;
        }  
    }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if(other.gameObject.CompareTag("block")){
    //         overlapping = true;
    //     }
    // }
    // void OnTriggerExit2D(Collider2D other)
    // {
    //     overlapping = false;
    //     //Debug.Log(other.gameObject.tag+" left trigger. count:"+count);   
    // }

    public void setSelected(Player obj){
      //  lastOverlapOutTime = 0;
        gameObject.SetActive(true);
        //overlapping= false;
        selected = true;
        player = obj;
        // selectedBlock.changeTag("selectedBlock");  
    }
}
