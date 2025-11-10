using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;

public class PlayerController : MonoBehaviour
{
    public Target[] targetArray;
    public List<int> hpTargets;
    private int targetNum = 0;
    void Awake()
    {
        TargetHealthCheck();
    }

    void Start()
    {
        Debug.Log("Press Space to begin locking onto the targets.");
    }

    void Update()
    {
        PlayerLock();
    }

    string ListToString(List<int> list)
    {
        return string.Join(", ", list);
    }

    void TargetHealthCheck()
    {
        foreach (Target selectedTarget in targetArray)
        {
            int targetHp = selectedTarget.hp;
            hpTargets.Add(targetHp);
        }
        Debug.Log("List of Target HP values: " + ListToString(hpTargets));
        ListSort(hpTargets);
        TargetArraySort(targetArray);
        Debug.Log("Sorted HP of Targets: " + ListToString(hpTargets));
    }

    void ListSort(List<int> list)
    {
        int n = list.Count;
        for (int i = 1; i < n; i++)
        {
            int key = list[i];
            int j = i - 1;
            while (j >= 0 && list[j] > key)
            {
                list[j + 1] = list[j];
                j = j - 1;
            }
            list[j + 1] = key;
        }
    }

    void TargetArraySort(Target[] array)
    {
        int n = array.Length;
        for (int i = 1; i < n; i++)
        {
            Target key = array[i];
            int j = i - 1;
            while (j >= 0 && array[j].hp > key.hp)
            {
                array[j + 1] = array[j];
                j = j - 1;
            }
            array[j + 1] = key;
        }
        
    }

    void PlayerLock()
    {
        bool spacePressed = Input.GetKeyDown(KeyCode.Space);
        if (spacePressed == true)
        {
            Debug.Log("Locking on...");
            Target enemyTarget = targetArray[targetNum];
            int targetHealth = enemyTarget.GetComponent<Target>().hp;
            transform.LookAt(enemyTarget.transform.position);
            Debug.Log("Currently looking at target number " + (targetNum + 1));
            Debug.Log("This target currently has " + targetHealth + "hp");
            targetNum++;
            if (targetNum > 2)
            {
                targetNum = 0;
            }
        }
    }
}
