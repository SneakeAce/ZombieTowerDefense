using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowUnitsHiringView : MonoBehaviour
{
    [SerializeField] private List<Button> _hireUnitButtons = new List<Button>();

    public List<Button> HireUnitButtons { get => _hireUnitButtons; }
}
