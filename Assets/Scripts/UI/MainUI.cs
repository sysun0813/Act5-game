using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [SerializeField] Animator stageIndicatorAnim;

    [SerializeField] Text stageIndicator_StageNumText;

    [SerializeField] Text returnCountText;
    [SerializeField] Text playerNameText;
    [SerializeField] Text stageNumText;

    [SerializeField] HpBar hpBarPrefab;
    List<HpBar> hpBars;

    CharacterSlot[] characterSlots;

    private void Awake()
    {
        characterSlots = transform.Find("¿µ¿õUI").GetComponentsInChildren<CharacterSlot>();
        hpBars = new List<HpBar>();
    }

    public void SetCharacterList(List<Main_Character> playerCharacters)
    {
        for(int i = 0; i < characterSlots.Length; i++)
        {
            if(i < playerCharacters.Count)
            {
                characterSlots[i].SetData(playerCharacters[i]);
                characterSlots[i].gameObject.SetActive(true);
            }
            else
            {
                characterSlots[i].gameObject.SetActive(false);
            }
        }
    }

    public void IndicateStage(int stageNum)
    {
        stageNumText.text = $"Stage {stageNum}";

        stageIndicator_StageNumText.text = stageNum.ToString();
        stageIndicatorAnim.SetTrigger("OnStage");
    }

    public void MatchHpBar(Character character)
    {
        if(hpBars.Count < 1)
        {
            HpBar hpBar = Instantiate(hpBarPrefab, transform.Find("HpBars"));
            hpBar.target = character.transform;
            character.hpBar = hpBar;
        }
        else
        {
            HpBar hpBar = hpBars[0];
            hpBar.target = character.transform;
            character.hpBar = hpBar;
            hpBar.gameObject.SetActive(true);
            hpBars.Remove(hpBar);
        }
    }

    public void AddHpBar(HpBar hpBar)
    {
        hpBars.Add(hpBar);
    }
}
