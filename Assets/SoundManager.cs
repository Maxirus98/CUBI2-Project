using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [Header("Sound Fx when nearby")]
    public AudioClip gunShootFx;
    public AudioClip takeWaterFx;
    public AudioClip takeSandFx;
    public AudioClip sandmanAttackFx;
    public AudioClip enemyHitFx;
    public AudioClip stepFx;
    public AudioClip attackEnemy1Fx;
    public AudioClip enemyStepFx;
    public AudioClip towerBuildingFx;
    public AudioClip towerEndBuildFx;
    public AudioClip towerDestroyedFx;
    public AudioClip towerShootFx;
    public AudioClip optionChangeMenuFx;
    public AudioClip cleanDestroyedTowerFx;

    [Header("Menu sounds")]
    public AudioClip selectionMenuFx;

    [Header("Sound Fx Ambiant")]
    public AudioClip windFx;
    public AudioClip waveStartSound;
    public AudioClip winSound;
    public AudioClip lostSound;
    public AudioClip waveEndSound;
    public AudioClip animalSound;
    public AudioClip enemyNearPortalSound;

    [Header("Music")]
    public AudioClip menuMusic;
    public AudioClip waveMusic;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
}
