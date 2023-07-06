using System.Collections;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _roundText;
    [SerializeField]
    private GameObject _leftButtons;
    [SerializeField]
    private TMP_Text _numRound;
    [SerializeField]
    private TMP_Text _numRound2;
    [SerializeField]
    private Buff[] _buff;

    public float _round = 1;

    public void NewRound()
    {
        _round++;
        StartCoroutine("NewRoundEvent");
    }

    public void NewGame()
    {
        SceneManager.LoadScene(0);
    }

    private IEnumerator NewRoundEvent()
    {
        yield return new WaitForSeconds(2f);

        _numRound.text = _numRound2.text = _round.ToString();
        _roundText.SetActive(true);

        yield return new WaitForSeconds(1);

        _roundText.SetActive(false);
        _leftButtons.SetActive(true);

        foreach (var buff in _buff)
        {
            buff.NewRoundBuff();
            buff.CancelBuff();

            if (buff._countSlot1.text != "" && Convert.ToInt32(buff._countSlot1.text) > 1)
                buff._countSlot1.text = (Convert.ToInt32(buff._countSlot1.text) - 1).ToString();

            if (buff._countSlot2.text != "" && Convert.ToInt32(buff._countSlot2.text) > 1)
                buff._countSlot2.text = (Convert.ToInt32(buff._countSlot2.text) - 1).ToString();
        }
    }
}
