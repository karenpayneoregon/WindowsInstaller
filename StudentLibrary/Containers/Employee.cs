namespace EmployeeLibrary.Containers
{
    public class Employee 
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString() => Name;

    }
}
