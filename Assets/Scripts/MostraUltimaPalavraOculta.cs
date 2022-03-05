using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Classe para exibir a ultima palavra oculta
/// decoberta, que foi salva na player pref e 
/// exibir em tela
/// </summary>
public class MostraUltimaPalavraOculta : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = PlayerPrefs.GetString("ultimaPalavraOculta");
    }
}
