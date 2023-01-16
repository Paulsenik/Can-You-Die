using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Destroyable : MonoBehaviour {

    [Header("Important")]
    public int deathsNeeded = int.MaxValue;
    public Text deathsNeedexIndicator;

    [Header("Particle")]
    public bool spawnParticles = true;
    public GameObject particle;
    public Transform shape;

    [Header("Optional")]
    public float destroyDelay = 0f;
    public UnityEvent additionalDeathEvent;

    private void Start() {
        if (deathsNeedexIndicator != null) {
            if (deathsNeeded > 100) {
                deathsNeedexIndicator.enabled = false;
            }
            int deathsleft = deathsNeeded - GameManager.singleton.deaths;
            deathsNeedexIndicator.text = "" + (deathsleft <= 0 ? 0 : deathsleft);
        }

        if (deathsNeeded <= GameManager.singleton.deaths) {
            Invoke("destroy", destroyDelay);
        }
    }

    public void destroy() {
        Debug.Log("Destroying go");
        if (spawnParticles && particle != null)
            spawnParticle();
        additionalDeathEvent.Invoke();
        Destroy(gameObject);
    }

    private void spawnParticle() {
        particle.transform.position = shape.transform.position;

        ParticleSystem ps = particle.GetComponent<ParticleSystem>();
        var sh = ps.shape;
        sh.scale = shape.transform.localScale;

        Instantiate(particle);
    }

}
