using UnityEngine;

public class Paddle : MonoBehaviour
{
    private Camera mainCamera;
    private float paddleInitialY;
    private float defaultPaddleWidthInPixels = 200;
    private SpriteRenderer sr;

    private void Start()
    {
        mainCamera = Camera.main;
        paddleInitialY = this.transform.position.y;
        sr = this.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        PaddleMovement();
    }

    private void PaddleMovement()
    {
        float paddleShift = (defaultPaddleWidthInPixels - (defaultPaddleWidthInPixels / 2) * sr.size.x) / 2;
        float leftClamp = 135 - paddleShift;
        float rightClamp = 410 + paddleShift;
        float mousePositionPixels = Mathf.Clamp(Input.mousePosition.x, leftClamp, rightClamp);
        float mousePositionWorldX = mainCamera.ScreenToWorldPoint(new Vector3(mousePositionPixels, 0, 0)).x;
        this.transform.position = new Vector3(mousePositionWorldX, paddleInitialY, 0);
    }
}
