using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public class Util
{
    /// <summary>
    /// перегоняет объект в JSON и создаёт в корневой дирректории новый файл NewJson.json
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    public static void ToJsonAndCreateFile<T>(T obj)
    {
        string str = JsonUtility.ToJson(obj);
        StreamWriter writer = new StreamWriter("./NewJson.json", false); //false - чтобы файл перезаписывался заново
        writer.WriteLine(str);
        writer.Close();
    }

    /// <summary>
    /// Сериализует объект obj и сохранит в Profile.dat
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
