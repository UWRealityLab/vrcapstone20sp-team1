//выход из приложения при нажатии кнопки
//повесить на любой объект и задать клавишу
using UnityEngine;
using System.Collections;


public class ExitOnKey : MonoBehaviour {
	public KeyCode key = KeyCode.Escape;
	
	void Update () {
		if(Input.GetKey(key)) Application.Quit();
	}
}
