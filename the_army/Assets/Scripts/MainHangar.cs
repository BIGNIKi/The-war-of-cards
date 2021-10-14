using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

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

        public DataSet dataset;

        public LocalisationData localization;

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
                DataSet.HeadquarterInfo headInfo = dataset.GetHeadquarterInfoByName(accountInfo.headQuars[i].name);
                bool isBig = false;
                if(i == 0) isBig = true;
                AddHeadQuarter(headInfo, isBig, accountInfo.headQuars[i]);
            }
            //я бы очень хотел исполнить строку ниже, но так как кадр ещё не сменился нельзя получить новосозданных детей (их не существуют в контексте текущего кадра)
            //поэтому приходится делать костыли по типу isBig
            //headSlider.transform.GetChild(0).gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }

        private void AddHeadQuarter(DataSet.HeadquarterInfo headInf, bool isBig, Headquarter userHeadInf)
        {
            GameObject sliderShtab = GameObject.Find("Main Camera/Canvas/background/sliderShtab (1)/sliderShtab");
            GameObject neww = Instantiate(prefOfShtab);
            if(!isBig)
            {
                neww.GetComponent<RectTransform>().localScale = new Vector3(0.6f, 0.6f, 0.6f); //изначально созданный штаб маленького размера
            }
            else
            {
                neww.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1); //изначально созданный штаб маленького размера
            }
            
            neww.transform.SetParent(sliderShtab.transform, false);
            neww.name = headInf.name;
            neww.transform.GetComponent<Image>().sprite = dataset.GetHeadquarterSpriteByName(headInf.name); //main picture of headquarter
            Transform structure = neww.transform.Find("bodyPapkaImg/structure");

            switch(localization.language)
            {
                case LocalisationData.Language.Russian:
                    structure.Find("NameBaseText").GetComponent<Text>().text = headInf.info0rus;
                    structure.Find("discriptionBaseText").GetComponent<Text>().text = headInf.info1rus;
                    break;
                case LocalisationData.Language.English:
                    structure.Find("NameBaseText").GetComponent<Text>().text = headInf.info0eng;
                    structure.Find("discriptionBaseText").GetComponent<Text>().text = headInf.info1eng;
                    break;
            }
            structure.Find("kolodaText").GetComponent<Text>().text = "Имя колоды";
            structure.Find("sortToCentreFIST/powerText").GetComponent<Text>().text = headInf.power.ToString() + " + power from cards";
            structure.Find("sortToCentreEXP/expText").GetComponent<Text>().text = userHeadInf.exp.ToString();
            String tmp = headInf.nation; //нация штаба
            if(tmp.Equals("USSR"))
            { 
                Instantiate(dataset.prefOfFlags[0], neww.transform.Find("bodyPapkaImg/structure/flagImg")); //здесь генерю нужный флаг
            }
            else if(tmp.Equals("Germany"))
            {
                Instantiate(dataset.prefOfFlags[1], neww.transform.Find("bodyPapkaImg/structure/flagImg"));
            }
            else if(tmp.Equals("USA"))
            {
                Instantiate(dataset.prefOfFlags[2], neww.transform.Find("bodyPapkaImg/structure/flagImg"));
            }
        }

        private Headquarter GetUserHeadquarter(string name)
        {
            for(int i = 0; i<accountInfo.headQuars.Count; i++)
            {
                if(accountInfo.headQuars[i].name.Equals(name))
                {
                    return accountInfo.headQuars[i];
                }
                if(i == accountInfo.headQuars.Count-1)
                {
                    Debug.LogError("Headquarter isn't found");
                }
            }
            return new Headquarter();
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