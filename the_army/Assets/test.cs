using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System;

public class test : MonoBehaviour {
	public AudioClip middleButtonSound; //озвучка наведения на кнопку
	public AudioClip middleButtonClickSound; //озвучка нажатия

	public AudioSource mainAudioSource; //проигрывает звук наведения
	public AudioSource forClickAudioSource; //проигрывает звук нажатия

	[Header ("Логин пользователя")]
	public string login; //здесь хранится логин
	[Header ("Пароль пользователя")]
	public string password; //здесь хранится пароль

	public InputField loginField; //поле логина
	public InputField passwordField; //поле пароля
	public Toggle canSavePass; //можем ли запомнить пароль?

	public GameObject darkPanel; //ожидание ответа от сервера

	public Text errorText; //текст с ошибкой

	public dontDestr dD; //сюда отправятся все данные профиля


	void Start () {
		if (File.Exists ("./userInfo.sv")) { //если файл существует
			StreamReader sr = new StreamReader ("./userInfo.sv", System.Text.Encoding.UTF7); //штука, которая будет читать
			if (sr != null) { //если все норм
				loginField.text = sr.ReadLine(); //прочитали строку
				login = loginField.text;
				string iso = sr.ReadLine();
				if (iso == "True") {
					canSavePass.isOn = true;
				} else {
					canSavePass.isOn = false;
				}
				if (canSavePass.isOn) {
					passwordField.text = sr.ReadLine();
					password = passwordField.text;
				}
				sr.Close(); //закрыли файл
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	private IEnumerator Send() { //регистрация
		WWWForm form = new WWWForm ();
		form.AddField ("Login", login);
		form.AddField ("Password", password);
		UnityWebRequest www = UnityWebRequest.Post ("http://thearmynations.ru/main/index.php", form);
		yield return www.SendWebRequest(); //ждет, пока не отправит
		if (www.error != null) {
			errorText.text = www.error;
			errorText.color = new Color(1, 0, 0, 1);
			darkPanel.SetActive(false);
			yield break;
		}
		if (www.downloadHandler.text == "001") {
			//Debug.Log(www.downloadHandler.text);
			errorText.text = "Этот аккаунт занят...";
			errorText.color = new Color (1,0,0,1); //красный
		} else if (www.downloadHandler.text == "0") {
			errorText.text = "Пользователь зарегистрирован.";
			errorText.color = new Color (0,1,0,1); //зеленый
		}
		darkPanel.SetActive(false);
	}

	private IEnumerator tryConnect() { //вход
		WWWForm form = new WWWForm ();
		form.AddField ("Login", login);
		form.AddField ("Password", password);
		UnityWebRequest www = UnityWebRequest.Post ("http://thearmynations.ru/main/tryEntranceToAccount.php", form);
		yield return www.SendWebRequest(); //ждет, пока не отправит
		if (www.error != null) {
			if(www.error == "Cannot resolve destination host" || www.error == "Failed to receive data") {
				errorText.text = "Ошибка интернет-соединения";
			} else {
				errorText.text = www.error;
			}
			errorText.color = new Color(1, 0, 0, 1);
			darkPanel.SetActive(false);
			yield break;
		}
		if (www.downloadHandler.text == "2") {
			errorText.text = "Неверный логин или пароль...";
			errorText.color = new Color (1,0,0,1); //красный
		} else if (www.downloadHandler.text == "0") { //вход в игру
			errorText.color = new Color (1,0,0,0); //прозрачный
												   //Application.LoadLevel(1);
			saveLoginAndPass();
			//Debug.Log(login);
			dD.login = login; //записали логин
			SceneManager.LoadScene(1);
			yield break;
		}
		darkPanel.SetActive(false);
	}

	public void clickMiddleButton () { //наведение на кнопку
		mainAudioSource.clip = middleButtonSound;
		mainAudioSource.Play ();
	}

	public void clickMiddleButtonWITH () { //прям КЛИК на кнопку
		forClickAudioSource.clip = middleButtonClickSound;
		forClickAudioSource.Play ();
	}

	public void savePassOrLogin (string text) { //метод срабатывает, когда мы отредактировали логин или пароль, чтобы записать их в переменные 
		if (text == "Login") {
			login = loginField.text;
		} else if (text == "Password") {
			password = passwordField.text;
		}
	}

	public void entrance () { //нажатие Регистрация
		if (login.Length < 6 || password.Length < 8) {
			errorText.text = "Неверная длина введенного поля.\n Логин 6+, пароль 8+";
			errorText.color = new Color (1,0,0,1); //красный
			return;
		}
		StartCoroutine (Send());
		darkPanel.SetActive (true);
	}

	public void tryConnection () { //нажатие Вход
		StartCoroutine (tryConnect());
		darkPanel.SetActive (true);
	}

	void OnApplicationQuit() {
		saveLoginAndPass();
	}

	void saveLoginAndPass() { //сохранит логин и пароль
		StreamWriter sw = new StreamWriter("./userInfo.sv", false, System.Text.Encoding.UTF7);
		sw.WriteLine(login);
		sw.WriteLine(canSavePass.isOn);
		if (canSavePass.isOn)
		{
			sw.WriteLine(password);
		}
		sw.Close();
	}
}