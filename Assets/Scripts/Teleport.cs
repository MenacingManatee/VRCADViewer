using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class Teleport : MonoBehaviour
{
    public AudioSource onTeleport;
    public AudioSource onElevator;
    public AudioSource onElevatorDone;
    public CharacterController player;
    public GameObject headFollowObject;

    private float teleportCD = 0f;
    private Coroutine c;

    void Start()
    {
        headFollowObject = GameObject.Find("Head Follower");
    }

    void Update()
    {
        if (teleportCD > 0)
            teleportCD -= Time.deltaTime;
    }

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
        if (teleportCD > 0)
            return;
        teleportCD = 2f;
        SteamVR_Fade.Start(Color.clear, 0);
        SteamVR_Fade.Start(Color.black, 0.15f);
        StartCoroutine(DoTeleport(pos));
        if (onTeleport)
        {
            onTeleport.Stop();
            onTeleport.Play();
        }
    }

    public void ElevatorToLocation(Transform pos)
    {
        if (c != null)
            return;
        SteamVR_Fade.Start(Color.clear, 0);
        SteamVR_Fade.Start(Color.black, 0.15f);
        c = StartCoroutine(DoTeleport(pos, true, 0.95f));
        if (onElevator)
        {
            if (onTeleport)
                onTeleport.Stop();
            onElevator.Stop();
            onElevator.Play();
        }
    }

    private IEnumerator DoTeleport(Transform pos, bool isElevator = false, float waitSeconds = 0.15f)
    {
        yield return new WaitForSeconds(waitSeconds);
        Vector3 headFollowOffset = Vector3.zero;
        if (player)
            player.enabled = false;
        if (headFollowObject)
            headFollowOffset = headFollowObject.transform.position - transform.position;
        transform.position = pos.position;
        if (headFollowObject)
            headFollowObject.transform.position = transform.position + headFollowOffset;
        if (player)
            player.enabled = true;
        if (isElevator && onElevator && onElevatorDone)
        {
            onElevator.Stop();
            onElevatorDone.Stop();
            onElevatorDone.Play();
        }
        StartCoroutine(FadeIn());
        c = null;
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
