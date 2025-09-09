using System;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int Hitpoints = 1;
    public ParticleSystem DestroyEffect;
    private SpriteRenderer sr;

    public static event Action<Brick> OnBrickDestruction;

    private void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        sr.sprite = BricksManager.Instance.Sprite[this.Hitpoints - 1];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Ball ball))
        {
            ApplyCollisionLogic(ball);
        }
    }

    private void ApplyCollisionLogic(Ball ball)
    {
        this.Hitpoints--;

        if (this.Hitpoints <= 0)
        {
            OnBrickDestruction?.Invoke(this);
            SpawnDestroyEffect();
            Destroy(this.gameObject);
        }
        else
        {
            this.sr.sprite = BricksManager.Instance.Sprite[this.Hitpoints - 1];
        }
    }

    private void SpawnDestroyEffect()
    {
        Vector3 brickPos = gameObject.transform.position;
        Vector3 spawnPosition = new Vector3(brickPos.x, brickPos.y, brickPos.z - .2f);
        GameObject effect = Instantiate(DestroyEffect.gameObject, spawnPosition, Quaternion.identity);

        ParticleSystem.MainModule mm = effect.GetComponent<ParticleSystem>().main;
        mm.startColor = this.sr.color;
        Destroy(effect, DestroyEffect.main.startLifetime.constant);
    }
}
