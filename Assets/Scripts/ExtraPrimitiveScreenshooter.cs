using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class ExtraPrimitiveScreenshooter : MonoBehaviour
{
    private static bool IsExists;
    [SerializeField] private string _subFolder = "android";
    private int index = 0;

#if UNITY_EDITOR
    private void Awake()
    {
        if (IsExists)
        {
            Destroy(gameObject);
            return;
        }

        IsExists = true;

        DontDestroyOnLoad(this.gameObject);

        if (PlayerPrefs.HasKey("screenshotIndex"))
        {
            index = PlayerPrefs.GetInt("screenshotIndex");
        }

        if (!Directory.Exists("Screenshots"))
        {
            Directory.CreateDirectory("Screenshots");
            Debug.LogWarning("Don't forget to add Screenshots folder to \".gitignore\"!");
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            string directoryPath = string.Format("Screenshots/{0}/{1}x{2}", _subFolder, Screen.width, Screen.height);

            if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

            ScreenCapture.CaptureScreenshot(string.Format("{0}/{1}.png", directoryPath, index));

            index++;
            PlayerPrefs.SetInt("screenshotIndex", index);
        }
    }
#endif

}