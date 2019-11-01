using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{   
    public static Cursor cursorInstance;
    public Sprite ok;
    public Sprite notok;
    [SerializeField]
    private bool selected;
    [SerializeField]
    private bool overlapping;
    [SerializeField]
    private Block selectedBlock;
    [SerializeField]
    //private float lastOverlapOutTime = 0;
    private SpriteRenderer srenderer;

    // Update is called once per frame
    private void Start() {
        Debug.Log("Hello");
        if(cursorInstance == null){
            cursorInstance = this;
            Debug.Log(cursorInstance);
        }
        
        gameObject.SetActive(false);
        srenderer = GetComponent<SpriteRenderer>(); 
           
    }
    void Update()
    {
        if(selected == true ){
            Vector2 cursorPos =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position =  new Vector2(cursorPos.x,cursorPos.y);
            if(Vector2.Distance(cursorPos,selectedBlock.transform.position) >= Block.gridUnit || overlapping){
                srenderer.sprite = notok;
            } else {
                srenderer.sprite = ok;
            }
            if(!overlapping){
                selectedBlock.setPosition(new Vector3(Mathf.Round(cursorPos.x/Block.gridUnit)*Block.gridUnit, Mathf.Round(cursorPos.y/Block.gridUnit)*Block.gridUnit,0),3);
               // Debug.Log(selectedBlock.value); 
            }   
        }
        if(Input.GetMouseButtonUp(0)){
                //lastOverlapOutTime = 0;
                gameObject.SetActive(false);
                overlapping = false;
                selected = false;
                selectedBlock.changeTag("block");
                selectedBlock = null;
        }  
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("block")){
            overlapping = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        overlapping = false;
        //Debug.Log(other.gameObject.tag+" left trigger. count:"+count);   
    }

    public void setSelected(Block block){
      //  lastOverlapOutTime = 0;
        gameObject.SetActive(true);
        overlapping= false;
        selected = true;
        selectedBlock = block;
        selectedBlock.changeTag("selectedBlock");  
    }
}
