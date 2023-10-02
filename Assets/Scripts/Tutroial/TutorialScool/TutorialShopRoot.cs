using System;
using System.Collections;
using UnityEngine;

public class TutorialShopRoot : MonoBehaviour
{
    private const string TutorialSchoolKey = "TutorialSchool";

    [SerializeField] private TutorialBehaviour _tutorialBehaviour;
    [SerializeField] private MoveTutorial _moveTutorial;
    [SerializeField] private TutorialAnalytics _tutorialAnalytics;

    public event Action Finished;

    public bool Completed => PlayerPrefs.HasKey(TutorialSchoolKey);

    private void OnEnable()
    {
        if (Completed)
            return;

        _tutorialBehaviour.SequanceStepsEnded += OnTutorialCompleted;
        _moveTutorial.Enable();
        _tutorialBehaviour.Enable();
        _tutorialAnalytics.Enable();
    }

    private void OnDisable()
    {
        if (Completed)
            return;

        _tutorialBehaviour.SequanceStepsEnded -= OnTutorialCompleted;
        _tutorialBehaviour.Disable();
        _tutorialAnalytics.Disable();
    }

    private void Start()
    {
        if (Completed)
            return;

        StartCoroutine(StartWithDelay(0.5f, () =>
        {
            _tutorialBehaviour.Initialize(onInitialized: () =>
            {
                var sequance = _tutorialBehaviour.CreateTutorialSequenceSteps();
                sequance.Execute();
            });
        }));
    }

    private void OnTutorialCompleted()
    {
        PlayerPrefs.SetInt(TutorialSchoolKey, 1);
        _tutorialBehaviour.SequanceStepsEnded -= OnTutorialCompleted;
        _tutorialBehaviour.Disable();
        _tutorialAnalytics.Disable();
        Finished?.Invoke();
    }

    private IEnumerator StartWithDelay(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }
}