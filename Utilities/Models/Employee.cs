namespace Utilities.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Employee otherEmployee = (Employee)obj;
            return Id == otherEmployee.Id && Name == otherEmployee.Name && Age == otherEmployee.Age;
        }

        //public override int GetHashCode()
        //{ return 0; }
    }
}