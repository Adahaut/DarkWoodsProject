using UnityEngine;

public class DW_SaveController : MonoBehaviour
{
    public DW_Class m_class;
    public DW_Class m_class2;
    public DW_Save save;
    // Start is called before the first frame update
    void Start()
    {
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Save()
    {
        for(int i = 0; i<DW_ClassController.Instance.classes.Count; i++)
        {
            CopyClassScriptable(save.data_class[i], DW_ClassController.Instance.classes[i]);
        }
    }

    public void Load()
    {
        for (int i = 0; i < DW_ClassController.Instance.classes.Count; i++)
        {
            CopyClassScriptable(DW_ClassController.Instance.classes[i], save.data_class[i]);
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
}
