using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submission : MonoBehaviour
{   
    // Start is called before the first frame update
    public string validValue;
    [SerializeField]
    private string input;
    private ConsoleManager manager;
    void OnTriggerEnter2D(Collider2D other)
    {
       
        if(other.gameObject.tag == "block" || other.gameObject.tag == "selectedBlock"){
            input = other.gameObject.GetComponent<Block>().value;
            if(manager != null){
                Debug.Log(validValue+" : "+input + " "+ validate() + "::" + manager.validate());    
            } else{
                Debug.Log(validValue+" : "+input + " "+ validate());
            }    
        } 
        //other.gameObject.SetActive(false);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("Submitted Object no "+ ++count + " with tag "+ other.gameObject.tag);
        // if(other.gameObject.tag == "block" || other.gameObject.tag == "selectedBlock"){
        //     input = null;
        // }
        //input = null;
        //other.gameObject.SetActive(false);
    }

    public bool validate(){
        Debug.Log("validvalue "+validValue);
        Debug.Log("input "+input);
        if(validValue != null && input != null){
            return validValue == input;
        } else {
            return false;
        }
    }
    public void setConsoleManager(ConsoleManager cmanager){
        manager = cmanager;
    }
    public void setValidValue(string s){
        validValue = s;
    }
}
