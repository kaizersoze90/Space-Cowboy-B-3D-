using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float reloadSceneDelay = 1f;
    [SerializeField] ParticleSystem explosionEffect;

    PlayerControl playerControl;

    void Awake()
    {
        playerControl = GetComponent<PlayerControl>();
    }

    void OnTriggerEnter(Collider other)
    {
        StartCrashSequence();
    }

    void StartCrashSequence()
    {
        if (!explosionEffect.isPlaying)
        {
            explosionEffect.Play();
        }
        GetComponent<MeshRenderer>().enabled = false;
        playerControl.isAlive = false;
        StartCoroutine(ReloadScene());
    }

    IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(reloadSceneDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
