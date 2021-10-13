using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

#pragma warning disable 0649

namespace MyNameSpace
{

    //вариант - хранить в базе данных значения: login + байтовая, сериализованная последовательность
    //искать поле (нужного игрока) будем по login'у, а сериализованные данные загружать в структуру, тем самым не придется звать JSON'ы

    public class MainHangar : MonoBehaviour
    {
        [Header("Основная информация о профиле")]
        public MainInfo accountInfo;

        public GameObject prefOfShtab; //префаб штаба

        //[SerializeField] - если это написать перед приватным полем класса, то это поле будет отображаться в редакторе
        //[Range(0, 100)] - появится удобный ползунок для изменения значения
        //[Space] - добавит отступ в инспекторе

        void Start()
        {
            //LoadProfileDebugJson();
            LoadProfileDebugBinary();
            InitAllHeadQuarters();
        }

        /// <summary>
        /// Загрузит профиль для Debug mod'а из JSON файла
        /// </summary>
        private void LoadProfileDebugJson()
        {
            if(Debug.isDebugBuild && File.Exists("./NewJson.json")) //если файл существует и мы в Debug моде
            {
                StreamReader sr = new StreamReader("./NewJson.json"); //штука, которая будет читать
                if(sr != null) //если все норм
                { 
                    string userInfoText = sr.ReadToEnd();
                    sr.Close(); //закрыли файл
                    accountInfo = JsonUtility.FromJson<MainInfo>(userInfoText);
                    return;
                }
            }
            Debug.LogError("Couldn't find a file.");
        }

        private void LoadProfileDebugBinary()
        {
            if(Debug.isDebugBuild && File.Exists("./Profile.dat")) //если файл существует и мы в Debug моде
            {
                FileStream fs = new FileStream("./Profile.dat", FileMode.OpenOrCreate);

                if(fs != null)
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    accountInfo = (MainInfo)formatter.Deserialize(fs);
                    fs.Close();
                    return;
                }
            }
            Debug.LogError("Couldn't find a file.");
        }

        /// <summary>
        /// Создание всех штабов игрока (чтобы их было видно)
        /// </summary>
        private void InitAllHeadQuarters()
        {
            //количество изначально валяющихся там штабов
            GameObject sliderShtab = GameObject.Find("Main Camera/Canvas/background/sliderShtab (1)/sliderShtab");
            int countChild = sliderShtab.transform.childCount;
            for(int i = 0; i < countChild; i++)
            {
                Destroy(sliderShtab.transform.GetChild(i).gameObject);
            }

            for(int i = 0; i < accountInfo.headQuars.Count; i++)
            {
                GameObject neww = Instantiate(prefOfShtab);
                if(i != 0)
                {
                    neww.GetComponent<RectTransform>().localScale = new Vector3(0.6f, 0.6f, 0.6f); //размеры всех штабов меньше, чем размер выбранного штаба
                }
                neww.transform.SetParent(sliderShtab.transform, false);
                neww.name = accountInfo.headQuars[i].name;
                //TODO
            }
        }

        void Update()
        {

        }

        [Serializable]
        public struct MainInfo
        {
            public string login;
            public int money;
            public int expi;
            public int gold;
            public List<Headquarter> headQuars;
            public List<Card> resCards;
        }

        [Serializable]
        public struct Headquarter //информация о штабах игрока
        {
            public string name;
            public int power;
            public int exp; //опыт штаба
            public List<CardAndCount> cards; //все карты, выбранные для этого штаба
        }

        [Serializable]
        public struct CardAndCount //имя карты и сколько штук
        {
            public string name;
            public int count; //количество
        }

        [Serializable]
        public struct Card //имя и количество данной карты (в профиле)
        {
            public string name;
            public int count;
        }
    }
}