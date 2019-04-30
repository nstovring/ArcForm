using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileWriter : MonoBehaviour
{
    // Start is called before the first frame update
  

    public void AppendFile(string path, string line)
    {
        using (System.IO.StreamWriter file =
          new System.IO.StreamWriter(@""+path+".txt", true))
        {
            file.WriteLine(line);
        }
    }
}
