using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] Image barHp;
    [SerializeField] Image barFuel;
    [SerializeField] GameObject panel_finish;
    private int HealthPoint;
    private int MaxHealthPoint = 100;

    private float Fuel;
    private int MaxFuel = 1000;

    [HideInInspector] public bool FuelConds = false;
    [HideInInspector] public int FuelCost;
    public void SetHP(int hp)
    {
        // Mengurangi atau menambah HealthPoint berdasarkan nilai hpChange
        this.HealthPoint += hp;
        // Pastikan HealthPoint tidak melebihi MaxHealthPoint atau kurang dari 0
        this.HealthPoint = Mathf.Clamp(this.HealthPoint, 0, MaxHealthPoint);
    }
    public void SetFuel(int fuel)
    {
        // Mengurangi atau menambah HealthPoint berdasarkan nilai hpChange
        this.Fuel += fuel;
        // Pastikan HealthPoint tidak melebihi MaxHealthPoint atau kurang dari 0
        this.Fuel = Mathf.Clamp(this.Fuel, 0, MaxFuel);
    }

    // Start is called before the first frame update
    void Start()
    {
        HealthPoint = MaxHealthPoint;
        Fuel = MaxFuel;
    }

    // Update is called once per frame
    void Update()
    {
        ManagerHP();
        ManagerFuel(FuelConds);

        if (Fuel == 0)
        {
            gameManager.isFuelFull = false;
        }
        else
        {
            gameManager.isFuelFull = true;
        }
    }
    void ManagerHP()
    {
        // Update the HP bar fill amount based on HealthPoint
        barHp.fillAmount = (float)HealthPoint / MaxHealthPoint;
        // Ensure HealthPoint does not go below 0
        HealthPoint = Mathf.Max(HealthPoint, 0);

        if (HealthPoint <= 0)
        {
            panel_finish.SetActive(true);
            Time.timeScale = 0;
        }
    }
    void ManagerFuel(bool isFuel)
    {
        // Update the HP bar fill amount based on Fuel
        barFuel.fillAmount = (float)Fuel / MaxFuel;
        // Ensure Fuel does not go below 0    
        Fuel = Mathf.Max(Fuel, 0);
        if (isFuel)
        {
            switch (FuelCost)
            {
                case 1:
                    Fuel -= 0.1f;
                    this.Fuel = Mathf.Clamp(this.Fuel, 0, MaxFuel);
                    break;
                case 2:
                    Fuel -= 0.2f;
                    this.Fuel = Mathf.Clamp(this.Fuel, 0, MaxFuel);
                    break;
            }
        }
    }
}
