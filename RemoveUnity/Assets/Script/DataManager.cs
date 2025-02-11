using System.IO;
using UnityEngine;
using Yarn.Unity;
using System.Security.Cryptography;
using System.Collections.Generic;
using static Cinemachine.DocumentationSortingAttribute;

[System.Serializable]
public class Data
{
    public bool sawStoryScene;
    public bool sawTrueEnding;
    public bool sawEnding1;
    public bool sawEnding2;
    public bool sawEnding3;
    public bool sawEnding4;

    //public Data(bool sawStoryScene, bool sawTrueEnding, bool sawEnding1, bool sawEnding2, bool sawEnding3, bool sawEnding4)
    //{
    //    this.sawStoryScene = sawStoryScene;
    //    this.sawTrueEnding = sawTrueEnding;
    //    this.sawEnding1 = sawEnding1;
    //    this.sawEnding2 = sawEnding2;
    //    this.sawEnding3 = sawEnding3;
    //    this.sawEnding4 = sawEnding4;
    //}

}

public class DataManager : MonoBehaviour
{
    private static readonly string privateKey = "1718hy9dsf0jsdlfjds0pa9ids78ahgf81h32re";

    static Data data = new Data(/*true, false, false, false, false, false*/);

    string path;

    public static DataManager instance;
    private void Awake()
    {
        path = GetPath();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(instance);
        }

        //Save();
    }
    public static void Save()
    {
        //??????? ?÷???? ?????? ?κ??? ???? ?????? ?? ??????.

        string jsonString = DataToJson(data);
        string encryptString = Encrypt(jsonString);
        SaveFile(encryptString);
    }

    public static void Load()
    {
        if (!File.Exists(GetPath()))
        {
            Debug.Log("세이브 파일이 존재하지 않음.");
            return;
        }

        string encryptData = LoadFile(GetPath());
        string decryptData = Decrypt(encryptData);

        Debug.Log(decryptData);

        data = JsonToData(decryptData);
        //return data;
    }
    static string DataToJson(Data data)
    {
        string jsonData = JsonUtility.ToJson(data);
        return jsonData;
    }

    //json string?? SaveData?? ???
    static Data JsonToData(string jsonData)
    {
        data = JsonUtility.FromJson<Data>(jsonData);
        return data;
    }

    static string GetPath()
    {
        return Path.Combine(Application.dataPath, "database.json");
    }
    private void Start()
    {
        //LoadData();

    }
    public void LoadData()
    {
        string loadJson = File.ReadAllText(path);
        data = JsonUtility.FromJson<Data>(loadJson);
    }
    public void SaveData()
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }

    //json string?? ????? ????
    static void SaveFile(string jsonData)
    {
        using (FileStream fs = new FileStream(GetPath(), FileMode.Create, FileAccess.Write))
        {
            //????? ?????? ?? ??? ??????
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsonData);

            //bytes?? ???빰?? 0 ~ max ??????? fs?? ????
            fs.Write(bytes, 0, bytes.Length);
        }
    }

    //???? ???????
    static string LoadFile(string path)
    {
        using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            //?????? ?????? ???? ?? ???? ?????? ????
            byte[] bytes = new byte[(int)fs.Length];

            //???????????? ???? ????? ????
            fs.Read(bytes, 0, (int)fs.Length);

            //?????? ??????? json string???? ?????
            string jsonString = System.Text.Encoding.UTF8.GetString(bytes);
            return jsonString;
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////

    private static string Encrypt(string data)
    {

        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data);
        RijndaelManaged rm = CreateRijndaelManaged();
        ICryptoTransform ct = rm.CreateEncryptor();
        byte[] results = ct.TransformFinalBlock(bytes, 0, bytes.Length);
        return System.Convert.ToBase64String(results, 0, results.Length);

    }

    private static string Decrypt(string data)
    {

        byte[] bytes = System.Convert.FromBase64String(data);
        RijndaelManaged rm = CreateRijndaelManaged();
        ICryptoTransform ct = rm.CreateDecryptor();
        byte[] resultArray = ct.TransformFinalBlock(bytes, 0, bytes.Length);
        return System.Text.Encoding.UTF8.GetString(resultArray);
    }


    private static RijndaelManaged CreateRijndaelManaged()
    {
        byte[] keyArray = System.Text.Encoding.UTF8.GetBytes(privateKey);
        RijndaelManaged result = new RijndaelManaged();

        byte[] newKeysArray = new byte[16];
        System.Array.Copy(keyArray, 0, newKeysArray, 0, 16);

        result.Key = newKeysArray;
        result.Mode = CipherMode.ECB;
        result.Padding = PaddingMode.PKCS7;
        return result;
    }

    ////////////////////////////////////////////////////////////////////////////////////////


    [YarnFunction("getSawStoryScene")]
    public static bool GetSawStoryScene()
    {
        Load();
        return data.sawStoryScene;
    }

    [YarnCommand("setTrueSawStoryScene")]
    public static void SetTrueSawStoryScene()
    {
        data.sawStoryScene = true;
        Save();
    }
    [YarnFunction("getSawTrueEnding")]
    public static bool GetSawTrueEnding()
    {
        Load();
        return data.sawTrueEnding;
    }
    [YarnCommand("setTrueSawTrueEnding")]
    public static void SetTrueSawTrueEnding()
    {
        data.sawTrueEnding = true;
        Save();
    }
    [YarnCommand("setTrueSawEnding1")]
    public static void SetTrueSawEnding1()
    {
        data.sawEnding1 = true;
        Save();
    }
    [YarnCommand("setTrueSawEnding2")]
    public static void SetTrueSawEnding2()
    {
        data.sawEnding2 = true;
        Save();
    }
    [YarnCommand("setTrueSawEnding3")]
    public static void SetTrueSawEnding3()
    {
        data.sawEnding3 = true;
        Save();
    }
    [YarnCommand("setTrueSawEnding4")]
    public static void SetTrueSawEnding4()
    {
        data.sawEnding4 = true;
        Save();
    }

    [YarnCommand("isSawEnding")]
    public static bool IsSawEnding(int ending)
    {
        Load();
        switch (ending)
        {
            case 1:
                if (data.sawEnding1)
                    return true;
                break;
            case 2:
                if (data.sawEnding2)
                    return true;
                break;
            case 3:
                if (data.sawEnding3)
                    return true;
                break;
            case 4:
                if (data.sawEnding4)
                    return true;
                break;
            case 5:
                if (data.sawTrueEnding)
                    return true;
                break;
            default:
                return false;
        }
        return false;
    }
}