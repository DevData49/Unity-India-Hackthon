using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{   
    public static int gridUnit = 2;
    [SerializeField]
    public bool inContact = false;
    [SerializeField]
    // public GameObject cursor;
    public bool isSelected = false;
    public LayerMask blockingLayer;
    public float moveTime = 0.05f;
    public string value;
    public bool moved = true;
    private Rigidbody2D rb2D;
    private BoxCollider2D boxCollider;
    private IEnumerator smoothmov;
    private float inverserMoveTime;

    private void Start() {
        rb2D = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        inverserMoveTime =  1f / moveTime;
    }
    // private void OnMouseOver() {
    //     if(Input.GetMouseButtonDown(0)){
    //         // cursor.GetComponent<Cursor>().setSelected(this);
    //         Vector2 cursorPos =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //         cursor.transform.position =  new Vector2(Mathf.Round(cursorPos.x), Mathf.Round(cursorPos.y));
    //     }    
    // }
    void OnMouseDown()
    {
        if(inContact && GameObject.FindWithTag("Player").GetComponent<Player>().full() && !isSelected){
            Debug.Log("This can be carried");
            GameObject.FindWithTag("Player").GetComponent<Player>().pushBlock(this);
            isSelected =true;
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else if(isSelected){
            GameObject.FindWithTag("Player").GetComponent<Player>().removeBlock(this);
            isSelected =false;
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
     void Update(){
        //  if(moved){
        //      rb2D.MovePosition(new Vector3(Mathf.Round(transform.position.x/gridUnit)*gridUnit, Mathf.Round(transform.position.y/gridUnit)*gridUnit,0));
        //      moved = false;
        //  }
         //rb2D.MovePosition(new Vector3(Mathf.Round(transform.position.x/gridUnit)*gridUnit, Mathf.Round(transform.position.y/gridUnit)*gridUnit,0));
    }

    protected IEnumerator SmoothMovement(Vector3 end){
        float sqrRemainingDistance = (transform.position -end).sqrMagnitude;

        while(sqrRemainingDistance > float.Epsilon){
            Vector3 newPos = Vector3.MoveTowards(rb2D.position, end,inverserMoveTime*Time.deltaTime);
            rb2D.MovePosition(newPos);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }
    }

    public bool setPosition(Vector3 pos,int strength,bool check = false){
        if(strength == 0){
            return false;
        }
        if(Mathf.Abs(transform.position.x - pos.x)<= Block.gridUnit && Mathf.Abs(transform.position.y-pos.y)<= Block.gridUnit && gameObject.activeSelf){
            Vector2 start = transform.position;
            Vector2 end = new Vector2(pos.x, pos.y);
            if(check){
                smoothmov = SmoothMovement(pos);
                StartCoroutine(smoothmov);
                moved = true;
                return true;
            }
            boxCollider.enabled = false;
            RaycastHit2D hit = Physics2D.Linecast(start, end, blockingLayer);
            boxCollider.enabled = true;
            //Debug.Log("here");
            //Debug.Log(hit.transform);
            if(hit.transform == null) {
                smoothmov = SmoothMovement(pos);
                StartCoroutine(smoothmov);
                moved = true;
                return true;
            } else {
                
                Vector3 push= hit.transform.position + (pos - transform.position);
                if(hit.collider.gameObject.tag == "block" && hit.transform.GetComponent<Block>().setPosition(new Vector3(Mathf.Round(push.x), Mathf.Round(push.y)),strength-1)){
                    smoothmov = SmoothMovement(pos);
                    StartCoroutine(smoothmov);
                    moved = true;
                    return true;
                } else {
                    return false;
                }
            }
        }
        return false;
        
        
    }
    public bool lookAhead(Vector2 start, Vector2 end){
        RaycastHit2D hit;
        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);
        boxCollider.enabled = true;
        return (hit.transform == null || hit.transform.gameObject.tag == "Player");
    }
    public void submit(){
        StopCoroutine(smoothmov);
    }
    public void changeTag(string tag){
        gameObject.tag = tag;
    }
    public void updateContact(bool val){
        inContact = val;
    }
}
