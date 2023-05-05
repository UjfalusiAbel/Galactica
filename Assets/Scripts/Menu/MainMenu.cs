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
    private Slot[] slots;
    [SerializeField]
    private GameObject[] deleteConfirms;
    [SerializeField]
    private GameObject[] deleteButtons;

    public void EnterSlots()
    {
        menuAnim.SetBool("_isSlot", true);
    }

    public void EnterMain()
    {
        menuAnim.SetBool("_isSlot", false);
    }

    public void LoadSaveSlot(int num)
    {
        if (slots[num].CheckSave())
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

    public void LoadDeleteConfirm(int num)
    {
        slots[num].gameObject.SetActive(false);
        deleteConfirms[num].SetActive(true);
        deleteButtons[num].SetActive(false);
    }

    public void DeactivateDeleteConfirm(int num)
    {
        slots[num].gameObject.SetActive(true);
        deleteConfirms[num].SetActive(false);
        deleteButtons[num].SetActive(true);
    }

    public void DeleteSave(int num)
    {
        FileHandler.Singleton.DeleteData(num);
        slots[num].gameObject.SetActive(true);
        deleteConfirms[num].SetActive(false);
        slots[num].SetText = "Szabad";
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
