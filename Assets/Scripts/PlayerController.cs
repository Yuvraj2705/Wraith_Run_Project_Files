using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] float yPadding;
    [SerializeField] float speed;

    private float yMax, yMin;
    

    void Start()
    {
        SettingBounderies();
    }

    void Update()
    {
        PlayerMove();
    }

    void SettingBounderies()
    {
        Camera gameCamera = Camera.main;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + yPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - yPadding;
    }

    void PlayerMove()
    {
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        var newYPos = Mathf.Clamp(transform.position.y + deltaY,yMin,yMax);

        transform.position = new Vector2(transform.position.x, newYPos);
    }
}
