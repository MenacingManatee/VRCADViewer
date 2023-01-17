using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class Teleport : MonoBehaviour
{
    public AudioSource onTeleport;
    public CharacterController player;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void TeleportToScene(string sceneName)
    {
        SteamVR_Fade.Start(Color.clear, 0);
        SteamVR_Fade.Start(Color.black, 0.15f);
        if (onTeleport)
            onTeleport.Play();
        StartCoroutine(OpenScene(sceneName));
    }

    public void TeleportToLocation(Transform pos)
    {
        SteamVR_Fade.Start(Color.clear, 0);
        SteamVR_Fade.Start(Color.black, 0.15f);
        StartCoroutine(DoTeleport(pos));
        if (onTeleport)
            onTeleport.Play();
    }

    private IEnumerator DoTeleport(Transform pos)
    {
        yield return new WaitForSeconds(0.15f);
        player.enabled = false;
        transform.position = pos.position;
        player.enabled = true;
        StartCoroutine(FadeIn());
    }

    private IEnumerator OpenScene(string sceneName)
    {
        yield return new WaitForSeconds(0.15f);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(0.15f);
        SteamVR_Fade.Start(Color.clear, 0.3f);
    }
}
