using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bg : MonoBehaviour
{
    private bool swipe;

    private void OnMouseOver() {
        if(Input.GetMouseButtonDown(0)){
            swipe = true;
        }
        if(Input.GetMouseButtonUp(0)){
               swipe = false;
        }    
    }

    void Update(){
         if(swipe == true){
            Vector2 cursorPos =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Cursor.cursorInstance.transform.position =  cursorPos;
         }
         if(Input.GetMouseButtonUp(0)){
               swipe = false;
        }
    }
}
