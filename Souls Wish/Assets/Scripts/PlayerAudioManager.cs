using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    [Header("Clips")]
    public AudioClip walk;
    public AudioClip attack;
    public AudioClip shield;
    public AudioClip jump_roll;
    public AudioClip death;

    [Header("Source")]
    public AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        Audio_IdleWalk();
    }

    public void Audio_IdleWalk()
    {
        if (PlayerManager.instance._speed == 0)
        {
            if (source.clip == walk)
                source.Stop();
        } else if (PlayerManager.instance._speed == 1)
        {
            source.loop = true;
            if (source.clip != walk)
            {
                source.clip = walk;
                source.Play();
            } else if (source.clip == walk && !source.isPlaying)
            {
                source.Play();
            }
        }
    }
    public void Audio_Stop()
    {
        source.Stop();
    }

    public void Audio_Death()
    {
        source.loop = false;
        source.clip = death;
        source.Play();
        StartCoroutine(CDDuration(2f));
    }
    public IEnumerator CDDuration(float time)
    {
        yield return new WaitForSeconds(time);
        Audio_Stop();
    }

    public void Audio_Attack()
    {
        source.loop = false;
        source.clip = attack;
        source.Play();
    }

    public void Audio_Shield()
    {
        source.loop = false;
        source.clip = shield;
        source.Play();
    }

    public void Audio_JumpRoll()
    {
        source.loop = false;
        source.clip = jump_roll;
        source.Play();
    }


}
