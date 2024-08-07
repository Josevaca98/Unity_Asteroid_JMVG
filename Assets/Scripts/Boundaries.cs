using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    public Camera mainCamera;
    private Vector2 screenBoundaries;
    private float objectWidth;
    private float objectHeight;

    // Start is called before the first frame update
    void Start()
    {
        screenBoundaries = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 viewPos = transform.position;

        viewPos.x = Mathf.Clamp(viewPos.x, screenBoundaries.x * -1 + objectWidth, screenBoundaries.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBoundaries.y * -1 + objectHeight, screenBoundaries.y - objectHeight);
        transform.position = viewPos;
    }
}
