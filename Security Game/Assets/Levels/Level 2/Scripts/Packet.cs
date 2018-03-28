using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Packet
{
    private string source;
    private string contents;
    private List<string> ipList = new List<string>() { "65.15.4.3", "102.3.75.1", "15.5.8.9", "10.0.0.5", "192.168.7.8", "10.0.1.5" };
    private List<string> contentList = new List<string>() { "COMMAND = LOCK", "Search: Encrypt text", "10101001101010001010101010110", "www.google.com", "ftue ue qzodkbfqp", "tuzf: geq efqsmzasdmbtk" };
    private List<Packet> packetList = new List<Packet>();

    public string Source { get { return source; } }
    public string Contents { get { return contents; } }
    public List<Packet> PacketList { get { return packetList; } }

    public Packet()
    {
        this.source = ipList[0];
        this.contents = contentList[0];
    }

    public Packet(string src, string cont)
    {
        this.source = src;
        this.contents = cont;
    }

    public List<Packet> Packets()
    {
        List<Packet> list = new List<Packet>();
        Shuffle(ipList);
        Shuffle(contentList);

        for (int i = 0; i < ipList.Count; i++)
        {
            Packet pack = new Packet(ipList[i], contentList[i]);
            list.Add(pack);
            packetList.Add(pack);
        }

        return list;
    }

    void Shuffle(List<string> arr)
    {
        for (int i = 0; i < arr.Count; i++)
        {
            string temp = arr[i];
            int rand = Random.Range(i, arr.Count);
            arr[i] = arr[rand];
            arr[rand] = temp;
        }
    }
}
