using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player singleton;

    public GameObject deathParticle;
    public GameObject stepParticle;

    public Transform hitBox;

    [Header("Attributes")]
    public float normalSpeed;
    public float normalJump;
    //[HideInInspector]
    public bool isInProtectionArea;

    [Header("GrooundCheck")]
    public Transform groundCheck;
    public LayerMask layers;

    [Header("Keys")]
    public KeyCode RightKey = KeyCode.D; public KeyCode LeftKey = KeyCode.A; public KeyCode JumpKey = KeyCode.Space;

    [Header("Sounds")]
    public String deathSound;
    public List<string> stepSounds = new List<string>();
    public float stepDelay;
    private float stepDelayCount;

    private bool hasJumped = false, isGround = false;
    [HideInInspector]
    public Rigidbody2D rb;

    private void Awake() {
        singleton = this;
        rb = GetComponent<Rigidbody2D>();
    }

    public void die() {
        ScreenShakeController.instance.startShake(1f, 0.05f, 0f, 15f);
        GameManager.singleton.playSound(deathSound);
        spawnDeathParticle();
        GameManager.singleton.restartLevel();
        Destroy(gameObject);
    }

    private void OnDestroy() {
        singleton = null;
    }

    private void FixedUpdate() {
        if (GameManager.singleton.isGameActive) {
            if (Input.GetKey(RightKey) && !Input.GetKey(LeftKey)) {
                rb.velocity = new Vector2(normalSpeed, rb.velocity.y);
            } else if (Input.GetKey(LeftKey) && !Input.GetKey(RightKey)) {
                rb.velocity = new Vector2(-normalSpeed, rb.velocity.y);
            } else {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }

            stepDelayCount += Time.fixedDeltaTime;
            if (rb.velocity.x != 0 && isGround && stepDelay <= stepDelayCount) {
                stepDelayCount = 0f;
                GameManager.singleton.playSound(stepSounds[UnityEngine.Random.Range(0, stepSounds.Count - 1)]);
                spawnStepParticle();
            }

            if (Input.GetKey(JumpKey)) {
                if (!hasJumped && isGround) {
                    hasJumped = true;
                    rb.velocity = new Vector2(rb.velocity.x, normalJump);
                }
            } else {
                hasJumped = false;
            }
            isGround = Physics2D.OverlapBox(groundCheck.position, groundCheck.localScale, 0, layers);
        }
    }

    public void OnDrawGizmosSelected() {
        Gizmos.DrawWireCube(groundCheck.position, groundCheck.localScale);
    }

    private void spawnDeathParticle() {
        deathParticle.transform.position = hitBox.transform.position;

        ParticleSystem ps = deathParticle.GetComponent<ParticleSystem>();
        var sh = ps.shape;
        sh.scale = hitBox.transform.localScale;

        Instantiate(deathParticle);
    }

    private void spawnStepParticle() {
        stepParticle.transform.position = new Vector2(hitBox.transform.position.x, hitBox.transform.position.y - hitBox.localScale.y / 2);
        Instantiate(stepParticle);
    }
}
