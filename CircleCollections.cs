namespace OOPLab2; 
// Містить допоміжні операції для роботи з масивом кіл
internal static class CircleCollections //Забороняє створювати екземпляри цього службового класу
{
    public static Circle[] Add(Circle[] circles, Circle circle) //Додає коло в новий масив
    {
        Circle[] result = new Circle[circles.Length + 1];
        Array.Copy(circles, result, circles.Length); 
        result[^1] = circle;
        return result;
    }
    public static Circle[] RemoveByRadius(Circle[] circles, double radius) //Видаляє перше коло із заданим радіусом
    {
        int index = Array.FindIndex(circles, circle => circle.Radius == radius); 
        if (index < 0) return (Circle[])circles.Clone();
        Circle[] result = new Circle[circles.Length - 1];
        Array.Copy(circles, 0, result, 0, index);
        Array.Copy(circles, index + 1, result, index, circles.Length - index - 1); 
        return result;
    }
    public static Circle? FindByRadius(Circle[] circles, double radius) => //Шукає перше коло із заданим радіусом
        Array.Find(circles, circle => circle.Radius == radius); 
}