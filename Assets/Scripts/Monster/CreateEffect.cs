using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEffect : MonoBehaviour {
    [SerializeField]
    Transform skillPos;
    [SerializeField]
    Transform AtackPosition;
    [SerializeField]
    float destryTime;

    public void CreateSkillEffect(string effectName)
    {

        EffectManager.Instance.CreateEffect(effectName, skillPos.position, destryTime);

    }

    public void CreateAtackEffect(string effectName)
    {
        EffectManager.Instance.CreateEffect(effectName,AtackPosition.position,destryTime);
    }
  
    public void CreateE(string name)
    {
        EffectManager.Instance.CreateEffect(name, transform.position, destryTime);

    }

    public void OnSe(string Name)
    {
        SoundManager.Instance.PlayeSE(Name);
    }
    
 }
