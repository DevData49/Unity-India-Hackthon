using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public int nextlevel = -1;
    public AudioSource grabAudio;
    public static int gridUnit = 2;
    public GameObject guide;
    public LayerMask blockingLayer;
    public float moveTime = 0.05f;
    public int capacity = 3;
    public List<Block> selectedBlocks;
    public bool moved = true;
    private Rigidbody2D rb2D;
    private BoxCollider2D boxCollider;
    private IEnumerator smoothmov;
    private float inverserMoveTime;
    private bool moving = false;
    public bool paused= false;
    

    private void Start() {
        if(MindEvents.current){
            MindEvents.current.Answered += levelUp;
        }        
        selectedBlocks = new List<Block>();
        rb2D = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        inverserMoveTime =  1f / moveTime;
    }
    private void OnMouseOver() {
        if(Input.GetMouseButtonDown(0) && !paused){
            guide.GetComponent<Guide>().setSelected(this);
            Vector2 cursorPos =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            guide.transform.position =  new Vector2(Mathf.Round(cursorPos.x/gridUnit)*gridUnit, Mathf.Round(cursorPos.y/gridUnit)/gridUnit);
        }    
    }
     void Update(){
         if(moved){
             rb2D.MovePosition(new Vector3(Mathf.Round(transform.position.x/gridUnit)*gridUnit, Mathf.Round(transform.position.y/gridUnit)*gridUnit,0));
             moved = false;
         }
    
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

    // private void moveChild(Vector3 pos, int strength){
    //     Debug.Log("Fired");
    //     if(selectedBlocks.Count != 0){
    //        foreach (Block block in selectedBlocks){
    //            block.setPosition(block.gameObject.transform.position+pos,strength, true);
    //        } 
    //     }
    // }

    public bool setPosition(Vector3 pos,int strength){
        Debug.Log("Fired");
        if(strength == 0){
            return false;
        }

        Vector2 start = transform.position;
        Vector2 end = new Vector2(pos.x, pos.y);
        Vector3 diff = end - start;
        if(selectedBlocks.Count != 0){
            foreach (Block block in selectedBlocks)
            {
                if(!(block.lookAhead(block.gameObject.transform.position,block.gameObject.transform.position + diff))){
                    return false;
                }
            }
        }
            boxCollider.enabled = false;
            RaycastHit2D hit = Physics2D.Linecast(start, end, blockingLayer);
            boxCollider.enabled = true;
            if(hit.transform == null) {
                smoothmov = SmoothMovement(pos);
                StartCoroutine(smoothmov);
                //moveChild(new Vector3(diff.x,diff.y,0), strength-1);
                moved = true;
                return true;
            } else {
                Vector3 push= hit.transform.position + (pos - transform.position);
                if(hit.collider.gameObject.tag == "block" && hit.transform.GetComponent<Block>().setPosition(new Vector3(Mathf.Round(push.x), Mathf.Round(push.y)),strength-1)){
                    smoothmov = SmoothMovement(pos);
                    StartCoroutine(smoothmov);
                // moveChild(diff, strength-1);
                    moved = true;
                    return true;
                } else {
                    return false;
                }
            }
    }
    public void submit(){
        StopCoroutine(smoothmov);
    }
    public void changeTag(string tag){
        gameObject.tag = tag;
    }
    void OnTriggerEnter2D(Collider2D other)
    {        
        if(other.gameObject.CompareTag("block") && !(
            (Mathf.Abs(other.transform.position.x - transform.position.x) == Player.gridUnit)
             && (Mathf.Abs(other.transform.position.y - transform.position.y) == Player.gridUnit)
        ) )
        {
            other.gameObject.GetComponent<Block>().updateContact(true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("block")){
            other.gameObject.GetComponent<Block>().updateContact(false);
        }   
    }
    public bool full(){
        return (selectedBlocks.Count != capacity);
    }
    public void pushBlock(Block block){
      
        if(selectedBlocks.Count < capacity){
            grabAudio.Play();
            block.isSelected = true;
            //block.gameObject.tag="selectedBlock";
            selectedBlocks.Add(block);
            block.gameObject.transform.SetParent(transform);
        }
    }
    public void removeBlock(Block block){
        
        for(int i=0;i<selectedBlocks.Count;i++){
            if(selectedBlocks[i].gameObject.GetInstanceID() == block.gameObject.GetInstanceID()){
                selectedBlocks.RemoveAt(i);
                grabAudio.Play();
                //block.gameObject.tag = "block";
                block.gameObject.transform.parent=null;
                block.isSelected = false;
                return;
            }
        }
    }
    
    void OnDestroy()
    {
        if(MindEvents.current){
            MindEvents.current.Answered -= levelUp;
        }
     
    }
    public void pause(){
        paused = true; 
    }
    public void levelUp(){
        Debug.Log("Game Complete");
        StartCoroutine(changeScene());
    }
    IEnumerator changeScene(){
        yield return new WaitForSeconds(2);
        if(nextlevel != -1){
            SceneManager.LoadScene(nextlevel);
        }
    }
}
