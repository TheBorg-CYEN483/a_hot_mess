using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level3  : MonoBehaviour
{
    
    public GameObject Gabriel;
    public GameObject Morgan;
    public GameObject Pat;

    public GameObject Folder_to_delete;

    public GameObject FolderTab_to_delete;
    public GameObject Damaged_Filesystem;
    public InputField inputfield;
    public Text output_text;
    public string keyword = " ";
    public Text chat_man;
    public Button chatbutton;
    public Button manualbutton;
    public string dialog_text = "";//Used for placing text within the chat
    public string manual_text;
    
    public float smoothTime = 0.3F;
    public float velocity = 0.0F;
    Vector3 targetPosition = Vector3.zero;

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
        inputfield.ActivateInputField();  
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
       
        if (limit == 0)
        {
            Gabriel = gameObject.transform.Find("Malware_Gabriel").gameObject;//gameObject refers to the object running the script
            Morgan = gameObject.transform.Find("Malware_Morgan").gameObject;
            Pat = gameObject.transform.Find("Malware_Pat").gameObject;
            Folder_to_delete = gameObject.transform.Find("FileSystem").Find("FolderToDelete").gameObject;
            FolderTab_to_delete = gameObject.transform.Find("FileSystem").Find("FolderTabToDelete").gameObject;
            Damaged_Filesystem = gameObject.transform.Find("Damages").gameObject;
            Gabriel.SetActive(false);
            Morgan.SetActive(false);
            Pat.SetActive(false);
            Damaged_Filesystem.SetActive(false);

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
        if(Input.GetKeyDown(KeyCode.Return))
        {
            output_text.text += ">>  " + input + "\n";
            inputfield.text = "";
        if (limit == 0)
        {           
          if (split[0] == "MalChecker")
            {
                if(split.Length == 2)
                {           
                shortened = input.Replace("MalChecker ", "");
                    if (split[1] == "--scan-all")
                    {                   
                        dialog_text = 
                        "Ada: Good, now you can see the malware \n" + "\n" +
                        "Charles: Luckily, MalChecker trapped them in the 'failure' folder.\n" + 
                        "They can’t move anymore." + 
                        "\nNow you just need to remove the folder.\n";
                
                    Gabriel.SetActive(true);
                    Morgan.SetActive(true);
                    Pat.SetActive(true);
                    Gabriel.GetComponent<MalwareMovement_Gabriel>().enabled = false;
                    Morgan.GetComponent<MalwareMovement_Morgan>().enabled = false;
                    Pat.GetComponent<MalwareMovement_Pat>().enabled = false;
                    Pat.transform.localScale -= new Vector3(0.7F, 0.7F, 0);
                    Gabriel.transform.localScale -= new Vector3(0.7F, 0.7F, 0);
                    Morgan.transform.localScale -= new Vector3(0.7F, 0.7F, 0);
                    limit += 1;                
                    }
                    chat();
                    inputfield.ActivateInputField();
                }

                    else if (split.Length == 1)
                    {
                        output_text.text += "Check the manual for options!" +"\n";
                    }
            }
}
        }  
    // Debug.Log(Gabriel.transform.position);
if (limit == 1)
{
     targetPosition = (new Vector3(240, 300, 0)); 
     float Pat_newPosition_x = Mathf.SmoothDamp(Pat.transform.position.x,  targetPosition.x, ref velocity, smoothTime);      
     float Pat_newPosition_y = Mathf.SmoothDamp(Pat.transform.position.y,  targetPosition.y, ref velocity, smoothTime);  
     float Gabriel_newPosition_x = Mathf.SmoothDamp(Gabriel.transform.position.x,  targetPosition.x, ref velocity, smoothTime);      
     float Gabriel_newPosition_y = Mathf.SmoothDamp(Gabriel.transform.position.y,  targetPosition.y, ref velocity, smoothTime);   
     float Morgan_newPosition_x = Mathf.SmoothDamp(Morgan.transform.position.x,  targetPosition.x, ref velocity, smoothTime);      
     float Morgan_newPosition_y = Mathf.SmoothDamp(Morgan.transform.position.y,  targetPosition.y, ref velocity, smoothTime);    
     Pat.transform.position = new Vector3(Pat_newPosition_x, Pat_newPosition_y, transform.position.z);
     Gabriel.transform.position = new Vector3(Gabriel_newPosition_x, Gabriel_newPosition_y, transform.position.z);
     Morgan.transform.position = new Vector3(Morgan_newPosition_x, Morgan_newPosition_y, transform.position.z);
   
    if(Input.GetKeyDown(KeyCode.Return))
    {
        inputfield.text = "";
        if (split[0] == "rm")
        {
            if(split.Length == 3)
            {
            shortened = input.Replace("rm ", "");
            if (split[1] == "-r" && split[2] == "failure")
            {
                dialog_text += 
                "\nCharles: Well done! Looks like you lost your files, though." +
                "\nGood thing we made a backup!" +
                "\nI knew copying all of your files without your permission would come in handy! I’ll send it to you now.\n" + "\n" +
                "\nReceived: backup.xml\n";
                
                limit += 1;
                DestroyImmediate(Gabriel);
                DestroyImmediate(Pat);
                DestroyImmediate(Morgan);
                Folder_to_delete.SetActive(false);//Set the deleted folder and tab be invisible 
                FolderTab_to_delete.SetActive(false);
                Damaged_Filesystem.SetActive(true);
                }   
            }
                else if(split.Length == 1)
                {
                    output_text.text += "Try using an option!"+ "\n";
                }
                else if(split[1] == "-r" && split.Length == 2)  
                {
                    output_text.text += "Type what you want to remove!" + "\n";
                }           
         
        }   
        manual();
        chat();
       
}
}
if (Input.GetKeyDown(KeyCode.Return))
{
    inputfield.text = "";
if (limit == 2 )
{
    if(split.Length == 4)
    {
    if (split[0] == "qoperation")
    {
        shortened = input.Replace("qoperation ", "");
        if (split[1] == "drbackup" && split[2] == "-t" && split[3] == "Q_FULL")
        {

            Damaged_Filesystem.SetActive(false);
            Folder_to_delete.SetActive(true);
            FolderTab_to_delete.SetActive(true);                        
            dialog_text += 
                "\nCharles: By the way, I also copied the English paper you had saved on here." +
                "\nBut I also just saved you. So you kind of owe me anyway." +
                "\nYou're welcome!";
           Invoke("chickendinner", 2);
            
        }
    }
    }
    

        else if(split.Length == 1)
        {
            output_text.text += "Try specifying what you want to do!" + "\n";
        }

        else if(split[1] == "drbackup" && split.Length == 2)
        {
            output_text.text += "Try using an option!" + "\n";
        }

        else if(split[1] == "drbackup" && split[2] == "-t" && split.Length == 3)
        {
            output_text.text += " Select the type of restore you want to use!" + "\n";
        }
    
    
}
}
    }

    void chat()
    {
       chat_man.text = dialog_text;
    }

    void chickendinner()
    { 
    SceneManager.LoadScene(("Ending Badge"));   
    }

    void manual()
    {
        chat_man.text ="MalChecker: Checks for malware.\n" +
        "--scan-all:\n" +
        "Selects everything for scanning.\n" +
        "\nrm:" +
        "\nShort for remove." +
        "\n-r: Followed by a filename allows you to completely delete the folder.\n" +
        "\nqoperation: Sets up the backup commands." +
        "\ndrbackup: Short for disaster recovery." +
        "\n-t Q_FULL: Restores entire filesystem.";
    }
}
