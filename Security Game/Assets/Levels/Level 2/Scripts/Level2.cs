using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class Level2 : MonoBehaviour
{
    public InputField inputfield;
    public Text output_text;
    // public string keyword = " ";
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
    private string ipRegex = @"((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\.|$)){4}";

    // public string openingDialog = "Charles: More locked doors. I guess we should have seen that coming. But it looks like this door is constantly receiving a signal to stay locked." +
    //     "\nAda: The signal is going through a router, or access point, in the room. Problem is, there are several other signals going through the access point going to different places. I can't narrow down what IP the signal could be coming from." +
    //     "\nCharles: I don't usually suggest this, but you will have to execute a Man-in-the-Middle attack to find, intercept, and modify the signal that is telling the door to stay locked." +
    //     "\nAda: We should look for another option first. [Name] could get in a lot of trouble for doing that." +
    //     "\nCharles: We don’t have that kind of time. Alan needs us. To do this, you need to find the IP address sending the signal that is keeping the door locked. Then, convince it to send the signal to your computer instead. When you receive the signal, modify it to unlock the door and send it to the access point. The tricky part is lying to both the access point and the door about where signals are coming from." +
    //     "\nAda: Your understanding of this frightens me, but if you think [Name] can do it then I won’t argue. This time." +
    //     "\nCharles: This is called a packet. Packets send information from one computer to the next. Think of packets like letters in the mail. They’re envelopes that carry information. When you use a tool like WireShark to view packets, you can see information about where the packet is going and who is sending it. It’s just like if you were looking at the destination address and the return address on an envelope. Viewing packets over a network like this is called packet sniffing.";

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
        if (Input.GetKeyDown(KeyCode.Return))
        {
            string input = inputfield.text;
            // string shortened;

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
                                Debug.Log("Turn on IPs on Packets");
                                output_text.text += ">>  " + input + "\n";
                                GameObject[] packets = GameObject.FindGameObjectsWithTag("Packet");

                                for (int i = 0; i < packets.Length; i++)
                                {
                                    // packets[i].SetActive(true);
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
                                Debug.Log("Display packet infromation");
                                output_text.text += ">>  " + input + "\n";

                                for (int i = 0; i < packList.Count; i++)
                                {
                                    if (packList[i].Source == ip)
                                    {
                                        packet.GetComponentInChildren<TextMesh>().text = ip + " -> " + packList[i + 1].Source + "\n" + packList[i].Contents;
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
                            Debug.Log("Change IP address");
                            output_text.text += ">>  " + input + "\n";
                            packet.SetActive(false);
                            ip = split[2];
                            ipAddress.text = " TERMINAL    IP: " + ip;
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

                            Debug.Log("Change 'Variable' on Packet to 'Value'");
                            output_text.text += ">>  " + input + "\n";

                            if (info[0].Contains(split[1]))
                            {
                                info[2] = split[3];
                            }

                            for (int i = 0; i < info.Length; i++)
                            {
                                str += info[i];
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
        }
    }

    void chat()
    {
        chat_man.text = "Ada: This is what we are looking at. Several packets are coming into the access point and being sent to various locations. Try using that WireShark tool Charles mentioned." +
            "\n\nAda: Good job. We can see where the packets are going and who is sending them. Now we need the packet going to the door to go to your computer instead. It looks like the signal going from the router to the door is 10.0.6.7 to 10.0.5.2. To do this next part, you will have to use IP Spoofing. This means that you make your IP address look like another IP address. By doing this, other computers will see the spoofed IP address rather than your IP address. It’s kind of like wearing a costume, no one knows it’s really you in there. Also, now that we know what we are looking for, it may be easier to ignore the other packets." +
            "\n\nCharles: Good, you got the packet. Now view the details of the packet and modify it to an unlock signal. Then you will need to spoof your IP to look like the original senders and send it to the door." +
            "\n\nCharles: Great. The door should unlock now.";
    }

    void manual()
    {
        chat_man.text = "Wireshark is a free and open source packet analyzer. It is used to see the information being sent over a network." +
            "\n\nCommon Commands:" +
            "\n-Using '-k' shows where the packet is coming from and where the packet is going." +
            "\n-Using '-w -' displays the information from the packet in the terminal." +
            "\n\nThe nmap command is used to scan IP addresses and can be used to send and recieve packets over a network." +
            "\n\nCommon Commands:" +
            "\n-Using '-s [Source] [Destination]' will send all packets going to the Source IP address to the Destination IP address" +
            "\n\nThe set command is used as a way to set variables in the terminal and in this case can be used to change information in a packet" +
            "\n\nUsage: 'set [Variable] = [Value]'";
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        router.SetActive(true);
    }
}