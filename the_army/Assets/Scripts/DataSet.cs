using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MyNameSpace
{

    public class DataSet : MonoBehaviour
    {
        [Header("Основная информация о параметрах штабов")]
        public HeadquarterInfo[] headquarterInfo;

        [Serializable]
        public struct HeadquarterInfo //информация о штабе
        {
            public string name;
            public int expPrice;
            public int monPrice;
            public string info0rus;
            public string info1rus;
            public string info0eng;
            public string info1eng;
            public int power;
            public int hp;
            public int damage;
            public int plusFuel;
            public string nation;
        }
    }
}