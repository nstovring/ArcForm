using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DataLogger : MonoBehaviour
{
    public static DataLogger Instance;
    public string ParticipantNr;

    [Header("Timers")]
    public float TestTime;
    public float ActionTime;
    public float InstructionTime;

    [Header("Booleans")]
    public bool LoggingTestTime;
    public bool LoggingTaskTime;
    public bool LogginActionTime;
    public bool LoggingInstructionTime;

    [Header("Refs")]
    public Text testPanelTitle;
    public Text testPanelText;
    public Transform testPanel;

    [Header("Tasks")]
    public int TaskCount = 0;
    public Task currentTask;
    public List<Task> MyTasks;


    public string testPath = "E:/GitHub/ArcForm/Assets/Data/";
    // Start is called before the first frame update
    public enum TaskType { FindUnitoken, IsolateToken, FindRelation, IsolateRelation }
    [System.Serializable]
    public class Task
    {
        [Header("Task Description")]
        public string title;
        public string description;
        public TaskType type;
        public string focusedLabel;

        [Header("Test Vars")]
        public bool isCompleted;
        public int taskNum;
        public float TaskTime;
        public List<string> LoggedActions;

        public bool Eval()
        {
            switch (type)
            {
                case TaskType.FindUnitoken:
                    return FindUnitoken();
                case TaskType.IsolateToken:
                    return IsolateToken();
                case TaskType.FindRelation:
                    return FindRelation();
                case TaskType.IsolateRelation:
                    return IsolateRelation();
            };
            return false;
        }
        public bool FindUnitoken()
        {
            if(ArcMapManager.Instance.unitokens.Any(x => x.myLabel.text == focusedLabel))
            {
                return true;
            }
            return false;
        }

        public bool IsolateToken()
        {
            return false;
        }

        public bool FindRelation()
        {
            if (ArcMapManager.Instance.ArcCollections.Any(x => x.myLabel.text == focusedLabel))
            {
                return true;
            }
            return false;
        }

        public bool IsolateRelation()
        {
            return false;
        }

      
    }

    public void Start()
    {
        Instance = this;
        currentTask = MyTasks[TaskCount];
        testPanelTitle.text = currentTask.title;
        testPanelText.text = currentTask.description;
    }

    private void Update()
    {
        TestTime += Time.deltaTime;
        ActionTime += Time.deltaTime;
    }
    public void WriteTaskToFile(Task task)
    {
        foreach (string x in task.LoggedActions)
        {
            AppendFile(testPath, x);
        }
    }


    public void StartCurrentTaskTest()
    {
        //currentTask = MyTasks[TaskCount];
        currentTask.taskNum = TaskCount;
        StartCoroutine(EvalTask(currentTask));
        TaskCount++;
        //If task count >= count end test
    }
    public void LogAction(string Action)
    {
        string log;
        log = Action + " , " + TestTime + " , " + currentTask.TaskTime + " , " + ActionTime + " , " + currentTask.taskNum;
        currentTask.LoggedActions.Add(log);
        AppendFile(testPath, log);
        ActionTime = 0;
    }
    public void LogSelection(Unitoken token)
    {
        string log;
        string Action = "Selected Token : " + token.myLabel.text;
        log = Action +" , " + TestTime + " , " + currentTask.TaskTime + " , " + ActionTime;
        currentTask.LoggedActions.Add(log);
        AppendFile(testPath, log);
        ActionTime = 0;
    }

    public void LogToggle(ArcMenuItem ami)
    {
        string log;
        string Action = "Toggled Menu Item : " + ami.textField.text + " : " + ami.myButtonToggle.toggled;
        log = Action + " , " + TestTime + " , " + currentTask.TaskTime + " , " + ActionTime;
        currentTask.LoggedActions.Add(log);
        AppendFile(testPath, log);
        ActionTime = 0;
    }

    public void LogToggle(ArcMenuSubItem amsi)
    {
        string log;
        string Action = "Toggled Sub Menu Item : " + amsi.text.text + " : " + amsi.isActive;
        log = Action + " , " + TestTime + " , " + currentTask.TaskTime + " , " + ActionTime;
        currentTask.LoggedActions.Add(log);
        AppendFile(testPath, log);
        ActionTime = 0;
    }

    public IEnumerator EvalTask(Task task)
    {
        testPanel.transform.gameObject.SetActive(false);
        
        while (!task.isCompleted)
        {
            LoggingTaskTime = true;
            task.TaskTime += Time.deltaTime;
            task.isCompleted = task.Eval();
            yield return new WaitForEndOfFrame();
        }

        //Log data
        //Clear list
        if(TaskCount < MyTasks.Count)
        {
            currentTask = MyTasks[TaskCount];
            testPanel.transform.gameObject.SetActive(true);
            testPanelTitle.text = currentTask.title;
            testPanelText.text = currentTask.description;
        }
        else
        {
            Debug.Log("End Test");
        }
    }

  

    public void AppendFile(string path, string line)
    {
        using (System.IO.StreamWriter file =
          new System.IO.StreamWriter(@"" + path + "Participant "+ParticipantNr+ ".txt", true))
        {
            file.WriteLine(line);
        }
    }

    void OnGUI()
    {

        List<string> debugString = new List<string>();
        GUIStyle gsTest = new GUIStyle();
        gsTest.normal.textColor = Color.black;
        GUILayout.BeginArea(new Rect(Screen.width - 250,0 , 250, 250), gsTest);
        foreach (string x in debugString)
        {
            GUILayout.Label(x, gsTest);
        }
        GUILayout.EndArea();
    }

}
