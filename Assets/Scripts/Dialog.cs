using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialog : MonoBehaviour
{
    public int nextScene = -1;
    // Start is called before the first frame update
    public TextMeshProUGUI textDisplay;
    [System.Serializable]
    public class Dialogue{
       public string person;
        public string text;
        public string nextAction;
    }
    public Dialogue[] sentences;
    private int index;
    public float typingSpeed; 

    public GameObject nextButton;
    IEnumerator type(){
        if(sentences[index].person != null){
            textDisplay.text += sentences[index].person;
        }
        foreach (char letter in sentences[index].text.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(0.02f);   
        }
    }

    public void NextSentence(){
        nextButton.SetActive(false);
        if(index < sentences.Length - 1 ){
            index++;
           if(sentences[index].nextAction != null){
               nextButton.GetComponentInChildren<TextMeshProUGUI>().text = sentences[index].nextAction;
           } 
            textDisplay.text = "";
            StartCoroutine(type());
        } else{
            textDisplay.text = "";
            if(nextScene != -1){
                SceneManager.LoadScene(nextScene);
            }
            
        }
    }

    private void Start() {
        StartCoroutine(type());
    }

    void Update()
    {
        if(textDisplay.text == sentences[index].text){
            nextButton.SetActive(true); 
        }
    }
}
