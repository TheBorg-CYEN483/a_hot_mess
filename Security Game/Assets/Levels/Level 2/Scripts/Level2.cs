using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level2 : MonoBehaviour
{
    public InputField inputfield;
    public Text output_text;
    public Text chat_man;
    public Button chatbutton;
    public Button manualbutton;
    public Scrollbar scroller;
    public GameObject packet;
    public GameObject router;
    public Text ipAddress;
    private List<Packet> packList = new List<Packet>();
    private string ip = "192.168.1.103";
    private int linecounter = 0;
    private string chatText = "Ada: This is what we are looking at. Several packets are coming into the router, but we can't see where the packets are going. Try using that WireShark tool Charles mentioned.";
    private string ipRegex = @"((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\.|$)){4}";
    private bool isChat = true;
    private string manText = "Wireshark is a free and open source packet analyzer. It is used to see the information being sent over a network." +
            "\n\nCommon Commands:" +
            "\n-Using '-k' shows where the packet is going." +
            "\n-Using '-w -' displays the information from the packet in the terminal." +
            "\n\nThe nmap command is used to scan IP addresses and can be used to send and recieve packets over a network." +
            "\n\nCommon Commands:" +
            "\n-Using '-s [Source] [Destination]' will send all packets going to the Source IP address to the Destination IP address" +
            "\n\nThe set command is used as a way to set variables in the terminal and in this case can be used to change information in a packet" +
            "\n\nUsage: 'set [Variable] = [Value]'" +
            "\n\nNote: You can use your original IP address '192.168.1.103' as the source in the nmap command to send the packet to the IP you are spoofing.";

    void Start()
    {
        chatbutton.onClick.AddListener(() => chat());
        manualbutton.onClick.AddListener(() => manual());
        Packet pack = new Packet();
        packList = pack.Packets();
        Destroy(GameObject.FindGameObjectWithTag("LevelScreen"), 3);
        StartCoroutine(Wait());
    }

    void Update()
    {
        if (isChat)
        {
            chat_man.text = chatText;
        }
        else
        {
            chat_man.text = manText;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            string input = inputfield.text;
            List<string> tools = new List<string>() { "wireshark", "nmap", "set" };
            List<string> wireshark = new List<string>() { "-w", "-k" };

            string[] split = input.Split(' ');

            if (!tools.Contains(split[0]))
            {
                output_text.text += ">>  " + "Command not recognized. Use the manual." + "\n";
            }
            else
            {
                if (split[0] == "wireshark")
                {
                    if (split.Length == 1)
                    {
                        output_text.text += ">>  " + "Command not recognized. Use the wireshark options." + "\n";
                    }
                    else if (!wireshark.Contains(split[1]))
                    {
                        output_text.text += ">>  " + "Command not recognized. Use the wireshark options." + "\n";
                    }
                    else
                    {
                        if (split[1] == "-k")
                        {
                            if (split.Length >= 3)
                            {
                                output_text.text += ">>  " + "Command not recognized. '-k' does not use arguments." + "\n";
                            }
                            else
                            {
                                output_text.text += ">>  " + input + "\n";
                                isChat = true;
                                chatText += "\n\nAda: Good job. We can see where the packets are going. Now we need the packet going to the door to go to your computer instead. Try using the nmap tool where your terminal IP is the destination and the IP on the packet is the source. Also use wireshark again to view the packet.";
                                GameObject[] packets = GameObject.FindGameObjectsWithTag("Packet");

                                for (int i = 0; i < packets.Length; i++)
                                {
                                    packets[i].GetComponentInChildren<TextMesh>().text = packList[i].Source;
                                }
                                packet.SetActive(false);
                            }
                        }
                        else if (split[1] == "-w")
                        {
                            if (split.Length >= 4 || split.Length < 3)
                            {
                                output_text.text += ">>  " + "Command not recognized. '-w' requires 1 argument." + "\n";
                            }
                            else if (split[2] != "-")
                            {
                                output_text.text += ">>  " + "Command not recognized. '-w' has argument '-'." + "\n";
                            }
                            else
                            {
                                output_text.text += ">>  " + input + "\n";

                                for (int i = 0; i < packList.Count; i++)
                                {
                                    if (packList[i].Source == ip)
                                    {
                                        packet.GetComponentInChildren<TextMesh>().text = ip + "\n" + packList[i].Contents;
                                        if (packList[i].Contents == "COMMAND = LOCK")
                                        {
                                            isChat = true;
                                            chatText += "\n\nCharles: Good, you got the packet. Now, view the details of the packet and modify it to an unlock signal. Try using the set command.";
                                        }
                                        break;
                                    }
                                }

                                packet.SetActive(true);
                            }
                        }
                    }
                }
                else if (split[0] == "nmap")
                {
                    if (split.Length == 1)
                    {
                        output_text.text += ">>  " + "Command not recognized. Use the namp options." + "\n";
                    }
                    else if (split[1] != "-s")
                    {
                        output_text.text += ">>  " + "Command not recognized. Use the nmap options." + "\n";
                    }
                    else
                    {
                        if (split.Length >= 5 || split.Length < 4)
                        {
                            output_text.text += ">>  " + "Command not recognized. '-s' takes 2 arguments." + "\n";
                        }
                        else if (!Regex.IsMatch(split[2], ipRegex))
                        {
                            output_text.text += ">>  " + "Source IP is not valid." + "\n";
                        }
                        else if (!Regex.IsMatch(split[3], ipRegex))
                        {
                            output_text.text += ">>  " + "Destination IP is not valid." + "\n";
                        }
                        else
                        {
                            output_text.text += ">>  " + input + "\n";
                            packet.SetActive(false);
                            ip = split[2];
                            ipAddress.text = " TERMINAL    Spoofed IP: " + ip;

                            if (packet.GetComponentInChildren<TextMesh>().text.Contains("COMMAND=UNLOCK"))
                            {
                                SceneManager.LoadScene("Level 2_Badge");
                            }
                        }
                    }
                }
                else if (split[0] == "set")
                {
                    if (split.Length == 1)
                    {
                        output_text.text += ">>  " + "Command not recognized. Use the set options." + "\n";
                    }
                    else
                    {
                        if (split.Length >= 5 || split.Length < 4)
                        {
                            output_text.text += ">>  " + "Command not recognized. 'set' takes 3 arguments." + "\n";
                        }
                        else if (split[1] == "=")
                        {
                            output_text.text += ">>  " + "A '[Variable]' is required." + "\n";
                        }
                        else if (split[2] != "=")
                        {
                            output_text.text += ">>  " + "An '=' operator is required." + "\n";
                        }
                        else if (split[3] == "=")
                        {
                            output_text.text += ">>  " + "A '[Value]' is required." + "\n";
                        }
                        else
                        {
                            string[] info = packet.GetComponentInChildren<TextMesh>().text.Split(' ');
                            string str = "";
                            output_text.text += ">>  " + input + "\n";

                            if (info[0].Contains(split[1]))
                            {
                                info[2] = split[3];
                            }

                            for (int i = 0; i < info.Length; i++)
                            {
                                str += info[i];
                            }

                            if (str.Contains("COMMAND=UNLOCK"))
                            {
                                isChat = true;
                                chatText += "\n\nCharles: Great. The door should unlock after you send that packet.";
                            }

                            packet.GetComponentInChildren<TextMesh>().text = str;
                        }
                    }
                }
            }

            linecounter += 1;
            if (linecounter > 4)
            {
                scroller.value -= (float).06;
            }

            inputfield.text = "";
        }

        inputfield.ActivateInputField();
    }

    void chat()
    {
        isChat = true;
        inputfield.ActivateInputField();
    }

    void manual()
    {
        isChat = false;
        inputfield.ActivateInputField();
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        router.SetActive(true);
    }
}