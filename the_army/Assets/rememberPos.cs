using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rememberPos : MonoBehaviour  {
    public Vector2 myPos;

    public int upOrDown = 0; // 0 - вниз, 1 - вверх, 2 - нельзя двигать (статичен)
    public int myPosAtHieararchy; //позиция в иерархии

    public unitParam uP;

    void Start() {
        //myPos = this.GetComponent<RectTransform>().anchoredPosition;
        myPos = new Vector2(0, -58.18904f);
    }

    void Update(){
        
    }
}
