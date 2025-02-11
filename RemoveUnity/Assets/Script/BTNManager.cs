using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yarn.Unity;

public enum BTNType
{
    Start,
    Option,
    Quit,
    Back,
    Music,
    Sound,
    SkipToPhone,
    SkipToStoryEnd,
    SkipToGamescene,
    MainMenu
}
public class BTNManager : MonoBehaviour
{
    public BTNType type;
    public CanvasGroup mainGroup;
    public CanvasGroup optionGroup;
    public CanvasGroup phoneGroup;
    

    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;


    private GameObject obj;

    private static int optionBlockNumber = 1;
    [YarnCommand("optionBlock")]
    public static void OptionBlock(int number)
    {
        optionBlockNumber = number;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionBlockNumber == 0)
                return;
            if (optionGroup.alpha == 1)
            {
                OptionCanvasOff();
            }
            else
            {
                OptionCanvasOn();
            }
                
        }
    }
    public void Awake()
    {
        //if (BTNType.SkipToPhone == type)
        //{
        //    gameObject.SetActive(GameManager.isScene[0]);
        //}
        //else if (BTNType.SkipToStoryEnd == type)
        //{
        //    gameObject.SetActive(GameManager.isScene[1]);
        //}
        //else if (BTNType.SkipToGamescene == type)
        //{
        //    gameObject.SetActive(GameManager.isScene[2]);
        //}
        //else
        //{
        //    gameObject.SetActive(true);
        //}
        if (BTNType.SkipToPhone == type)
        {
            //gameObject.GetComponent<Button>().interactable = DataManager.GetSawStoryScene();
            gameObject.SetActive(DataManager.GetSawStoryScene());
        }
        else if (BTNType.SkipToStoryEnd == type)
        {
            gameObject.SetActive(DataManager.GetSawStoryScene());
            //gameObject.GetComponent<Button>().interactable = DataManager.GetSawStoryScene();
        }
        else if (BTNType.SkipToGamescene == type)
        {
            gameObject.SetActive(DataManager.GetSawStoryScene());
            //gameObject.GetComponent<Button>().interactable = DataManager.GetSawStoryScene();
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
    public void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        variableStorage = FindObjectOfType<InMemoryVariableStorage>();
    }

    public void OnButtonClick()
    {
        switch (type)
        {
            case BTNType.Start:
                MusicManager.instance.MusicFadeOut();
                Invoke("JumpStoryStart", 1f);
                break;
            case BTNType.Option:
                OptionCanvasOn();
                break;
            case BTNType.Quit:
                //dialogueRunner.StartDialogue("AppExit");
                Quit();
                break;
            case BTNType.Back:
                OptionCanvasOff();
                break;
            case BTNType.SkipToPhone:
                dialogueRunner.Stop();
                SceneManager.LoadScene("Phone");
                break;
            case BTNType.SkipToStoryEnd:
                dialogueRunner.Stop();
                SceneManager.LoadScene("StoryEnd");
                break;
            case BTNType.SkipToGamescene:
                SceneManager.LoadScene("GameScene");
                break;
            case BTNType.MainMenu:
                dialogueRunner.StartDialogue("GameExit");
                break;
        }
    }
    [YarnCommand("quit")]
    public static void Quit()
    {
        Application.Quit();
    }

    public void OptionCanvasOn()
    {
        IsPause();
        CanvasGroupOn(optionGroup);
        optionGroup.alpha = 1;
        CanvasGroupOff(mainGroup);
        CanvasGroupOff(phoneGroup);
        phoneGroup.alpha = 0;
    }
    public void OptionCanvasOff()
    {
        UnPause();
        CanvasGroupOff(optionGroup);
        optionGroup.alpha = 0;
        CanvasGroupOn(mainGroup);
        CanvasGroupOn(phoneGroup);
        phoneGroup.alpha = 1;
    }

    public static void IsPause()
    {
        Time.timeScale = 0;
    }
    public static void UnPause()
    {
        Time.timeScale = 1;
    }

    public void CanvasGroupOn(CanvasGroup cg)
    {
        //cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }
    public void CanvasGroupOff(CanvasGroup cg)
    {
        //cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    [YarnCommand("jumpMainScene")]
    public static void JumpMainScene()
    {
        MusicManager.instance.musicSource.Stop();
        UnPause();
        SceneManager.LoadScene("StartScene");
    }
    [YarnCommand("jumpGameScene")]
    public static void JumpGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
    
    public void JumpStoryStart()
    {
        MusicManager.instance.musicSource.Stop();
        SceneManager.LoadScene("StoryStart");
    }
    [YarnCommand("jumpPhone")]
    public static void JumpPhone()
    {
        MusicManager.instance.musicSource.Stop();
        SceneManager.LoadScene("Phone");
    }
    [YarnCommand("jumpStoryEnd")]
    public static void JumpStoryEnd()
    {
        MusicManager.instance.musicSource.Stop();
        SceneManager.LoadScene("StoryEnd");
    }
    [YarnCommand("jumpEnding1")]
    public static void JumpEnding1()
    {
        SceneManager.LoadScene("Ending1");
    }
    [YarnCommand("jumpEnding2")]
    public static void JumpEnding2()
    {
        SceneManager.LoadScene("Ending2");
    }
    [YarnCommand("jumpEnding3")]
    public static void JumpEnding3()
    {
        SceneManager.LoadScene("Ending3");
    }
    [YarnCommand("jumpEnding4")]
    public static void JumpEnding4()
    {
        SceneManager.LoadScene("Ending4");
    }
    [YarnCommand("jumpEnding5")]
    public static void JumpEnding5()
    {
        SceneManager.LoadScene("Ending5");
    }

    [YarnCommand("jumpScene")]
    public static void JumpScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    [YarnCommand("fadeInStarting")]
    public static void FadeInStarting()
    {
        GameObject.Find("FadeCanvas").transform.Find("Starting").gameObject.SetActive(true);
    }

    [YarnCommand("fadeOutStarting")]
    public static void FadeOutStarting()
    {
        GameObject.Find("FadeCanvas").transform.Find("Starting").gameObject.SetActive(false);
    }

    [YarnCommand("fadeInHospital")]
    public static void FadeInHospital()
    {
        GameObject.Find("FadeCanvas").transform.Find("Hospital").gameObject.SetActive(true);
    }

    [YarnCommand("fadeOutHospital")]
    public static void FadeOutHospital()
    {
        GameObject.Find("FadeCanvas").transform.Find("Hospital").gameObject.SetActive(false);
    }

    [YarnCommand("checkingTab")]
    public static void CheckingTab()
    {

    }
}
