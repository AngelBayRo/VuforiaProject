using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class pizarra : MonoBehaviour {
	public Text texto;
	public TextAsset PreguntasTxt;
	public TextAsset RespuestasTxt;
	public Button A;
	public Button B;
	public Button C;
	public AudioSource risa; //risa
	public AudioSource sad; //triste
	public AudioSource victory; //oh yeah
	public Text puntuacion;
	private const int maxpreguntas=15;
	private int[] respuestas = new int[maxpreguntas];
	private int count;
	private string[] preguntas = new string[maxpreguntas];
	private List<int> listAleatorio = new List<int> ();
	private bool intro;
	private bool resuelto;
	public Animator myAnimator;
	private bool resultado;
	private int puntos = 0;
	// Use this for initialization
	void Start () {
		puntos = 0;
		resultado = true;
		intro = false;
		resuelto = false;
		A.gameObject.SetActive (false);
		B.gameObject.SetActive (false);
		C.gameObject.SetActive (false);
		count = 0;
		string[] fs = PreguntasTxt.text.Split(';');
		int i=0;
		foreach (string line in fs)
		{
			preguntas [i] = line.ToString();
			i++;
		}
		string[] rs = RespuestasTxt.text.Split(';');
		i = 0;
		foreach (string line in rs)
		{
			respuestas [i] = int.Parse(line.ToString());
			i++;
		}
		int aux=0;
		while (listAleatorio.Count != 5) {
			System.Random rnd = new System.Random ();
			aux = rnd.Next (0, 15);
			if (!isOnList(aux)) {
				listAleatorio.Add (aux);
			}
		}
		texto.text = "Hola, soy MocDock, entrenemos tu cerebro, deberás contestar algunas preguntas de lógica, veamos que tal se te da, ahora pulsa para continuar";
	}

	bool isOnList(int e){
		for (int i = 0; i < listAleatorio.Count; i++) {
			if (listAleatorio [i] == e) {
				return true;
			}
		}
		return false;
	}
	// Update is called once per frame
	void Update () {
		if (count < 5) {
			if (intro == false) {
				if (Input.GetMouseButtonDown (0)) {
					intro = true;
				}
			} else {
				if (resuelto == false) {
					texto.text = preguntas [listAleatorio[count]];
					A.gameObject.SetActive (true);
					B.gameObject.SetActive (true);
					C.gameObject.SetActive (true);
					A.onClick.AddListener (ATaskOnClick);
					B.onClick.AddListener (BTaskOnClick);
					C.onClick.AddListener (CTaskOnClick);
				} else {
					A.gameObject.SetActive (false);
					B.gameObject.SetActive (false);
					C.gameObject.SetActive (false);
					if (Input.GetMouseButtonDown (0)) {
						if (resultado == true)
							puntos += 100;
						puntuacion.text = "Puntos: "+puntos.ToString();
						count++;
						resuelto = false;
					}
				}
			}
		} else {
			texto.text = "Fin de la prueba";
			A.gameObject.SetActive (false);
			B.gameObject.SetActive (false);
			C.gameObject.SetActive (false);	
			sad.Play();
		}
	}

	void ATaskOnClick(){
		if (1 == respuestas[listAleatorio[count]]) {
			texto.text = "Correcto!";
			victory.Play();
			resuelto = true;
			myAnimator.CrossFade("Celebrar",1);
			resultado = true;
		} else {
			texto.text = "Incorrecto!!";
			risa.Play();
			resuelto = true;
			myAnimator.CrossFade("Risa",1);
			resultado = false;
		}
	}
	void BTaskOnClick(){
		if (2 == respuestas[listAleatorio[count]]) {
			texto.text = "Correcto!";
			victory.Play();
			resuelto = true;
			myAnimator.CrossFade("Celebrar",1);
			resultado = true;
		}else {
			texto.text = "Incorrecto!!";
			risa.Play();
			resuelto = true;
			myAnimator.CrossFade("Risa",1);
			resultado = false;
		}
	}
	void CTaskOnClick(){
		if (3 == respuestas[listAleatorio[count]]) {
			texto.text = "Correcto!";
			victory.Play();
			resuelto = true;
			myAnimator.CrossFade("Celebrar",1);
			resultado = true;
		}else {
			texto.text = "Incorrecto!!!";
			risa.Play();
			resuelto = true;
			myAnimator.CrossFade("Risa",1);
			resultado = false;
		}
	}
}
