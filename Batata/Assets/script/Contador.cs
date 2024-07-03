using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Contador : MonoBehaviour
{
    
    int tick;
    public int hora;
    int minuto;
    int segundo;
    int dia;
    int tDia;
    int mes;
    public int ano;
    [SerializeField]
    TextMeshProUGUI tempo;
    [SerializeField]
    TextMeshProUGUI epoca;
    int tickLog = 250;
    public static bool isTimeFrozen = false;
    public string era;

    #region 
    public static Contador instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public void Start()
    {
        segundo = 0;
        minuto = 0;
        hora = 0;
        tick = 0;
        dia = 0;
        mes = 11;
        ano = 9;
        era = "PréHistórica";
        
    }        
    public void Update()
    {
        if (ano == 10)
        {
            era = "Medieval";
        }
        if (ano == 20)
        {
            era = "Contemporânea";
        }
        if (ano == 30)
        {
            era = "Moderna";
        }
        

        if (Input.GetKeyDown(KeyCode.G))
        {
            tickLog = (tickLog == 250) ? 25 : 250;
            Debug.Log("tickLog value: " + tickLog);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            isTimeFrozen = !isTimeFrozen;
            Debug.Log("Ticking: " + !isTimeFrozen);
        }

        if (!isTimeFrozen)
        {
            if (tick < tickLog)
            {
                tick++;
            }
            else
            {
                segundo++;
                tick = 0;
            }

            if (segundo == 1)
            {
                minuto++;
                segundo = 0;
            }

            if (minuto == 1)
            {
                hora++;
                minuto = 0;
            }

            if (hora == 1)
            {
                dia++;
                tDia++;
                hora = 0;
            }

            int diasNoMes = GetDiasNoMes(mes, ano);

            if (dia > diasNoMes)
            {
                dia = 1;
                mes++;
            }

            if (mes > 12)
            {
                mes = 1;
                ano++;
            }

            string formattedHora = hora.ToString("D2");
            string formattedMinuto = minuto.ToString("D2");
            string formattedSegundo = segundo.ToString("D2");
            string formattedDia = dia.ToString("D2");
            string formattedMes = mes.ToString("D2");
            string formattedAno = ano.ToString("D2");
            //:{formattedHora}:{formattedMinuto}:{formattedSegundo}
            tempo.text = $"O tempo é {formattedAno}:{formattedMes}:{formattedDia}";
            epoca.text = $"Era {era}";
        }
    }

    int GetDiasNoMes(int mes, int ano)
    {
        switch (mes)
        {
            case 1: // Janeiro
            case 3: // Março
            case 5: // Maio
            case 7: // Julho
            case 8: // Agosto
            case 10: // Outubro
            case 12: // Dezembro
                return 31;
            case 4: // Abril
            case 6: // Junho
            case 9: // Setembro
            case 11: // Novembro
                return 30;
            case 2: // Fevereiro
                return IsLeapYear(ano) ? 29 : 28;
            default:
                throw new System.ArgumentOutOfRangeException("Mês inválido");
        }
    }

    bool IsLeapYear(int ano)
    {
        return (ano % 4 == 0) && (ano % 100 != 0 || ano % 400 == 0);
    }

    




}