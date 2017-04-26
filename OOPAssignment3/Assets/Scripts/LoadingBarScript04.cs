using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingBarScript04 : MonoBehaviour {


	AsyncOperation ao;
	public GameObject LoadingScreenBG;
	public Slider ProgressBar;
	public Text LoadingText;
	
	public bool isFakeLoadingBar = false;
	public float fakeIncrement = 0f;
	public float fakeTiming = 0f;
	
	// Use this for initialization
	void Start () {
        Cursor.visible = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void LoadLevel01()
	{
        //if play is clicked these are activated and displayed over the start menu
        LoadingScreenBG.SetActive(true);
        ProgressBar.gameObject.SetActive(true);
        LoadingText.gameObject.SetActive(true);
        LoadingText.text = "LOADING...";

        if(!isFakeLoadingBar)
        {
            //calls real loading bar
            StartCoroutine(LoadLevelWithRealProgress());
        }
        else
        {
            //calls the fake loading bar
            StartCoroutine(LoadLevelWithFakeProgress());
        }

        
	}

    IEnumerator LoadLevelWithRealProgress()
    {
        yield return new WaitForSeconds(1);

        ao = SceneManager.LoadSceneAsync(1);
        ao.allowSceneActivation = false;

        while(!ao.isDone)
        {
            ProgressBar.value = ao.progress;
            //when level is loaded an "f" is pressed the playable game is displayed
            if(ao.progress == 0.9f)
            {
                ProgressBar.value = 1f;
                LoadingText.text = "Press \"F\" to Continue";
                if(Input.GetKeyDown(KeyCode.F))
                {
                    ao.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }

    //to use if we dicide to use fake time because loading is too fast
    IEnumerator LoadLevelWithFakeProgress()
    {
        yield return new WaitForSeconds(1);

        while(ProgressBar.value != 1f)
        {
            ProgressBar.value += fakeIncrement;
            yield return new WaitForSeconds(fakeTiming);
        }

        while(ProgressBar.value == 1f)
        {
            LoadingText.text = "Press \"F\" to Continue";
            if (Input.GetKeyDown(KeyCode.F))
            {
                SceneManager.LoadScene(1); ;
            }
            yield return null;
        }
    }
}
