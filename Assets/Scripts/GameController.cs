using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    public GameObject[] asteroids;
    public GameObject star;
    public Camera mainCamera;
    public StarController starC;
    public PlayerMovement playerMovement;
    public ScoreUI scoreUI;
    public GameObject scorePanel;

    // Start is called before the first frame update
    void Start()
    {
        SpawnAsteroids();
        SpawnStars();
    }

    void Update()
    {
        CheckPlayersDeath();
    }

    void SpawnAsteroids()
    {
        int randoms = Random.Range(5, 8);
        int i = 0;

        while(i < randoms)
        {
            float spawnY = Random.Range(
                mainCamera.ScreenToWorldPoint(new Vector2(0, 0)).y, mainCamera.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
            float spawnX = Random.Range(
                mainCamera.ScreenToWorldPoint(new Vector2(0, 0)).x, mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

            Vector2 spawnPosition = new Vector2(spawnX, spawnY);

            if(CheckDistance(spawnPosition))
                {
                    Instantiate(asteroids[Random.Range(0, asteroids.Length)], spawnPosition, Quaternion.identity); 
                    i++;
                }                   
        }
    }
    void SpawnStars()
    {
        int randoms = Random.Range(5, 8);
        int i = 0;
        playerMovement.starCont = randoms;
        while(i < randoms)
        {
            float spawnY = Random.Range(
                mainCamera.ScreenToWorldPoint(new Vector2(0, 0)).y, mainCamera.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
            float spawnX = Random.Range(
                mainCamera.ScreenToWorldPoint(new Vector2(0, 0)).x, mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

            Vector2 spawnPosition = new Vector2(spawnX, spawnY);

            if(CheckDistance(spawnPosition))
            {
                GameObject starObj = Instantiate(star, spawnPosition, Quaternion.identity);
                starC = starObj.GetComponent<StarController>();
                starC.starNumb = i;

                starC.starText = starObj.GetComponentInChildren<TextMeshPro>();

                int starTextNum = starC.starNumb + 1;
                starC.starText.text = starTextNum.ToString();
                i++;
            }
        }        
    }

    //Check distance between the player and asteroid/star
    public bool CheckDistance(Vector2 spawnP)
    {
        GameObject PlayerP = GameObject.FindGameObjectWithTag("Player");
        float vecDist = Vector2.Distance(spawnP, PlayerP.transform.position);

        //if the distance of the spawn position is out of the player radius
        if(vecDist > 3)
                return true;
        else
            return false;
    }

    void CheckPlayersDeath()
    {
        if(playerMovement.playerLives == 0)
            scorePanel.SetActive(true);
    }

    public void SaveD()
    {     
        scoreUI.CheckPreviousData();
        SaveSystem.SavePlayer(playerMovement);
        scoreUI.SaveName();
    }

    public void RestartScene()
    {
        SceneManager.LoadScene("Test");
        playerMovement.score = 0;
    }    
}
