using System.Collections.Generic; 
namespace OOPLab2;
// Реалізує окремий компаратор кіл за радіусом
public sealed class CircleRadiusComparer : IComparer<Circle> //Дозволяє передати власне правило сортування
{
    public int Compare(Circle? first, Circle? second) //Порівнює два кола
    {
        if (ReferenceEquals(first, second)) return 0; 
        if (first is null) return -1; 
        if (second is null) return 1;
        return first.Radius.CompareTo(second.Radius);
    }
}