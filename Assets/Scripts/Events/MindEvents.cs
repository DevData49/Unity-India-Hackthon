using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindEvents : MonoBehaviour
{
    // Start is called before the first frame update
    public static MindEvents current;
    private void Awake(){
        current = this;
    }
    public event Action Answered;
    public void onAnswered(){
        if(Answered != null){
            Answered();   
        }
    }
}
