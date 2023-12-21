using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DW_SaveController : MonoBehaviour
{
    [SerializeField] private List<DW_Class> data_class;
    public DW_Save save;
    // Start is called before the first frame update
    void Start()
    {
        Load();

    }

    private void OnDisable()
    {
        Save();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DW_ClassController.Instance.classes[0].currentHealth = 10;
        }

        for (int i = 0; i < DW_ClassController.Instance.classes.Count; i++)
        {
            if (data_class.Count < DW_ClassController.Instance.classes.Count)
            {
                data_class.Add(DW_ClassController.Instance.classes[i]);
            }
            else
                data_class[i] = DW_ClassController.Instance.classes[i];

        }
    }


    public void Save()
    {
        //JsonSave(DW_ClassController.Instance);
        for (int i = 0; i<DW_ClassController.Instance.classes.Count; i++)
        {
            CopyClassScriptable(save.data_class[i], DW_ClassController.Instance.classes[i]);
            if(data_class.Count < DW_ClassController.Instance.classes.Count)
            {
                data_class.Add(DW_ClassController.Instance.classes[i]);
            }
            else
                data_class[i] = DW_ClassController.Instance.classes[i];
            
        }
    }

    public void Load()
    {
        //JsonLoad(DW_ClassController.Instance);
        for (int i = 0; i < save.data_class.Count; i++)
        {
            CopyClassScriptable(DW_ClassController.Instance.classes[i], save.data_class[i]);
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            for (int j = 0; j < data_class.Count; j++)
            {
                if (data_class.Count > DW_ClassController.Instance.classes.Count)
                {
                    DW_ClassController.Instance.classes.Add(data_class[j]);
                }
                else
                    DW_ClassController.Instance.classes[j] =data_class[j];

            }

            
        }
    }

    private void CopyClassScriptable(DW_Class copy, DW_Class _class)
    {
        copy.className = _class.className;
        copy.classDescription = _class.classDescription;
        copy.classIcon = _class.classIcon;
        copy.classPassif = _class.classPassif;
        copy.classSkill = _class.classSkill;
        copy.currentHealth = _class.currentHealth;
        copy.maxHealth = _class.maxHealth;

        copy.currentAttackSpeed = _class.currentAttackSpeed;
        copy.minattackDamage = _class.minattackDamage;
        copy.maxattackDamage = _class.maxattackDamage;

        copy.shouldBeAggro = _class.shouldBeAggro;
        copy.minPercentDamage = _class.minPercentDamage;
        copy.maxPercentDamage = _class.maxPercentDamage;
        copy.specialSourceType = _class.specialSourceType;
        copy.specialSourceAmount = _class.specialSourceAmount;

        CopyInventoryScriptable(copy.slotLeft, _class.slotLeft);
        CopyInventoryScriptable(copy.slotRight, _class.slotRight);
    }

    private void CopyInventoryScriptable(So_ItemData copy, So_ItemData inventory)
    {
        copy.image = inventory.image;
        copy.item = inventory.item;
        copy.useAction = inventory.useAction;
        copy.numberOfItem = inventory.numberOfItem;
        copy._class = inventory._class;
        copy.consommable = inventory.consommable;
    }

    private void JsonSave(DW_ClassController _class)
    {
        string player = JsonUtility.ToJson(_class);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/PlayerData.json", player);

    }

    private void JsonLoad(DW_ClassController _class)
    {
        var inputString = File.ReadAllText(Application.persistentDataPath + "/PlayerData.json");

        JsonUtility.FromJsonOverwrite(inputString, _class);
        //JsonUtility.FromJsonOverwrite("player", _class);
    }
}
