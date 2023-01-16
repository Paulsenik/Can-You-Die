using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed;
    public float lifetime;
    private float lifetimeCounter;
    public Transform hitBox;
    public LayerMask destroyedOnLayers;

    [Header("Particles")]
    public GameObject trailParticle;
    public float particleDelay;
    private float particleDelayCount;

    SpriteRenderer sr;

    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
    }

    public void FixedUpdate() {
        if (GameManager.singleton.isGameActive) {
            transform.Translate(Vector3.right.normalized * Time.fixedDeltaTime * speed);

            lifetimeCounter += Time.fixedDeltaTime;
            if (lifetimeCounter >= lifetime)
                die();

            if (Physics2D.OverlapBox(hitBox.position, hitBox.localScale, 0, destroyedOnLayers))
                die();

            if (transform.rotation.eulerAngles.z > 90 && transform.rotation.eulerAngles.z < 270) {
                sr.flipY = true;
            } else {
                sr.flipY = false;
            }

            particleDelayCount += Time.fixedDeltaTime;
            if (particleDelay <= particleDelayCount) {
                particleDelayCount = 0f;
                spawnTrailParticle();
            }

        }
    }

    private void spawnTrailParticle() {
        trailParticle.transform.position = hitBox.transform.position;
        Instantiate(trailParticle);
    }

    public void die() {
        Destroy(gameObject);
    }
}
