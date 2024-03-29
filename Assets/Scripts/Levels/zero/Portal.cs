﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    // Start is called before the first frame update
   /// <summary>
   /// Sent when another object enters a trigger collider attached to this
   /// object (2D physics only).
   /// </summary>
   /// <param name="other">The other Collider2D involved in this collision.</param>
   void OnTriggerEnter2D(Collider2D other)
   {
       if(other.gameObject.tag == "Player"){
           other.gameObject.GetComponent<Player>().pause();
           other.gameObject.GetComponent<Player>().setPosition(transform.position,3);
           StartCoroutine(nextLevel());
       }
           
   }

   IEnumerator nextLevel(){
       yield return new WaitForSeconds(1);
       GetComponent<Load>().LoadLevel();
   }
}
