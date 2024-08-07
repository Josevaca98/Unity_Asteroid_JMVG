using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    //Mobile
    private Touch touch;
    private float speed;

    //Mouse
    private float startPosX;
    private float startPosY;
    private bool isBeingClicked = false;

    //Player Life's
    public int playerLives = 3;
    public GameObject[] livesUI;
    new Collider2D collider2D;
    Animator animator;

    //PLayer's Components
    public int score;
    static int newScore;
    private int currentObjective = 0;
    public int starCont;

    public PlayerData pd;

    public string playerName;

    // Start is called before the first frame update
    void Start()
    {
        //Mobile
        speed = 0.01f;
        
        score = newScore; 

        collider2D = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

        animator.SetBool("IsHurt", false);

        playerLives = 3; 
    }

    // Update is called once per frame
    void Update()
    {
        GameOver();

        //Mouse 
        if(isBeingClicked == true)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);
        }

        //Mobile
        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector3(
                    transform.position.x + touch.deltaPosition.x * speed,
                    transform.position.y,
                    transform.position.z + touch.deltaPosition.y * speed);
            }
        }
    }

    private void OnMouseDown()
    {
        //Mouse
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            isBeingClicked = true;
        }
    }

    private void OnMouseUp()
    {
        isBeingClicked = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Asteroid"))
        {
            --playerLives;
            collider2D.enabled = !collider2D.enabled;
            animator.SetBool("IsHurt", true);
            StartCoroutine(WaitToGetHealed());
        }
        else if(collision.collider.CompareTag("Star"))
        {
            StarController currentStart = collision.gameObject.GetComponent<StarController>();
            if(currentObjective == currentStart.starNumb)
            {
                Destroy(collision.gameObject);
                currentObjective++;
                score++;
                if(currentObjective == starCont)
                    {
                        SceneManager.LoadScene("Test");
                        newScore = score;
                    }        
            }
        }
    }

    IEnumerator WaitToGetHealed()
    {
        yield return new WaitForSeconds(1);
        collider2D.enabled = !collider2D.enabled;
        animator.SetBool("IsHurt", false);
    }

    public void GameOver()
    {
        if (playerLives == 2)
        {
            livesUI[0].SetActive(false);
        }
        else if (playerLives == 1)
        {
            livesUI[1].SetActive(false);
        }
        else if(playerLives == 0)
        {
            livesUI[2].SetActive(false);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }
}
