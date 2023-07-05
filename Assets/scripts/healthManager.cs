// Менеджер здоровья игрока

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class healthManager : MonoBehaviour
{
    [SerializeField] public int healthPlayer; // кол-во здоровья игрока
    [SerializeField] private int bulletDamage; // урон от пули
    [SerializeField] private TextMeshProUGUI textHealthPlayer; // текст кол-ва здоровья игрока
    [SerializeField] private GameObject DamageRedImage; // красная картинка повреждения

    public int healthPlayerTEMP; // кол-во здоровья игрока (для вычислений)

    // менеджер игры
    [SerializeField] private GameObject GameManager;
    private GameManager script_GameManager;



    // Start is called before the first frame update
    void Start()
    {
        healthPlayerRestart();        
        DamageRedImage.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);

        script_GameManager = GameManager.GetComponent<GameManager>(); // менеджер игры

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Обновляет данные игрока на экране (здоровье)
    public void UpdateUIHealthPlayer()
    {
        textHealthPlayer.text = healthPlayerTEMP.ToString();
    }

    // Урон игрока
    public void Damage()
    {
        healthPlayerTEMP -= bulletDamage; // вычитаем урон
        if (healthPlayerTEMP < 0) healthPlayerTEMP = 0; // что-бы не уходить в минус
        UpdateUIHealthPlayer();
        //Debug.Log("Пуля попала в игрока");

        



        // кончилось здоровье
        if (healthPlayerTEMP <= 0)
        {            
            script_GameManager.GameOwer(); // Загрузка GAME OVER
        }
    }

    // рестарт здоровья игрока
    public void healthPlayerRestart()
    {
        healthPlayerTEMP = healthPlayer;
        UpdateUIHealthPlayer();
    }

    //private void DamageIndicator()
    //{
    //    float timerDamageRedImage = 1f;
        
    //    if (timerDamageRedImage > 0)
    //    {
    //        timerDamageRedImage -= Time.deltaTime;
    //    }
    //    else
    //}


}
