using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


 
public class ExitPortal : MonoBehaviour
{
    public Camera cam;
    public GameObject player;
    private bool hasEntered = false;
    public Animator anim;
    new AudioSource audio;
    AudioSource camAudio;

    public string NextSceneName;

    // Start is called before the first frame update
    void Start()
    {

        camAudio = cam.GetComponent<AudioSource>();
        audio = GetComponent<AudioSource>();
        if (!anim)
            anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasEntered)
        {
            Vector3 portalPosition = new Vector3(25.75f, -3.57f,cam.transform.position.z);
            hasEntered = false;
        }
    }

    IEnumerator Wait()
    {
        anim.Play("portal close animation");
        yield return new WaitForSeconds(2.0f);
        Debug.Log($"ExitPort:Wait: NextScene: {NextSceneName} C");
        SceneManager.LoadScene(NextSceneName);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            camAudio.Stop();
            hasEntered = true;
            Destroy(player);
            audio.Play(0);
            StartCoroutine(Wait());
        }
    }
}
