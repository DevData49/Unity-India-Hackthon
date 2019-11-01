using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragnDrop : MonoBehaviour
{
 
    public GameObject cursor;
    [SerializeField]
    private bool selected;
    [SerializeField]
    private bool overlapping;
    [SerializeField]
    private float lastMouseOutTime = 0;
    [SerializeField]
    private float lastOverlapOutTime = 0;
    private void Update() {
        
        if(selected == true && !overlapping){
            Vector2 cursorPos =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cursor.transform.position =  new Vector2(cursorPos.x,cursorPos.y);
            transform.position = new Vector2(Mathf.Round(cursorPos.x), Mathf.Round(cursorPos.y));           
        }

        if(Input.GetMouseButtonUp(0)){
                selected = false;
                transform.position = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
                // Debug.Log(transform.position);
                cursor.SetActive(false);
        }

       if(lastMouseOutTime > 0 && Time.time-lastMouseOutTime > 0.2){
           lastMouseOutTime = 0;
           selected=false;
           overlapping=false;
       }
       if(lastOverlapOutTime > 0 && !selected && Time.time - lastOverlapOutTime > 0.2){
           lastOverlapOutTime=0;
           overlapping = false;
       }
    }
    private void OnMouseOver() {
        lastMouseOutTime = 0;
        if(Input.GetMouseButtonDown(0)){
            selected = true;
            cursor.SetActive(true);
            Vector2 cursorPos =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cursor.transform.position =  new Vector2(Mathf.Round(cursorPos.x), Mathf.Round(cursorPos.y));
        }    
    }
    void OnMouseExit()
    {   
        lastMouseOutTime = Time.time;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("block")){
            overlapping = true;
            lastOverlapOutTime = Time.time;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        overlapping = false;
    }  
}


