using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveAnimDie : MonoBehaviour {
    void Start() {
        
    }

    void Update() {
        
    }


    public void startMove(Vector3 startPositionNew, Vector3 finishPositionNew) {
        StartCoroutine(smoothMoveDie(startPositionNew, finishPositionNew));
    }
    IEnumerator smoothMoveDie(Vector3 startPositionNew, Vector3 finishPositionNew) { //плавная анимация движения
        float curTime = 0, timeOfTravel = 0.7f, normalizedValue;
        while(curTime < timeOfTravel) {
            curTime += Time.deltaTime;
            normalizedValue = curTime / timeOfTravel; // we normalize our time

            this.transform.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(startPositionNew, finishPositionNew, normalizedValue);

            yield return null;
        }

        Destroy(this.gameObject);
        yield break;
    }
}
