using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro_ScriptChanges : MonoBehaviour
{
    public Button leftButton;
    public Text dialogue;
    public GameObject packet;
    public GameObject router;
    public GameObject packetInfo;
    public GameObject terminal;
    private int page = 0;
    private bool activate = false;
    private Dictionary<int, Action> pageMethods;
    private List<string> openingDialogue = new List<string>() {
        "Charles: This is called a packet. Packets send information from one computer to the next. Think of packets like letters in the mail. They have a sender, receiver, and messages inside.",
        "The packets go from one router to the next until they get to their destination. That is how information travels over the Internet.",
        "Each packet here has a destination and information that the packet is transporting. When you use a tool like WireShark to view packets, you can see this information.",
        "To get the packets on your computer where you can view them you will have to fake, or spoof, your IP address. Your current IP address will be labeled on the terminal.",
        "Using the nmap command you can intercept packets and send them to your computer. You can then view and change the information in the packet using WireShark and set commands."
        };


    // Use this for initialization
    void Start()
    {
        pageMethods = new Dictionary<int, Action>() {
            { 0, ShowPage_0 },
            { 1, ShowPage_1 },
            { 2, ShowPage_2 },
            { 3, ShowPage_3 },
            { 4, ShowPage_4 },
        };
    }

    private void ShowPage_4()
    {
        dialogue.text = openingDialogue[page];
        terminal.GetComponentInChildren<Text>().text = " TERMINAL    IP: 8.8.8.8\n\n>> nmap -s 8.8.8.8 192.168.1.103";
    }

    private void ShowPage_3()
    {
        dialogue.text = openingDialogue[page];
        terminal.GetComponentInChildren<Text>().text = " TERMINAL    IP: 192.168.1.103";
        packetInfo.SetActive(false);
        terminal.SetActive(true);
    }

    private void ShowPage_2()
    {
        dialogue.text = openingDialogue[page];
        packet.transform.position = new Vector3(0, 22, 5);
        packet.transform.rotation = new Quaternion(0, 0, 0, packet.transform.rotation.w);
        terminal.SetActive(false);
        router.SetActive(false);
        packet.SetActive(false);
        packetInfo.SetActive(true);
        activate = true;
    }

    private void ShowPage_1()
    {
        leftButton.interactable = true;
        dialogue.text = openingDialogue[page];
        packet.transform.position = new Vector3(packet.transform.position.x, packet.transform.position.y, 0);
        packetInfo.SetActive(false);
        router.SetActive(true);
        if (!packet.activeSelf && activate)
        {
            packet.SetActive(true);
            activate = false;
        }
    }

    private void ShowPage_0()
    {
        leftButton.interactable = false;
        dialogue.text = openingDialogue[page];
        packet.transform.position = new Vector3(0, 22, 5);
        packet.transform.rotation = new Quaternion(0, 0, 0, packet.transform.rotation.w);
        router.SetActive(false);
        packet.SetActive(true);
    }

    public void ClickNext()
    {
        if (page < (pageMethods.Count - 1))
        {
            page++;
        }
        else
        {
            SceneManager.LoadScene("Level 2");
        }
    }

    public void ClickPrev()
    {
        if (page > 0)
        {
            page--;
        }
    }

    void Update() { pageMethods[page].Invoke(); }
}
