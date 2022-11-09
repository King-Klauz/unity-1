using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class vida : MonoBehaviour
{
    
    float valorVida = 100;
    public Slider life;


    public void tomarDano(float dano){

        life.value = valorVida - dano;
        valorVida -= dano;
    }

}
