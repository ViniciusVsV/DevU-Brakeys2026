using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenU : MonoBehaviour {
    [SerializeField] private GameObject PainelMainMenU;
    [SerializeField] private GameObject PainelOptions;
    public void LoadScenes(string Rscene){
        SceneManager.LoadScene(Rscene);
    }
    public void InOptions(){
        PainelMainMenU.SetActive(false);
        PainelOptions.SetActive(true);
    }
    public void OffOptions(){
        PainelMainMenU.SetActive(true);
        PainelOptions.SetActive(false);
    }
    public void Quit() {
        Debug.Log("bury the light deep with in");
        Application.Quit();   
}   }

//Eu sei exatamente oque está pensando sobre minha maneira de identar
//And... i dont care
//You are you and i'm me. 