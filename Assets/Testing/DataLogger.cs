using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataLogger : MonoBehaviour
{
    public string ParticipantNr;
    public static DataLogger Instance;

    [Header("Timers")]
    public float TestTime;
    public float ActionTime;
    public float InstructionTime;

    [Header("Booleans")]
    public bool LoggingTestTime;
    public bool LoggingTaskTime;
    public bool LogginActionTime;
    public bool LoggingInstructionTime;
    bool awaitParticipantNumber = true;
    public bool enabledInUpdate = true;

    [Header("Refs")]
    public TestUIElements elements;
   
    [Header("Tasks")]
    public int TaskCount = 0;
    public Task currentTask;
    public List<Task> MyTasks;

    [Header("Scene")]
    int sceneCount = 0;


    public string testPath = "E:/GitHub/ArcForm/Assets/Data/";
    // Start is called before the first frame update
    public enum TaskType { FindUnitoken, IsolateToken, FindRelation, IsolateRelation, SelectUnitoken, AwaitInput }
    [System.Serializable]
    public class Task
    {
        [Header("Task Description")]
        public string title;
        public string description;
        public TaskType type;
        public string focusedLabel;
        public KeyCode key;

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
                case TaskType.SelectUnitoken:
                    return SelectUnitoken();
                case TaskType.AwaitInput:
                    return AwaitInput();
            };
            return false;
        }

        public bool twiceTask = false;

        public bool FindUnitoken()
        {
            if (ArcMapManager.Instance.unitokens.Any(x => x.myLabel.text == focusedLabel))
            {
                if (!twiceTask)
                {
                    return true;
                }
                else
                {
                    Fragment y = ArcMapManager.Instance.unitokens.Find(j => j.myLabel.text == focusedLabel);
                    y.myLabel.text = focusedLabel + " ";
                }
                return true;
            }
            return false;
        }

        public bool SelectUnitoken()
        {
            if(ArcToolUIManager.ArcUIUtility.PropertyMenuText.text == focusedLabel)
            {
                return true;
            }
            return false;
        }

        public bool IsolateToken()
        {
            if (ArcToolUIManager.Instance.subItems != null && ArcToolUIManager.Instance.subItems.Count > 0)
            {
                foreach (ArcMenuSubItem item in ArcToolUIManager.Instance.subItems)
                {
                    if (item.text.text != focusedLabel)
                    {
                        if (item.isActive)
                            return false;
                    }
                }

                return true;
            }

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
            if(ArcMapManager.Instance.unitokens.Count == 1)
            {
                if(ArcMapManager.Instance.unitokens[0].myLabel.text == focusedLabel)
                {
                    return true;
                }
            }

            return false;
        }

        public bool AwaitInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                return true;
            }
            return false;
        }
    }

    public bool spaceInput;

    public void Start()
    {
        Instance = this;
        testPath = Application.dataPath;
        currentTask = MyTasks[TaskCount];
        elements = GameObject.FindGameObjectWithTag("TestTaskPanel").GetComponent<TestUIElements>();
        elements.testPanelTitle.text = currentTask.title + " : TestNum " + ((int)SceneManager.GetActiveScene().buildIndex);
        elements.testPanelText.text = currentTask.description;
    }

    public void Reset()
    {
        
    }

    private void Update()
    {
        //Complete Task
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if(currentTask != null)
            currentTask.isCompleted = true;
        }
       
        //Next Scene
        if (Input.GetKeyDown(KeyCode.F2))
        {
            LoadScene();
        }

        //Restart Test
        if (Input.GetKeyDown(KeyCode.F3))
        {
            SceneManager.LoadScene(0);
        }

        if (!enabledInUpdate)
            return;
        if(!awaitParticipantNumber)
            TestTime += Time.deltaTime;
        ActionTime += Time.deltaTime;
        ParticipantNr = elements.testPanelInput.text;

       
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
        awaitParticipantNumber = false;
        elements.testPanelInput.transform.gameObject.SetActive(false);
        ActionTime = 0;
        currentTask.taskNum = TaskCount;
        StartCoroutine(EvalTask(currentTask));
        TaskCount++;
    }
    public void LogAction(string Action)
    {
        string log;
        log = Action + " , " + TestTime + " , " + currentTask.TaskTime + " , " + ActionTime + " , " + (currentTask.taskNum + 1)+ " , " + currentTask.LoggedActions.Count;
        currentTask.LoggedActions.Add(log);
        AppendFile(testPath, log);
        ActionTime = 0;
    }
    public void LogSelection(Unitoken token)
    {
        string log;
        string Action = "Selected Token : " + token.myLabel.text;
        log = Action +" , " + TestTime + " , " + currentTask.TaskTime + " , " + ActionTime + " , " + (currentTask.taskNum + 1) + " , " + currentTask.LoggedActions.Count;
        currentTask.LoggedActions.Add(log);
        AppendFile(testPath, log);
        ActionTime = 0;
    }

    public void LogToggle(ArcMenuItem ami)
    {
        string log;
        string Action = "Toggled Menu Item : " + ami.textField.text + " : " + ami.buttonStateHandler.myState.ToString("F");
        log = Action + " , " + TestTime + " , " + currentTask.TaskTime + " , " + ActionTime + " , " + (currentTask.taskNum + 1) + " , " + currentTask.LoggedActions.Count;
        currentTask.LoggedActions.Add(log);
        AppendFile(testPath, log);
        ActionTime = 0;
    }

    public void LogToggle(ArcMenuSubItem amsi)
    {
        string log;
        string Action = "Toggled Sub Menu Item : " + amsi.text.text + " : " + amsi.isActive;
        log = Action + " , " + TestTime + " , " + currentTask.TaskTime + " , " + ActionTime + " , " + (currentTask.taskNum + 1) + " , " + currentTask.LoggedActions.Count;
        currentTask.LoggedActions.Add(log);
        AppendFile(testPath, log);
        ActionTime = 0;
    }

    public IEnumerator EvalTask(Task task)
    {
        MovePanelToCorner();

        LogAction("Task : " + task.title + " : Started");
        while (!task.isCompleted)
        {
            LoggingTaskTime = true;
            task.TaskTime += Time.deltaTime;
            task.isCompleted = task.Eval();
            yield return new WaitForEndOfFrame();
        }

        LogAction("Task : " + task.title + " : Completed");
        //Log data
        //Clear list
        if (TaskCount < MyTasks.Count)
        {
            currentTask = MyTasks[TaskCount];

            elements.testPanelTitle.text = currentTask.title + " : Test " + ((int)SceneManager.GetActiveScene().buildIndex); ;
            elements.testPanelText.text = currentTask.description;
        }
        else
        {
            elements.testPanelTitle.text = "The End of Test: " + ((int)SceneManager.GetActiveScene().buildIndex); ;
            elements.testPanelText.text = "Thank you for participating in this test";
            Debug.Log("End Test");
            yield return new WaitForSeconds(10.0f);

            LoadScene();
        }

        MovePanelToCenter();
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        //if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCount)
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //}
        //else
        //{
        //    SceneManager.LoadScene(0);
        //}
    }

    public void MovePanelToCorner()
    {
        Mouselistener.Instance.isActive = true;
        elements.backgroundImage.transform.gameObject.SetActive(false);
        elements.testPanelbutton.transform.gameObject.SetActive(false);
        RectTransform rt = (RectTransform)elements.testPanel.transform;
        rt.anchorMax = new Vector2(1, 1);
        rt.anchorMin = new Vector2(1, 1);
        rt.pivot = new Vector2(1, 1);
        rt.anchoredPosition = Vector2.zero;
    }

    public void MovePanelToCenter()
    {
        Mouselistener.Instance.isActive = false;
        elements.backgroundImage.transform.gameObject.SetActive(true);
        elements.testPanelbutton.transform.gameObject.SetActive(true);
        RectTransform rt = (RectTransform)elements.testPanel.transform;
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = Vector2.zero;
    }

  

    public void AppendFile(string path, string line)
    {
        if (!enabledInUpdate)
            return;
        int testIndex = SceneManager.GetActiveScene().buildIndex;
        using (System.IO.StreamWriter file =
          new System.IO.StreamWriter(@"" + path + "Participant " + ParticipantNr + ".txt", true))
        {
            file.WriteLine(line + "," + testIndex);
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
