using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioManager : MonoBehaviour
{
    [Header("Clips")]
    public AudioClip walk;
    public AudioClip attack;
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
        Enemy_Audio_IdleWalk();
    }

    public void Enemy_Audio_IdleWalk()
    {
        if (EnemyManager.instance._speed == 0)
        {
            if (source.clip == walk)
                source.Stop();
        } else if (EnemyManager.instance._speed == 1)
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

    public void Enemy_Audio_Stop()
    {
        source.Stop();
    }

    public void Enemy_Adio_Death()
    {
        source.loop = false;
        source.clip = death;
        source.Play();
        StartCoroutine(Enemy_CDDuration(2f));
    }

    public IEnumerator Enemy_CDDuration(float time)
    {
        yield return new WaitForSeconds(time);
        Enemy_Audio_Stop();
    }

    public void Enemy_Audio_Attack()
    {
        source.loop = false;
        source.clip = attack;
        source.Play();
    }
}
