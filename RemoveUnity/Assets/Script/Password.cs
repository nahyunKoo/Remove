using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Yarn;
using Yarn.Unity;
public class Password : MonoBehaviour
{
    private DialogueRunner Runner;
    public TMP_InputField inputfield_birthday;
    public Button Enter;
    public GameObject BirthdayImage;

    private string password = "0828";
    public void Start()
    {
        Runner = FindObjectOfType<DialogueRunner>();
    }
    public void EnterClick()
    {
        if(inputfield_birthday.text.Equals(password))
        {
            Debug.Log("����");
            GameObject.Find("BirthdayImage").SetActive(false);
            Runner.StartDialogue("PasswordCorrect");
        }

        else
        {
            Debug.Log("����");
            Runner.StartDialogue("InferingPassword");
        }
    }
}