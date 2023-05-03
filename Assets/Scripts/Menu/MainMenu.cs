using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Animator menuAnim;
    [SerializeField]
    private GameObject main;
    [SerializeField]
    private GameObject saves;
    [SerializeField]
    private Slot[] slot;

    public void EnterSlots()
    {
        saves.SetActive(true);
        menuAnim.SetBool("_isSlot", true);
        Invoke(nameof(TimedMainInactivate), 1f);
    }

    public void EnterMain()
    {
        main.SetActive(true);
        menuAnim.SetBool("_isSlot", false);
        Invoke(nameof(TimedSavesInactivate), 1f);
    }

    public void LoadSaveSlot(int num)
    {
        if (slot[num].CheckSave())
        {
            FileHandler.Singleton.LoadSaveData(num);
        }
        else
        {
            FileHandler.Singleton.HasSaveData = false;
        }
        FileHandler.Singleton.SetSlotNumber = num;
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void TimedMainInactivate()
    {
        main.SetActive(false);
    }

    private void TimedSavesInactivate()
    {
        saves.SetActive(false);
    }
}
