// using System;
// using System.Collections.Generic;
// using DG.Tweening;
// using Ink.Runtime;
// using TMPro;
// using UnityEngine;

// namespace DialogueSystem
// {
//     public class ChoiceEffects : MonoBehaviour
//     {
//         [SerializeField] private DialogueData dialogueData;

//         [SerializeField] private GameObject[] choiceObjects;
//         private RectTransform[] choiceTransforms;
//         private TextMeshProUGUI[] choiceTexts;

//         private Sequence enableSequence;
//         private Sequence disableSequence;

//         private void Awake()
//         {
//             choiceTransforms = new RectTransform[choiceObjects.Length];
//             choiceTexts = new TextMeshProUGUI[choiceObjects.Length];

//             for (int i = 0; i < choiceObjects.Length; i++)
//             {
//                 choiceTransforms[i] = choiceObjects[i].GetComponent<RectTransform>();
//                 choiceTexts[i] = choiceObjects[i].GetComponentInChildren<TextMeshProUGUI>();
//             }
//         }

//         public void EnableChoices(List<Choice> choices, Action onFinish)
//         {
//             if (choices.Count > choiceObjects.Length)
//                 Debug.LogWarning("Há mais escolhas do que botões disponíveis na UI!");

//             disableSequence.Kill();
//             enableSequence = DOTween.Sequence();

//             for (int i = 0; i < choices.Count; i++)
//             {
//                 int index = i;

//                 choiceTransforms[index].localScale = Vector3.one * 0.8f;
//                 choiceTexts[index].text = choices[index].text;

//                 Tween tween = choiceTransforms[index]
//                     .DOScale(Vector3.one, 0.3f)
//                     .SetEase(Ease.OutExpo)
//                     .OnStart(() =>
//                     {
//                         choiceObjects[index].SetActive(true);
//                     });

//                 // Insere na sequência com delay
//                 enableSequence.Insert(index * 0.1f, tween);
//             }

//             enableSequence.OnComplete(() => { onFinish?.Invoke(); });
//         }

//         public void DisableChoices(Action onFinish)
//         {
//             enableSequence.Kill();
//             disableSequence = DOTween.Sequence();

//             for (int i = 0; i < choiceObjects.Length; i++)
//             {
//                 int index = i;

//                 Tween tween = choiceTransforms[index]
//                     .DOScale(Vector3.zero, 0.1f)
//                     .SetEase(Ease.Linear)
//                     .OnComplete(() =>
//                     {
//                         choiceObjects[index].SetActive(false);
//                     });

//                 disableSequence.Join(tween);
//             }

//             disableSequence.OnComplete(() => { onFinish?.Invoke(); });
//         }
//     }
// }