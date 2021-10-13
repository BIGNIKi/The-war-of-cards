using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public class Util
{
    /// <summary>
    /// ���������� ������ � JSON � ������ � �������� ����������� ����� ���� NewJson.json
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    public static void ToJsonAndCreateFile<T>(T obj)
    {
        string str = JsonUtility.ToJson(obj);
        StreamWriter writer = new StreamWriter("./NewJson.json", false); //false - ����� ���� ��������������� ������
        writer.WriteLine(str);
        writer.Close();
    }

    /// <summary>
    /// ����������� ������ obj � �������� � Profile.dat
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    public static void ToBinaryAndCreateFile<T>(T obj)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        using FileStream fs = new FileStream("Profile.dat", FileMode.OpenOrCreate);
        formatter.Serialize(fs, obj);
    }

}
