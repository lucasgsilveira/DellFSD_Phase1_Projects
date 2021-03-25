using ProjectPhase1.Builders;
using ProjectPhase1.Repositories;
using ProjectPhase1.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPhase1.Templates
{
    public class ConcreteTeacherManagementApp : AbstractTeacherAppTemplate
    {
        protected override void loadTeachers()
        {
            var teachersRepository = new PipeDelimitedFileTeachersRepository("teachers.txt");
            var teachersAsList = teachersRepository.Load();
            _teachers = teachersAsList.ToDictionary(t => t.ID);
            Console.WriteLine($"Loaded {_teachers.Count} teachers");
        }

        protected override void saveTeachers(IEnumerable<Teacher> teachers)
        {
            var teachersRepository = new PipeDelimitedFileTeachersRepository("teachers.txt");
            teachersRepository.Save(teachers);
        }

        protected override int getOption()
        {
            try
            {
                var input = int.Parse(Console.ReadLine());
                return input;
            }
            catch (Exception e) { return 100; }
        }

        protected override void addTeacher()
        {
            var teacherBuilder = new ConcreteTeacherBuilder();
            var teacher = teacherBuilder.Build();
            if (_teachers.ContainsKey(teacher.ID))
            {
                Console.WriteLine("Not able to add Teacher. ID already exists");
            }
            else 
            {
                _teachers[teacher.ID] = teacher;
            }
        }

        protected override void deleteTeacher()
        {
            Console.WriteLine("Enter ID of teacher to delete");
            var id = int.Parse(Console.ReadLine());

            if (!_teachers. ContainsKey(id))
            {
                Console.WriteLine($"Teacher with id {id} not found");
            }
            else
            {
                _teachers.Remove(id);
                Console.WriteLine("Removed teacher");
            }
        }

        protected override void findTeacher()
        {
            Console.WriteLine("Enter ID of teacher to find");
            var id = int.Parse(Console.ReadLine());

            if (!_teachers.ContainsKey(id))
            {
                Console.WriteLine($"Teacher with id {id} not found");
            }
            else
            {
                Console.WriteLine(_teachers[id].ToString());
            }
        }

        protected override void listTeachers(IEnumerable<Teacher> teachers)
        {
            // TODO: loop through all teachers passed as a parameter and write them to the console
            foreach (Teacher teacher in teachers)
            {
                Console.WriteLine(teacher.ToString());
            }
        }

        protected override void sortTeachers()
        {
            Console.WriteLine("You chose to sort teachers");
            Console.WriteLine("How would you like to sort them?");
            Console.WriteLine("1) ID");
            Console.WriteLine("2) Last Name");
            Console.WriteLine("3) First Name");

            var option = int.Parse(Console.ReadLine());
            ISortTeachersStrategy sortStrategy = null;
            switch (option)
            {
                case 1: sortStrategy = new SortTeachersByIDStrategy(); break; 
                case 2: sortStrategy = new SortTeachersByLastNameStrategy(); break;
                case 3: sortStrategy = new SortTeachersByFirstNameStrategy(); break;
            }

            var sorted = sortStrategy.Sort(_teachers.Values);
            listTeachers(sorted);
        }

        protected override void updateTeacher()
        {
            Console.WriteLine("Enter ID of teacher to update");
            var id = int.Parse(Console.ReadLine());
            if (!_teachers.ContainsKey(id))
            {
                Console.WriteLine($"Did not find teacher with id: {id}");
                return;
            }

            var teacher = _teachers[id];
            Console.WriteLine("You selected...");
            Console.WriteLine(teacher);

            Console.WriteLine("Update First Name: ");
            teacher.FirstName = Console.ReadLine();
            Console.WriteLine("Update Last Name: ");
            teacher.LastName = Console.ReadLine();
            Console.WriteLine("Update successful!");
        }
    }
}
