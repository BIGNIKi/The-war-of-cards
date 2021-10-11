using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class dontDestr : MonoBehaviour {
	[Header("Логин пользователя")]
	public string login;

	public atPlayerWindow aPW;

	public GameObject darkPanel; //ожидание ответа от сервера

	public test t;

	Scene scene;

	void Awake () { //исполняется до начала любых функций
		DontDestroyOnLoad (this);
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (aPW == null) {
			scene = SceneManager.GetActiveScene();
			if (scene.name == "atGame TestUI")
			{
				findMainScript();
			}
		}
	}

	public void findMainScript() { //поиск объекта на сцене
		GameObject mC = GameObject.Find("Main Camera");
		aPW = mC.GetComponent<atPlayerWindow>();
		aPW.mainInfoProfile.login = login;
		aPW.startChekOnError();
		//aPW.startUpdate();
		//aPW.updateShtabInfo();
		aPW.darkPanelFromMM = darkPanel;
		this.GetComponent<dontDestr>().enabled = false;
	}

	public void goBackAtMenu(string textError) { //запускается при ошибке загрузки с сервера в игре. Вернет в главное меню и напишет ошибку
		StartCoroutine(goBackParall(textError));
		
	}

	IEnumerator goBackParall(string textError) {
		if(textError == "Cannot resolve destination host" || textError == "Failed to receive data") {
			textError = "Ошибка интернет-соединения";
		}
		SceneManager.LoadScene(0); //сцена с гл меню
		GameObject mC = null;
		while(mC == null) {
			mC = GameObject.Find("Main Camera");
			yield return null;
		}
		t = mC.GetComponent<test>();
		t.errorText.text = textError;
		t.errorText.color = new Color(1, 0, 0, 1); //красный
		Destroy(this.gameObject);
		yield break;
	}
}
