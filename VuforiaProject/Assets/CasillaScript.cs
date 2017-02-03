using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CasillaScript : MonoBehaviour {

    private Vector2 position;
    public Text valor;
    public int n;
    private ArrayList values; //si da error intentar con solo 2 variables, una para el anterior y otra para el nuevo
    float tiempo;
    bool vacio;
    public Button prueba;
    public float rand;
    public Text aciertos;
    public int scene;

// Use this for initialization
    void Start ()
    {
        values = new ArrayList();
        int resultado = (int)Random.value * 2;
        n = 0;
        tiempo = Time.time;
        vacio = false;
        values.Add(resultado);
    }
	
	// Update is called once per frame
	void Update ()
    {
        int resultado = (int)Random.Range(0, 6);
        float lastTiempo = Time.time;

        int diferencia = (int)((lastTiempo - tiempo) % 60);

        if (!vacio)
        {
            if (diferencia >= rand)
            {
                valor.GetComponent<Text>().text = resultado.ToString();
                tiempo = lastTiempo;
                values.RemoveAt(0);
                values.Add(resultado.ToString());
                vacio = true;
            }
        }
        else
        {
            if (diferencia >= 1) { 
                valor.GetComponent<Text>().text = " ";
                vacio = false;
                tiempo = lastTiempo;
            }
        }

        prueba.onClick.AddListener(Pushed);

        if (aciertos.text == "0")
            UnityEngine.SceneManagement.SceneManager.LoadScene("n-back"+scene.ToString());
    }

    void Pushed() {
        if (valor.GetComponent<Text>().text == values[n].ToString() )
            aciertos.text = (int.Parse(aciertos.text) - 1).ToString();
    }
}
