using UnityEngine;

public class Paddle : MonoBehaviour
{
    public static Paddle Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Ball ball))
        {
            Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
            Vector3 hitPoint = collision.GetContact(0).point;
            Vector3 paddleCenter = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y);

            ballRb.linearVelocity = Vector2.zero;
            float difference = paddleCenter.x - hitPoint.x;

            if (hitPoint.x < paddleCenter.x)
            {
                ballRb.AddForce(new Vector2(-Mathf.Abs(difference * 200), BallsManager.Instance.initialBallSpeed));
            }
            else
            {
                ballRb.AddForce(new Vector2(Mathf.Abs(difference * 200), BallsManager.Instance.initialBallSpeed));
            }
        }
    }
}
