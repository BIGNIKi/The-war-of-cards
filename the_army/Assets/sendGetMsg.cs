//отвественен за получение и отправку сообщений
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;

public class sendGetMsg : MonoBehaviour {
	public InputField textOfMsg; //здесь лежит текст сообщения, которое мы пишем

	public string login; //ник игрока

	public float timer = 0; //считает время

	public string hourAndMinutes = "-1"; //здесь хранится время последнего сообщения (пример: 22:50)

	public int idAtChat = -1; //здесь хранится id последнего сообщения

	public bool idColor = false; //0 - черный, 1 - посветлее (цвет следующего сообщения)

	public GameObject content; //в него кладутся новые сообщения
	public GameObject newMsgPrefab; //префаб сообщения

	void Start() {
		login = GameObject.Find("Main Camera/Canvas/background/Profile/name").GetComponent<Text>().text; //находим логин
	}


	void Update() {
		timer += Time.deltaTime; //время тикает
		if (timer >= 1f) { //каждую секунду проверяем новые сообщения на сервере
			timer = 0;
			StartCoroutine(checkOnNewMsg());
		}
	}

	private IEnumerator checkOnNewMsg(){ //проверяет на новые сообщения
		WWWForm form = new WWWForm();
		form.AddField("hour", hourAndMinutes);
		form.AddField("idAtChat", idAtChat);
		form.AddField("nameOfTable", this.name);
		UnityWebRequest www = UnityWebRequest.Post("http://thearmynations.ru/main/alwaysUpdateChat.php", form);
		yield return www.SendWebRequest(); //ждет, пока отправит
		if (www.error != null) {
			Debug.Log(www.error);
			yield break;
		}
		MaybeMessage mM = JsonUtility.FromJson<MaybeMessage>(www.downloadHandler.text);
		if (mM.login == "0") { //когда в логине ноль (не нашел новых сообщений)
			hourAndMinutes = mM.hour;
			idAtChat = mM.idAtChat;
		} else { //нашли новое сообщение
			hourAndMinutes = mM.hour;
			idAtChat = mM.idAtChat;
			GameObject neww = Instantiate(newMsgPrefab); //создали сообщение
			neww.transform.SetParent(content.transform, false);
			if (!idColor) {
				neww.GetComponent<Image>().color = new Color(65 / 255.0f, 65 / 255.0f, 65 / 255.0f);
				idColor = true;
			} else {
				neww.GetComponent<Image>().color = new Color(100 / 255.0f, 100 / 255.0f, 100 / 255.0f);
				idColor = false;
			}
			Transform textulya = neww.transform.Find("Text");
			textulya.GetComponent<Text>().text = "[" + mM.hour + "] " + mM.login + ":\n" + mM.textMsg;
			//Debug.Log(www.downloadHandler.text);
		}
		yield break;
	}

		private IEnumerator Send() { //отправка на сервер
		if (textOfMsg.text == "") { //пустое сообщение сосет
			yield break;
		}
		WWWForm form = new WWWForm();
		form.AddField("nameOfTable", this.name);
		form.AddField("login", login);
		form.AddField("textOfMsg", textOfMsg.text.Replace('\n', ' '));
		UnityWebRequest www = UnityWebRequest.Post("http://thearmynations.ru/main/funcChat.php", form);
		yield return www.SendWebRequest(); //ждет, пока отправит
		textOfMsg.text = "";
		yield break;
	}

	public void completeTextMsg() { //когда нажал на кнопку отправки сообщения
		StartCoroutine(Send());
	}
	public void maybeString() { //когда нажал ентер (запускается каждые раз, когда срабатывает event end edit)
		if (Input.GetKey(KeyCode.Return)) {
			StartCoroutine(Send());
		}
		}
}

[Serializable]
public class MaybeMessage { //структура POST ответа с сервера
	public string hour; //время
	public int idAtChat; //id в чате
	public string login; //никнейм
	public string textMsg; //текст сообщения
}