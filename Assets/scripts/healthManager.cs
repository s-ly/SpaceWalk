// Менеджер здоровья игрока

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class healthManager : MonoBehaviour
{
    int healthPlayer; // кол-во здоровья игрока
    [SerializeField] private int bulletDamage; // урон от пули
    [SerializeField] private TextMeshProUGUI textHealthPlayer; // текст кол-ва здоровья игрока
    [SerializeField] private GameObject DamageRedImage; // красная картинка повреждения

    public int healthPlayerTEMP; // кол-во здоровья игрока (для вычислений)

    // менеджер игры
    [SerializeField] private GameObject GameManager;
    private GameManager script_GameManager;
    public BatteryManager SCRIPT_BatteryManager;



    // Start is called before the first frame update
    void Start()
    {
        healthPlayer = ProgressManager.Instance.YandexDataOBJ.DATA_player_health;
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

    // Урон игрока. Пуля попала в игрока
    public void Damage()
    {
        int compensation_protective_field = 0;
        compensation_protective_field = SCRIPT_BatteryManager.ActivationProtectiveField(bulletDamage);
        healthPlayerTEMP += compensation_protective_field; // компенсация защитного поля
        healthPlayerTEMP -= bulletDamage; // вычитаем урон
        if (healthPlayerTEMP < 0) healthPlayerTEMP = 0; // что-бы не уходить в минус
        UpdateUIHealthPlayer();


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

    public void healthAdd() {
        int aid_kit_size = 10; // емкость балона
        for (int i = 0; i < aid_kit_size; i++) {
            if (healthPlayerTEMP < healthPlayer) {
                healthPlayerTEMP++;
            }
        }
        UpdateUIHealthPlayer();
    }


}
