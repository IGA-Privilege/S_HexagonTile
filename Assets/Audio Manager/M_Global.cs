using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class M_Global : MonoBehaviour
{
    private static M_Global instance;
    public static M_Global Instance { get { return instance; } }

    public SO_AudioRepo repo_Audio;

    public AnimationClip[] animClips;
    public GameObject transitionBody;
    private float animTime;
    private bool isBlooming = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        //string[] world1BgAudio = new string[1] { "Dark World" };
        //M_Audio.PlayLoopAudio(world1BgAudio);
    }

    public void EnterSwitchScene(int targetScene)
    {
        StartCoroutine(LoadScene(targetScene));
    }

    IEnumerator LoadScene(int targetScene)
    {
        int random = Random.Range(0, 3);
        animTime = 0;
    
        while (animTime < animClips[random].length/2)
        {
            Debug.Log("dsaadsasddsa");
            animTime += Time.deltaTime;
            animClips[random].SampleAnimation(transitionBody, animTime);
            yield return null;
        }
        SceneManager.LoadScene(targetScene);
        yield return new WaitForSeconds(0.2f);
        while (animTime < animClips[random].length)
        {
            animTime += Time.deltaTime;
            animClips[random].SampleAnimation(transitionBody, animTime);
            yield return null;
        }


        yield return new WaitForSeconds(0.2f);
        Debug.Log("Finished");

        //for (int i = 0; i < clip_Leaves.; i++)
        //{

        //}
        if (FindObjectOfType<M_Tile>() != null) FindObjectOfType<M_Tile>().TriggerMapGen();
    }

    
}
