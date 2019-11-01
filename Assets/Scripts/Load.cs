using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    // Start is called before the first frame update
    public int level;

    // Update is called once per frame
    public void LoadLevel(){
        SceneManager.LoadScene(level);
    }
}
