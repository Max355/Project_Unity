using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    void Awake() 
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numGameSessions > 1)
        {
            Destroy(gameObject);//when we die it return us to the start
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

   void Start() 
   {
      livesText.text = playerLives.ToString();
      scoreText.text = score.ToString();
   }
//    void OnTriggerEnter2D(Collider2D other)
//     {
//        StartCoroutine(ProcessPlayerDeath());
//     }
    
 
    
    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession(); 
        }
        
        
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }

    void TakeLife()
    {
      playerLives = playerLives - 1;
      int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;//load where we currently are
      SceneManager.LoadScene(currentSceneIndex);
      livesText.text = playerLives.ToString();
      
    }

    void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

   
         
    
}
