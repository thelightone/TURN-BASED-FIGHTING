using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BuffManager : MonoBehaviour
{
    [SerializeField]
    private List<BuffConfig> _BuffList;

    public Dictionary<PlayerController, Dictionary <BuffConfig, float>> dict = new Dictionary<PlayerController, Dictionary<BuffConfig, float>>();

    public void Buff(PlayerController controller)
    {
        if (!dict.ContainsKey(controller))
        {
            dict.Add(controller, new Dictionary<BuffConfig, float>());
        }

        if (dict[controller].Count < 2)
        {
            BuffConfig newBuff;

            do
            {
                 newBuff = _BuffList[Random.Range(0, _BuffList.Count)];
            }
            while (dict[controller].ContainsKey(newBuff));

            dict[controller].Add(newBuff, newBuff.duration);

            Debug.Log(controller);
            foreach (var l in dict[controller]) { Debug.Log(l.Key); Debug.Log(l.Value); }

            newBuff._player = controller;

            controller.health += newBuff.healthChange;
            controller.armor += newBuff.armorCnahge;
            controller.vamp += newBuff.vampChange;

            controller._damageHealth *= newBuff.damageHealthChange;
            controller._damageArmor += newBuff.damageArmorChange;
            controller._damageVamp += newBuff.damageVampChange;

            controller.Buff();
        }
    }

    public void BuffDurationDecrease()
    {
        foreach (var controller in dict)
        {            
            foreach (var buff in controller.Value.ToList())
            {
                controller.Value[buff.Key]--;

                if (controller.Value[buff.Key]<1)
                {
                    controller.Value.Remove(buff.Key);
                    RemoveBuff(buff.Key);
                }
            }
        }      
    }

    public void RemoveBuff(BuffConfig buff)
    {
        buff._player.health -= buff.healthChange;
        buff._player.armor -= buff.armorCnahge;
        buff._player.vamp -= buff.vampChange;

        buff._player._damageHealth /= buff.damageHealthChange;
        buff._player._damageArmor -= buff.damageArmorChange;
        buff._player._damageVamp -= buff.damageVampChange;

        buff._player.Buff();
    }

    public string BuffStatus(PlayerController controller)
    {
        if (dict.ContainsKey(controller))
        {
            StringBuilder buffs = new StringBuilder();
            foreach (var buff in dict[controller])
            {
                var nameBuff = buff.Key.ToString().Replace("(BuffConfig)", " ");
                buffs.Append(nameBuff + buff.Value + "\n");
            }
            return buffs.ToString();
        }
        return "";
    }
}
