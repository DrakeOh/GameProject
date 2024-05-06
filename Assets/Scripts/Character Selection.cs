using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
   

    // Start is called before the first frame update
    void Start()
    {
    

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void greenscene()
    {
        SceneManager.LoadScene("GreenPlayerScene");
        Time.timeScale = 1f;

    }
    public void normalscene()
    {
        SceneManager.LoadScene("NormalPlayerScene");
        Time.timeScale = 1f;

    }
    public void Charachterscene()
    {
        SceneManager.LoadScene("Charachter");
        Time.timeScale = 1f;

    }
}
