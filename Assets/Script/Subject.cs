using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Subject : MonoBehaviour
{
    readonly List<Observer> observers = new();
    public void Attach(Observer observer)
    {
        observers.Add(observer);
    }
    public void Detach(Observer observer)
    {
        observers.Remove(observer);
    }

    public void NotifyAll()
    {
        foreach (Observer observer in observers)
        {
            observer.Notify(this);           
        }
    }
}
