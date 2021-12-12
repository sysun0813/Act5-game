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

    [SerializeField] GameObject[] characterSlots;

    public void SetCharacterList(int characterCount)
    {
        for(int i = 0; i < characterSlots.Length; i++)
        {
            if(i < characterCount)
            {
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
}
