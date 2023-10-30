using Microsoft.VisualStudio.TestPlatform.TestHost;
using MSite;
using System.Linq;

namespace MSiteTest
{
    public class UnitTest1
    {
        public List<Person> People = new List<Person>
        {
            new Person { Surname = "Smith", Forename = "John", Title = Title.Mr },
            new Person { Surname = "Jones", Forename = "Steve", Title = Title.Mr },
            new Person { Surname = "Bradshaw", Forename = "Lisa", Title = Title.Mrs },
            new Person { Surname = "Thompson", Forename = "Joanne", Title = Title.Miss },
            new Person { Surname = "Johnson", Forename = "David", Title = Title.Mr }
        };

        public List<Department> Departments = new List<Department>
        {
            new Department
            {
                Name = "Purchasing",
                Location = "Top floor",
                Members = new List<Person>
                {
                    new Person { Surname = "Smith", Forename = "John", Title = Title.Mr },
                    new Person { Surname = "Jones", Forename = "Steve", Title = Title.Mr },
                    new Person { Surname = "Bradshaw", Forename = "Lisa", Title = Title.Mrs },
                }
            },

            new Department
            {
                Name = "Sales",
                Location = "Bottom floor",
                Members = new List<Person>
                {
                    new Person { Surname = "Bradshaw", Forename = "Lisa", Title = Title.Mrs },
                    new Person { Surname = "Thompson", Forename = "Joanne", Title = Title.Miss },
                    new Person { Surname = "Johnson", Forename = "David", Title = Title.Mr }
                }
            }
        };



        [Fact]
        public void GetSurnamesFromDepartment_ReturnsEmptyListForNonExistentDepartment()
        {
            var surnames = GetSurnamesFromDepartment("Circus");

            Assert.Empty(surnames);
        }

        [Fact]
        public void GetSurnamesFromDepartment_ReturnsSurnamesForExistingtDepartment()
        {
            var surnames = GetSurnamesFromDepartment("Sales");

            Assert.NotEmpty(surnames);
        }

        [Fact]
        public void GetDepartmentsPersonIn_ReturnsDepartmentsForExistingPerson()
        {
            IList<string> departmentOne = GetDepartmentsPersonIn("Smith", "John");
            IList<string> departmentTwo = GetDepartmentsPersonIn("Johnson", "David");

            Assert.Collection(departmentOne, departmentName => Assert.Equal("Purchasing", departmentName));
            Assert.Collection(departmentTwo, departmentName => Assert.Equal("Sales", departmentName));
        }

        [Fact]
        public void GetDepartmentsPersonIn_ReturnsEmptyList_ForNonExistentPerson()
        {
            IList<string> department = GetDepartmentsPersonIn("Bob", "Bobson");

            Assert.Empty(department);
        }

        public IList<string> GetSurnamesFromDepartment(string departmentName)
        {
            var surnames = Departments
            .Where(department => department.Name == departmentName)
            .SelectMany(department => department.Members.Select(member => member.Surname))
            .ToList();

            return surnames;
        }

        public  IList<string> GetDepartmentsPersonIn(string surname, string forename)
        {
            Person person = Departments
                .SelectMany(department => department.Members)
                .FirstOrDefault(p => p.Surname == surname && p.Forename == forename);


            // Find the departments that the person is a member of
            List<string> departmentNames = Departments
                .Where(department => department.Members.Contains(person))
                .Select(department => department.Name)
                .ToList();

            return departmentNames;
        }
    }
}