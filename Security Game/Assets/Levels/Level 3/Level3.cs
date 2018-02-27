using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level3  : MonoBehaviour
{
    public GameObject Gabriel;
    public GameObject Morgan;
    public GameObject Pat;
    public InputField inputfield;
    public Text output_text;
    public string keyword = " ";
    public Text chat_man;
    public Button chatbutton;
    public Button manualbutton;
    public string dialog_text = "";//Used for placing text within the chat
    public string manual_text = 
    "scan" +
    "\n- This will begin looking through the" +
    "\n  folders. To setup the scan with MalChecker you would type" +
    "\n  Malchecker --scan" +
    "\n  all:" +
    "\n- This will select everything in the" +
    "\n  space that you specify." +
    "\n- For instance, scan-all will select" +
    "\n  everything";
    public Transform target;
    public float smoothTime = 0.3F;
    //Vector3 velocity = Vector3.zero;
   // Vector3 targetPosition = targetPosition.TransformPoint(new Vector3(0, 5, 10));
    public int limit = 0;

    // Use this for initialization
    void Start()
    {
        chatbutton.onClick.AddListener(() => chat());
        manualbutton.onClick.AddListener(() => manual());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(limit);
        if (limit == 0)
        {
            Gabriel = gameObject.transform.GetChild(4).gameObject;//gameObject refers to the object running the script
            Morgan = gameObject.transform.GetChild(5).gameObject;
            Pat = gameObject.transform.GetChild(3).gameObject;
        }

        string input = inputfield.text;
        string shortened;     

        string[] split = input.Split(' '); //Take the input and split at spaces

        List<string> MalChecker = new List<string>()
        {
            "--scan-all"
        };

        List<string> rm = new List<string>()
        {
            "-r failure"
        };

        List<string> qoperation = new List<string>()
        {
            "drbackup -t Q_FULL"
        };
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("input is " + input);   
            
        output_text.text += (">>  " + input);    //Ask why does this print like 7 or 8 times. Want it to happen once and append onto the end i.e. "+="
        if (split[0] == "MalChecker")
        {           
            shortened = input.Replace("MalChecker ", "");
                if (MalChecker[0].Contains(shortened))
                {                   
                    dialog_text = 
                    "Ada: Good, now you can see the malware \n" +
                    "Charles: Luckily, MalChecker trapped them in the failure folder.\n" + 
                    "They can’t move anymore." + 
                    "\nNow you just need to remove the folder.\n";
                manual_text += "\nrm:" +
                                "\n- This is short for remove" +
                                "\n- Using '-r' means it will delete" +
                                "\n everything in the folder as well." +
                                "\n- Next, type the name of the folder you" +
                                "\n want to remove";
             
                limit += 1;
                Gabriel.GetComponent<MalwareMovement_Gabriel>().enabled = false;
                Morgan.GetComponent<MalwareMovement_Morgan>().enabled = false;
                Pat.GetComponent<MalwareMovement_Pat>().enabled = false;
               Pat.transform.localScale -= new Vector3(0.7F, 0.7F, 0);
                Gabriel.transform.localScale -= new Vector3(0.7F, 0.7F, 0);
                Morgan.transform.localScale -= new Vector3(0.7F, 0.7F, 0);
             //   Gabriel.transform.position = Vector3.SmoothDamp(transform.position, ref velocity, smoothTime);
                }
            Debug.Log(limit);
        }
}
if (limit == 1 && Input.GetKeyDown(KeyCode.Return))
{
    output_text.text = (">>  " + input);
    if (split[0] == "rm")
    {
        shortened = input.Replace("rm ", "");
        if (rm[0].Contains(shortened))
        {
            dialog_text += 
            "\nCharles: Well done! Looks like you lost your files, though." +
            "\nGood thing we made a backup!" +
            "\nI knew copying all of your files without your permission would come in handy! I’ll send it to you now.\n" +
            "\nReceived: backup.xml\n";
            manual_text += "\nqoperation: This is a program" +
            "\nthat sets up the" +
            "\nbackup commands." +
            "\ndrbackup: This is short for" +
            "\ndisaster recovery backup and" +
            "\nspecifies what qoperation" +
            "\nshould do." +
            "\n- Using '-t' will be the type of" +
            "\n backup you want." +
            "\n- For instance -t Q_FULL" +
            "\n will be a full backup.";
            limit += 1;
        }

    }
}

if (limit == 2 && Input.GetKeyDown(KeyCode.Return))
{
    output_text.text = (">>  " + input);
    if (split[0] == "qoperation")
    {
        shortened = input.Replace("qoperation ", "");
        if (qoperation[0].Contains(shortened))
        {
                                       
            dialog_text += 
                "\nCharles: By the way, I also copied the English paper you had saved on here." +
                "\nBut I also just saved you. So you kind of owe me anyway." +
                "\nYou're welcome!";
        }
    }
}
    }

    void chat()
    {
       chat_man.text = dialog_text;
    }

    void manual()
    {
        chat_man.text = manual_text;
    }
}
