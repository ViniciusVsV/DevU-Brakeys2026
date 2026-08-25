using DG.Tweening;
using UnityEngine;

public class PersonBehaviour : MonoBehaviour
{
    //Cada pessoa vai ter uma mala e um passaporte aleatórios das listas
    //Cada pessoa aleatoriamente terá drogas ou não

    public void MoveToNextArea(Vector2 nextPoint)
    {
        transform.DOMove(nextPoint, 1);
    }
}
