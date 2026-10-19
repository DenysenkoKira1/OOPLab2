using System.Collections;
namespace OOPLab2;
//Реалізує узагальнене бінарне дерево пошуку для порівнюваних класів
public sealed class BinarySearchTree<T> : IEnumerable<T> where T : class, IComparable<T> //Обмежує T посилальним типом із методом CompareTo
{
    private sealed class Node(T value) //Описує один вузол дерева
    {
        public T Value { get; } = value;
        public Node? Left { get; set; }
        public Node? Right { get; set; }
    }
    private Node? _root;
    public int Count { get; private set; }
    public void Add(T value) //Додає значення в дерево
    {
        ArgumentNullException.ThrowIfNull(value);
        if (_root is null)
        {
            _root = new Node(value);
            Count++;
            return;
        }
        Node current = _root; //Починає пошук місця від кореня
        while (true) //Повторює пошук, доки не знайде вільне місце
        {
            int comparison = value.CompareTo(current.Value); //Порівнює нове значення з поточним вузлом
            if (comparison == 0) //Перевіряє повторення ключа
                throw new ArgumentException("Елемент із таким ключем порівняння вже є в дереві.", nameof(value));
            if (comparison < 0) //Визначає, чи треба перейти ліворуч
            {
                if (current.Left is null) //Перевіряє, чи ліве місце вільне
                {
                    current.Left = new Node(value); 
                    Count++; 
                    return; 
                }
                current = current.Left; 
            }
            else //Обробляє значення, більше за поточне
            {
                if (current.Right is null) //Перевіряє, чи праве місце вільне
                {
                    current.Right = new Node(value);
                    Count++;
                    return;
                }
                current = current.Right;
            }
        }
    }
    public T? Find(T value) //Шукає значення в дереві
    {
        ArgumentNullException.ThrowIfNull(value);
        Node? current = _root;
        while (current is not null) //Продовжує пошук, поки є вузол
        {
            int comparison = value.CompareTo(current.Value);
            if (comparison == 0) return current.Value;
            current = comparison < 0 ? current.Left : current.Right;
        }
        return null;
    }
    public IEnumerator<T> GetEnumerator() => new PreOrderEnumerator(_root); //Створює власний узагальнений ітератор
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator(); //Повертає той самий ітератор через неузагальнений інтерфейс
    //Реалізує прямий обхід: вузол, ліве піддерево, праве піддерево
    private sealed class PreOrderEnumerator : IEnumerator<T> //Описує власний ітератор дерева
    {
        private readonly Node? _root;
        private readonly Stack<Node> _pending = new();
        private Node? _current;
        public PreOrderEnumerator(Node? root) //Створює ітератор для заданого кореня
        {
            _root = root;
            if (root is not null) //Перевіряє, чи дерево містить корінь.
                _pending.Push(root);
        }
        public T Current => _current?.Value ?? throw new InvalidOperationException("Ітератор не перебуває на елементі.");
        object IEnumerator.Current => Current;
        public bool MoveNext() //Переходить до наступного вузла
        {
            if (_pending.Count == 0) //Перевіряє, чи залишилися вузли
            {
                _current = null;
                return false;
            }
            _current = _pending.Pop(); //Бере наступний вузол зі стеку
            if (_current.Right is not null) //Перевіряє існування правого нащадка
                _pending.Push(_current.Right); //Додає правого нащадка першим
            if (_current.Left is not null) //Перевіряє існування лівого нащадка
                _pending.Push(_current.Left); //Додає лівого нащадка після правого
            return true;
        }
        public void Reset() //Повертає ітератор на початок обходу
        {
            _pending.Clear();
            _current = null;
            if (_root is not null) //Перевіряє, чи дерево не порожнє
                _pending.Push(_root);
        }
        public void Dispose() { } //Не звільняє ресурсів, бо дерево їх не використовує
    }
}