namespace OOPLab2;
//Описує коло з параметрами
public sealed class Circle : IComparable<Circle> //Дозволяє порівнювати кола для сортування
{
    public string FillColor { get; } 
    public string OutlineColor { get; }
    public double Radius { get; }
    public double Area => Math.PI * Radius * Radius; //Обчислює площу за формулою πr²
    public double Circumference => 2 * Math.PI * Radius; //Обчислює довжину кола за формулою 2πr
    public Circle(string fillColor, string outlineColor, double radius) //Створює новий об'єкт кола
    {
        if (string.IsNullOrWhiteSpace(fillColor)) //Перевіряє, чи заданий колір заповнення
            throw new ArgumentException("Колір заповнення не може бути порожнім.", nameof(fillColor));
        if (string.IsNullOrWhiteSpace(outlineColor)) //Перевіряє, чи заданий колір контуру
            throw new ArgumentException("Колір контуру не може бути порожнім.", nameof(outlineColor));
        if (radius <= 0 || double.IsNaN(radius) || double.IsInfinity(radius)) //Перевіряє допустимість радіуса
            throw new ArgumentOutOfRangeException(nameof(radius), "Радіус має бути скінченним додатним числом.");
        FillColor = fillColor;
        OutlineColor = outlineColor;
        Radius = radius;
    }
    // Визначає природний порядок кіл за радіусом
    public int CompareTo(Circle? other) => other is null ? 1 : Radius.CompareTo(other.Radius); // Порівнює радіус поточного та іншого кола
    public override string ToString() => //Повертає текстове подання кола
        $"Коло: заповнення={FillColor}, контур={OutlineColor}, радіус={Radius}"; 
}