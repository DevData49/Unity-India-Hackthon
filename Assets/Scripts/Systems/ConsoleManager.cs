using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleManager : MonoBehaviour
{
    public AudioSource successAudio;
    protected Transform parentConsole;
    protected List<Submission> consoleList = new List<Submission>();
    public Vector2 consolePos;
    public GameObject console; 
    public bool horizontal = true;
    public Console[] rules;
    [System.Serializable]
    public class Console{
        public string validValue;
        public Sprite img;
    }

    protected virtual void Start()
    {
        
        if(rules.Length != 0) {        
        parentConsole = new GameObject("pconsole").transform;
        for( int i= 0; i < rules.Length ; i++ ){
            GameObject instance = Instantiate(console, new Vector3(consolePos.x*Player.gridUnit+i*Player.gridUnit,consolePos.y*Player.gridUnit,0f),Quaternion.identity) as GameObject;
            consoleList.Add(instance.GetComponent<Submission>());
            instance.transform.SetParent(parentConsole);
            instance.GetComponent<Submission>().setValidValue(rules[i].validValue);
            instance.GetComponent<Submission>().setConsoleManager(this);
            if(rules[i].img){
                
            }
        }
        }
    }

    public bool validate(){
        for( int i =0; i<consoleList.Count;i++){
            if(consoleList[i].validate() == false){
                return false;
            }
        }
        successAudio.Play();
        MindEvents.current.onAnswered();
        return true;
    }
    
}
