using System.Collections;
using TMPro;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject standart;
    [SerializeField] GameObject fast;
    [SerializeField] GameObject heavy;
    [SerializeField] GameObject boss;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float cooldown = 0.8f;

    private bool isSpawning = false;
    bool canStart = false;
    int wave = 0;
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] float[] moneyPerWave;

    void Update()
    {
        GameObject[] zombs = GameObject.FindGameObjectsWithTag("Zombie");

        if (zombs.Length == 0 && !isSpawning && wave < 10)
        {
            FinanceManager.money += moneyPerWave[wave];
            canStart = true;
            wave++;
        }

        if (canStart)
        {
            switch (wave){
                case 1:
                    StartCoroutine(Wave1());
                    break;
                case 2:
                    StartCoroutine(Wave2());
                    break;
                case 3:
                    StartCoroutine(Wave3());
                    break;
                case 4:
                    StartCoroutine(Wave4());
                    break;
                case 5:
                    StartCoroutine(Wave5());
                    break;
                case 6:
                    StartCoroutine(Wave6());
                    break;
                case 7:
                    StartCoroutine(Wave7());
                    break;
                case 8:
                    StartCoroutine(Wave8());
                    break;
                case 9:
                    StartCoroutine(Wave9());
                    break;
                case 10:
                    StartCoroutine(Wave10());
                    break;
            }
            canStart = false;
        }

        waveText.text = "Волна: " + wave.ToString();
    }

    IEnumerator SpawnRoutine(GameObject obj,int times)
    {
        isSpawning = true;

        for (int i = 0; i < times; i++)
        {
            Vector3 spawnPos = new Vector3(spawnPoint.position.x, obj.transform.position.y, spawnPoint.position.z);
            Instantiate(obj, spawnPos, Quaternion.identity);

            yield return new WaitForSeconds(cooldown);
        }

        isSpawning = false;
    }

    IEnumerator Wave1()
    {
        StartCoroutine(SpawnRoutine(standart, 4));
        yield return new WaitForSeconds(0f);
    }
    IEnumerator Wave2()
    {
        StartCoroutine(SpawnRoutine(standart, 6));
        yield return new WaitForSeconds(0f);
    }
    IEnumerator Wave3()
    {
        StartCoroutine(SpawnRoutine(standart, 8));
        yield return new WaitForSeconds(0f);
    }
    IEnumerator Wave4()
    {
        StartCoroutine(SpawnRoutine(fast, 3));
        yield return new WaitForSeconds(cooldown * 3);
        StartCoroutine(SpawnRoutine(standart, 7));
    }
    IEnumerator Wave5()
    {
        StartCoroutine(SpawnRoutine(fast, 5));
        yield return new WaitForSeconds(cooldown * 5);
        StartCoroutine(SpawnRoutine(standart, 9));
    }
    IEnumerator Wave6()
    {
        StartCoroutine(SpawnRoutine(fast, 7));
        yield return new WaitForSeconds(cooldown * 7);
        StartCoroutine(SpawnRoutine(standart, 11));
    }
    IEnumerator Wave7()
    {
        StartCoroutine(SpawnRoutine(fast, 3));
        yield return new WaitForSeconds(cooldown * 3);
        StartCoroutine(SpawnRoutine(standart, 6));
        yield return new WaitForSeconds(cooldown * 6);
        StartCoroutine(SpawnRoutine(heavy, 8));
    }
    IEnumerator Wave8()
    {
        StartCoroutine(SpawnRoutine(fast, 4));
        yield return new WaitForSeconds(cooldown * 4);
        StartCoroutine(SpawnRoutine(standart, 8));
        yield return new WaitForSeconds(cooldown * 8);
        StartCoroutine(SpawnRoutine(heavy, 11));
    }
    IEnumerator Wave9()
    {
        StartCoroutine(SpawnRoutine(fast, 6));
        yield return new WaitForSeconds(cooldown * 6);
        StartCoroutine(SpawnRoutine(standart, 10));
        yield return new WaitForSeconds(cooldown * 10);
        StartCoroutine(SpawnRoutine(heavy, 14));
    }
    IEnumerator Wave10()
    {
        StartCoroutine(SpawnRoutine(fast, 5));
        yield return new WaitForSeconds(cooldown * 5);
        StartCoroutine(SpawnRoutine(standart, 9));
        yield return new WaitForSeconds(cooldown * 9);
        StartCoroutine(SpawnRoutine(heavy, 12));
        yield return new WaitForSeconds(cooldown * 12);
        StartCoroutine(SpawnRoutine(boss, 1));
    }
}
