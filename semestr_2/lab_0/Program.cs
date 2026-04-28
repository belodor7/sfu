    using System.Globalization;
        public class Student
        {
        public string LastName;
        public string FirstName;
        public DateTime BirthDate;
        public int RecordNumber;

        public Student(string lastName, string firstName, DateTime birthDate, int recordNumber)
        {
            LastName = lastName;
            FirstName = firstName;
            BirthDate = birthDate.Date;
            RecordNumber = recordNumber;
        }

        public override string ToString()
        {
            return "#" + RecordNumber + "  " + LastName + "  " + FirstName + "  " + BirthDate.ToString("dd.MM.yyyy");
        }
    }

    public enum SortField
    {
        RecordNumber,
        LastName,
        FirstName,
        BirthDate
    }

    public class StudentGroup
    {
        private List<Student> students = new List<Student>();

        public string Name;

        public StudentGroup(string name)
        {
            Name = name;
        }

        public int Count
        {
            get { return students.Count; }
        }

        public Student GetAt(int index)
        {
            return students[index];
        }

        public Student GetByRecordNumber(int recordNumber)
        {
            int i = 0;
            while (i < students.Count)
            {
                if (students[i].RecordNumber == recordNumber)
                {
                    return students[i];
                }
                i++;
            }
            throw new Exception("Запись с номером " + recordNumber + " не найдена.");
        }

        public void Add(Student s)
        {
            if (s == null) throw new Exception("Нельзя добавить null.");

            int i = 0;
            while (i < students.Count)
            {
                if (students[i].RecordNumber == s.RecordNumber)
                {
                    throw new Exception("Номер записи " + s.RecordNumber + " уже существует.");
                }
                i++;
            }

            students.Add(s);
        }

        public bool RemoveAt(int index)
        {
            if (index < 0) return false;
            if (index >= students.Count) return false;

            students.RemoveAt(index);
            return true;
        }

        public bool RemoveByRecordNumber(int recordNumber)
        {
            int i = 0;
            while (i < students.Count)
            {
                if (students[i].RecordNumber == recordNumber)
                {
                    students.RemoveAt(i);
                    return true;
                }
                i++;
            }
            return false;
        }

        public List<Student> FindByLastName(string lastName)
        {
            List<Student> result = new List<Student>();
            if (lastName == null) return result;

            int i = 0;
            while (i < students.Count)
            {
                string a = students[i].LastName;
                if (a != null && a.ToLower() == lastName.ToLower())
                {
                    result.Add(students[i]);
                }
                i++;
            }
            return result;
        }

        public List<Student> FindByFirstName(string firstName)
        {
            List<Student> result = new List<Student>();
            if (firstName == null) return result;

            int i = 0;
            while (i < students.Count)
            {
                string a = students[i].FirstName;
                if (a != null && a.ToLower() == firstName.ToLower())
                {
                    result.Add(students[i]);
                }
                i++;
            }
            return result;
        }

        public List<Student> FindByBirthDate(DateTime date)
        {
            List<Student> result = new List<Student>();
            DateTime d = date.Date;

            int i = 0;
            while (i < students.Count)
            {
                if (students[i].BirthDate == d)
                {
                    result.Add(students[i]);
                }
                i++;
            }
            return result;
        }

        public void Sort(SortField field, bool ascending)
        {
            int n = students.Count;
            int i = 0;
            while (i < n - 1)
            {
                int j = 0;
                while (j < n - 1 - i)
                {
                    Student a = students[j];
                    Student b = students[j + 1];

                    int cmp = Compare(a, b, field);

                    bool needSwap = false;
                    if (ascending)
                    {
                        if (cmp > 0) needSwap = true;
                    }
                    else
                    {
                        if (cmp < 0) needSwap = true;
                    }

                    if (needSwap)
                    {
                        students[j] = b;
                        students[j + 1] = a;
                    }

                    j++;
                }
                i++;
            }
        }

        private int Compare(Student a, Student b, SortField field)
        {
            if (field == SortField.RecordNumber)
            {
                if (a.RecordNumber < b.RecordNumber) return -1;
                if (a.RecordNumber > b.RecordNumber) return 1;
                return 0;
            }

            if (field == SortField.LastName)
            {
                return CompareStrings(a.LastName, b.LastName);
            }

            if (field == SortField.FirstName)
            {
                return CompareStrings(a.FirstName, b.FirstName);
            }

            if (a.BirthDate < b.BirthDate) return -1;
            if (a.BirthDate > b.BirthDate) return 1;
            return 0;
        }

        private int CompareStrings(string x, string y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            string a = x.ToLower();
            string b = y.ToLower();

            int i = 0;
            int min = a.Length;
            if (b.Length < min) min = b.Length;

            while (i < min)
            {
                if (a[i] < b[i]) return -1;
                if (a[i] > b[i]) return 1;
                i++;
            }

            if (a.Length < b.Length) return -1;
            if (a.Length > b.Length) return 1;
            return 0;
        }

        public IEnumerator<Student> GetEnumerator()
        {
            return students.GetEnumerator();
        }
    }

    class Program
    {
        static DateTime D(string s)
        {
            return DateTime.ParseExact(s, "dd.MM.yyyy", CultureInfo.InvariantCulture);
        }

        static void PrintGroup(StudentGroup group, string title)
        {
            Console.WriteLine("========================================");
            Console.WriteLine(title);
            Console.WriteLine("Группа: " + group.Name + " (кол-во: " + group.Count + ")");
            Console.WriteLine("----------------------------------------");

            int i = 0;
            while (i < group.Count)
            {
                Console.WriteLine(group.GetAt(i));
                i++;
            }
            Console.WriteLine();
        }

        static void PrintList(List<Student> list, string title)
        {
            Console.WriteLine(title);
            int i = 0;
            while (i < list.Count)
            {
                Console.WriteLine(list[i]);
                i++;
            }
            Console.WriteLine();
        }

        static void Main()
        {
            StudentGroup g = new StudentGroup("КИ25-11Б");

            g.Add(new Student("Иванов", "Пётр", D("14.03.2004"), 101));
            g.Add(new Student("Смирнова", "Анна", D("02.11.2003"), 102));
            g.Add(new Student("Петров", "Илья", D("25.07.2004"), 103));
            g.Add(new Student("Иванов", "Даниил", D("09.01.2005"), 104));
            g.Add(new Student("Кузнецова", "Мария", D("30.05.2003"), 105));

            PrintGroup(g, "1. Исходная группа");

            Console.WriteLine("2. Доступ по индексу 2:");
            Console.WriteLine(g.GetAt(2));
            Console.WriteLine();

            Console.WriteLine("3. Доступ по номеру записи 104:");
            Console.WriteLine(g.GetByRecordNumber(104));
            Console.WriteLine();

            PrintList(g.FindByLastName("Иванов"), "4. Поиск по фамилии Иванов:");
            PrintList(g.FindByFirstName("Анна"), "5. Поиск по имени Анна:");
            PrintList(g.FindByBirthDate(D("25.07.2004")), "6. Поиск по дате рождения 25.07.2004:");

            g.Sort(SortField.LastName, true);
            PrintGroup(g, "7. Сортировка по фамилии");

            g.Sort(SortField.BirthDate, false);
            PrintGroup(g, "8. Сортировка по дате рождения");

            Console.WriteLine("9. Удаление по номеру записи 102:");
            bool removed = g.RemoveByRecordNumber(102);
            if (removed) Console.WriteLine("Удалено");
            else Console.WriteLine("Не найдено");
            Console.WriteLine();

            Console.WriteLine("10.  Удаление по индексу 0:");
            removed = g.RemoveAt(0);
            if (removed) Console.WriteLine("Удалено");
            else Console.WriteLine("Не найдено");
            Console.WriteLine();

            PrintGroup(g, "11. Итоговая группа");
        }
    }