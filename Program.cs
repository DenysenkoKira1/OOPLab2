using System.Collections; 
namespace OOPLab2;
internal static class Program 
{
    private static void Main() //Запускається першою після старту програми
    {
        //Шість прикладів відповідають кількості об'єктів
        Circle[] examples = //Створює масив початкових кіл
        [
            new("червоний", "чорний", 3),
            new("синій", "білий", 1),
            new("зелений", "сірий", 5), 
            new("жовтий", "чорний", 2), 
            new("фіолетовий", "білий", 4),
            new("помаранчевий", "сірий", 6)
        ]; 
        ShowCircle(examples[0]); //Показує методи класу для першого кола
        DemonstrateArray(examples);
        DemonstrateGenericList(examples);
        DemonstrateNonGenericList(examples);
        DemonstrateSorting(examples);
        DemonstrateTree(examples);
        //Перевіряє граничний випадок: радіус має бути додатним
        try
        {
            _ = new Circle("прозорий", "чорний", 0);
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Перевірка радіуса 0: коректно відхилено.\n");
        }
    }
    private static void ShowCircle(Circle circle) //Виводить дані та обчислення для кола
    {
        Console.WriteLine(circle);
        Console.WriteLine($"Площа: {circle.Area:F2}; довжина кола: {circle.Circumference:F2}\n");
    }
    private static void DemonstrateArray(Circle[] source) //Показує типові операції з масивом
    {
        Console.WriteLine("Масив (фіксований розмір; для додавання створюємо новий масив):");
        Circle[] circles = (Circle[])source.Clone();
        Console.WriteLine($"Кількість до додавання: {circles.Length}");
        circles = CircleCollections.Add(circles, new Circle("бірюзовий", "чорний", 7));
        Console.WriteLine($"Кількість після додавання: {circles.Length}");
        Circle oldCircle = circles[0];
        Circle updatedCircle = new("бордовий", "золотий", 3);
        circles[0] = updatedCircle;
        Console.WriteLine($"Оновлення: {oldCircle} → {updatedCircle}");
        Console.WriteLine($"Пошук радіуса 4: {CircleCollections.FindByRadius(circles, 4)}");
        circles = CircleCollections.RemoveByRadius(circles, 7);
        Console.WriteLine($"Кількість після видалення: {circles.Length}");
        PrintCircles(circles);
        Console.WriteLine();
    }
    private static void DemonstrateGenericList(Circle[] source) //Показує типові операції з List<Circle>.
    {
        Console.WriteLine("List<Circle> (узагальнена колекція):");
        List<Circle> circles = [.. source];
        Console.WriteLine($"Кількість до додавання: {circles.Count}");
        circles.Add(new Circle("бірюзовий", "чорний", 7));
        Console.WriteLine($"Кількість після додавання: {circles.Count}");
        Circle oldCircle = circles[0];
        Circle updatedCircle = new("бордовий", "золотий", 3);
        circles[0] = updatedCircle;
        Console.WriteLine($"Оновлення: {oldCircle} → {updatedCircle}");
        Console.WriteLine($"Пошук радіуса 4: {circles.Find(circle => circle.Radius == 4)}");
        circles.RemoveAll(circle => circle.Radius == 7);
        Console.WriteLine($"Кількість після видалення: {circles.Count}");
        PrintCircles(circles);
        Console.WriteLine();
    }
    private static void DemonstrateNonGenericList(Circle[] source) //Показує типові операції з ArrayList.
    {
        Console.WriteLine("ArrayList (неузагальнена колекція; елементи потрібно приводити до Circle):");
        ArrayList circles = new(source);
        Console.WriteLine($"Кількість до додавання: {circles.Count}");
        circles.Add(new Circle("бірюзовий", "чорний", 7));
        Console.WriteLine($"Кількість після додавання: {circles.Count}");
        Circle oldCircle = (Circle)circles[0]!;
        Circle updatedCircle = new("бордовий", "золотий", 3);
        circles[0] = updatedCircle;
        Console.WriteLine($"Оновлення: {oldCircle} → {updatedCircle}");
        Circle? found = null;
        foreach (Circle circle in circles) //Перебирає елементи з приведенням до Circle
        {
            if (circle.Radius == 4) //Перевіряє радіус поточного кола
            {
                found = circle;
                break;
            }
        }
        Console.WriteLine($"Пошук радіуса 4: {found}");
        circles.RemoveAt(circles.Count - 1);
        Console.WriteLine($"Кількість після видалення: {circles.Count}");
        foreach (Circle circle in circles) 
            Console.WriteLine($"  {circle}"); 
        Console.WriteLine();
    }
    private static void DemonstrateSorting(Circle[] source) //Показує сортування двома інтерфейсами
    {
        Console.WriteLine("Сортування за радіусом через IComparable<Circle> та IComparer<Circle>:");
        Circle[] byNaturalOrder = (Circle[])source.Clone();
        Array.Sort(byNaturalOrder);
        PrintCircles(byNaturalOrder); 
        List<Circle> byComparer = [.. source]; 
        byComparer.Sort(new CircleRadiusComparer());
        PrintCircles(byComparer);
        Console.WriteLine();
    }
    private static void DemonstrateTree(Circle[] source) //Показує додавання, пошук і обхід дерева
    {
        Console.WriteLine("Узагальнене бінарне дерево пошуку (власний обхід preorder):");
        BinarySearchTree<Circle> tree = new();
        foreach (Circle circle in source) 
            tree.Add(circle);
        Console.WriteLine("Радіуси у порядку обходу: " + string.Join(", ", tree.Select(circle => circle.Radius)));
        Console.WriteLine("Елементи дерева:");
        foreach (Circle circle in tree)
            Console.WriteLine($"  {circle}");
        try //Перевіряє спробу додати коло з повторним ключем
        {
            tree.Add(new Circle("дубль", "чорний", 3));
        }
        catch (ArgumentException) //Перехоплює повідомлення дерева про повторний ключ
        {
            Console.WriteLine("Коло з радіусом 3 не додано: радіус є унікальним ключем дерева.");
        }
        Console.WriteLine($"Пошук радіуса 5: {tree.Find(new Circle("пошук", "пошук", 5))}\n");
    }
    private static void PrintCircles(IEnumerable<Circle> circles) //Виводить будь-яку послідовність кіл
    {
        foreach (Circle circle in circles) 
            Console.WriteLine($"  {circle}");
    }
}