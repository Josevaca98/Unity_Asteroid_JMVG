using UnityEngine;
using TMPro;

public class StarController : MonoBehaviour
{
    //Physics variables
    public float speed;
    Vector2 direction;
    Rigidbody2D rb2b;

    //Object variables
    public int starNumb = 0;
    public TextMeshPro starText;
       

    // Start is called before the first frame update
    void Start()
    {
        rb2b = GetComponent<Rigidbody2D>();
        direction = Vector2.one.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        rb2b.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Check Collision 
        string tag = collision.gameObject.tag;
        switch (tag)
        {
            case "Wall":
                direction.y = -direction.y;
                break;
            case "WallLeftRight":
                direction.x = -direction.x;
                break;
            case "Star":
                direction.x = -direction.x;
                direction.y = -direction.y;
                break;
            case "Asteroid":
                direction.x = -direction.x;
                direction.y = -direction.y;
                break;
            case "Player":
                direction.x = -direction.x;
                direction.y = -direction.y;
                break;
        }
    }
}
