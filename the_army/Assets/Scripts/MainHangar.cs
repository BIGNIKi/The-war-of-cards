using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using System.Text;

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

        public static Vector3 sizeOfSmallHQ = new Vector3(0.6f, 0.6f, 0.6f);

        //[SerializeField] - если это написать перед приватным полем класса, то это поле будет отображаться в редакторе
        //[Range(0, 100)] - появится удобный ползунок для изменения значения
        //[Space] - добавит отступ в инспекторе

        void Start()
        {
            LoadProfileDebugJson();
            //Util.ToBinaryAndCreateFile<MainInfo>(accountInfo);
            LoadMainStuffs();
            //LoadProfileDebugBinary();
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
        /// загружает имя профиля, число денег, опыта и золота
        /// </summary>
        private void LoadMainStuffs()
        {
            GameObject.Find("Main Camera/Canvas/background/Profile/name").GetComponent<Text>().text = accountInfo.login;
            GameObject.Find("Main Camera/Canvas/UpPlashkaImg/moneyHere/Text").GetComponent<Text>().text = accountInfo.money.ToString();
            GameObject.Find("Main Camera/Canvas/UpPlashkaImg/freExpHere/Text").GetComponent<Text>().text = accountInfo.expi.ToString();
            GameObject.Find("Main Camera/Canvas/UpPlashkaImg/goldHere/Text").GetComponent<Text>().text = accountInfo.gold.ToString();
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
                neww.GetComponent<RectTransform>().localScale = sizeOfSmallHQ; //изначально созданный штаб маленького размера
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
            structure.Find("sortToCentreFIST/powerText").GetComponent<Text>().text = headInf.power.ToString() + " + power from cards";
            structure.Find("sortToCentreEXP/expText").GetComponent<Text>().text = SplitStr(userHeadInf.exp.ToString(), 3);
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

            for(int i = 0; i<userHeadInf.decks.Count; i++)
            {
                if(userHeadInf.decks[i].isSelected)
                {
                    structure.Find("kolodaText").GetComponent<Text>().text = userHeadInf.decks[i].name;
                    break;
                }
                if(i == userHeadInf.decks.Count-1)
                {
                    Debug.LogError("There are no selected deck");
                }
            }
        }

        /// <summary>
        /// Вставляет пробелы в строку через каждые maxSymbols начиная с конца
        /// </summary>
        /// <param name="str"></param>
        /// <param name="maxSymbols"></param>
        /// <returns></returns>
        private string SplitStr(string str, int maxSymbols)
        {
            var sb = new StringBuilder();
            var counter = str.Length;
            foreach(var element in str)
            {
                if(counter % maxSymbols == 0)
                {
                    sb.Append(" ");
                }

                sb.Append(element);
                counter--;
            }
            return sb.ToString();
        }

        private Headquarter GetUserHeadquarter(string name)
        {
            for(int i = 0; i < accountInfo.headQuars.Count; i++)
            {
                if(accountInfo.headQuars[i].name.Equals(name))
                {
                    return accountInfo.headQuars[i];
                }
                if(i == accountInfo.headQuars.Count - 1)
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
        public struct Deck
        {
            public string name;
            public bool isSelected; //выбрана эта колода или нет
            public List<CardAndCount> cards;
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
        public struct Headquarter 
        {
            public string name;
            public int power;
            public int exp; //опыт штаба
            public List<Deck> decks; //колоды для этого штаба
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
            public int count; //это помечает ещё и факт исследования карты (если 0 и больше - исследована), если нет Card'ы, то она вовсе не исследована
        }
    }
}