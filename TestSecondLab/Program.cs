using System;
using System.Collections.Generic;

namespace TestSecondLab
{
    public class Reward
    {
        public string reason;
        public string reward;
        public bool ready = false;
        public bool isIssued = false;
        public bool isAccepted = false;
        public Reward(string reason, string reward)
        {
            this.reason = reason;
            this.reward = reward;
        }
        public void Ready()
        {
            ready = true;
        }
        public void Issue()
        {
            isIssued = true;
        }
        public void Accept()
        {
            isAccepted = true;
        }
    }
    public class Address
    {
        public string city;
        public string house;
        public string street;
        public Address(string city, string house, string street)
        {
            this.city = city;
            this.house = house;
            this.street = street;
        }
        public string GetFullAddress()
        {
            if(String.IsNullOrEmpty(city) || String.IsNullOrEmpty(street) || String.IsNullOrEmpty(house))
            {
                return "Введен некорректный адрес";
            }
            return "Город:" + city + " улица:" + street + " дом:" + house;
        }
    }
    public class Company
    {
        public string name;
        public Address address;
        public List<Department> departments = new List<Department>();
        public void AddDepartment(Department department)
        {
            if(department != null)
            {
                departments.Add(department);
            }
            else
            {
                Console.WriteLine("Введен некорректный отдел, операция не выполнена");
            }
        }
        public void RemoveDepartment(Department department)
        {
            departments.Remove(department);
        }
        public Company(Address address, string name)
        {
            this.address = address;
            this.name = name;
        }
    }
    public class Department
    {
        public List<Employee> employees = new List<Employee>();
        public Manager manager;
        public string name;
        public Department(string name)
        {
            this.name = name;

        }
        public void SetManager(Manager manager)
        {
            if(manager != null)
            {
                this.manager = manager;
                manager.SetDepartment(this);
            }
        }
        public void RemoveManager()
        {
            if(manager != null)
            {
                manager.RemoveDepartment(this);
            }
            manager = null;
        }
        public void AddEmployee(Employee employee)
        {
            if(employee != null)
            {
                employees.Add(employee);
                employee.department = this;
            }
            else
            {
                Console.WriteLine("Сотрудник некорректен, операция не выполнена");
            }
        }
        public void Fire(Employee employee) 
        {
            if (employee != null)
            {
                employees.Remove(employee);
                employee.SetFiredStatus();
                employee.department = null;
            }
            else Console.WriteLine("Введен некорректный пользователь, операция не выполнена");
        }
    }
    public class Task
    {
        public string topic;
        public bool isCompleted = false;
        public Task(string topic)
        {
            this.topic = topic;
        }
        public void Complete()
        {
            isCompleted = true;
        }
    }
    public class Candidate
    {
        public string fullName;
        public string careerObjective;
        public string desiredSalary = "0";
        public bool accepted = false;
        public bool canceled = false;
        public List<Task> tasks = new List<Task>();
        public Candidate(string name, string careerObjective)
        {
            fullName = name;
            this.careerObjective = careerObjective;
        }
        public void SetAcceptedStatus()
        {
            accepted = true;
        }
        public void SetCanceledStatus()
        {
            canceled = true;
        }
        public void AddTask(Task task)
        {
            if(task != null)
            {
                tasks.Add(task);
            }
            else
            {
                Console.WriteLine("Введена некорректная задача, операция не выполнена");
            }
        }
        public void CompleteTasks()
        {
            foreach (var t in tasks)
            {
                t.Complete();
            }
            tasks.Clear();
        }
    }
    public class Manager
    {
        public string fullName;
        public List<Reward> rewards = new List<Reward>();
        public List<Department> managingDepartment = new List<Department>();
        public List<Candidate> candidates = new List<Candidate>();
        public Manager(string fullName)
        {
            this.fullName = fullName;
        }
        public void AddCandidate(Candidate candidate)
        {
            if (candidate != null)
            {
                candidates.Add(candidate);
            }
            else
            {
                Console.WriteLine("Введен неверный кандидат, действие не выполнено");
            }
        }
        public void Hire(Candidate candidate, Department department)
        {
            if(candidate != null && department != null)
            {
                var newEmployee = new Employee(candidate.fullName, candidate.careerObjective);
                department.AddEmployee(newEmployee);
                candidate.SetAcceptedStatus();
                candidates.Remove(candidate);
            }
        }
        public void Cancel(Candidate candidate)
        {
            if(candidate != null)
            {
                candidate.SetCanceledStatus();
                candidates.Remove(candidate);
            }
            else
            {
                Console.WriteLine("Введен некорректный кандидат, действие не выполнено");
            }
        }
        public void AddReward(Reward reward)
        {
            if(reward != null)
            {
                rewards.Add(reward);
                reward.Ready();
            }
        }
        public void GiveReward(Employee employee, Reward reward)
        {
            if(employee != null && reward != null && !reward.isIssued && !reward.isAccepted)
                {
                employee.AddReward(reward);
                rewards.Remove(reward);
                reward.Issue();
            }
            else
            {
                Console.WriteLine("Введен некорректный сотрудник или некорректная награда,либо награда уже в процессе выдачи, операция не выполнена");
            }
        }
        public void SetDepartment(Department department)
        {
            if(department != null)
            {
                managingDepartment.Add(department);
            }
            else
            {
                Console.WriteLine("Ввведен некорректный отдел, операция не выполнена");
            }
        }
        public void RemoveDepartment(Department department)
        {
            managingDepartment.Remove(department);
        }
        
    }
    public class Employee
    {
        public string fullName; 
        public List<Task> tasks = new List<Task>();
        public string position;
        public Department department;
        public List<Reward> rewards = new List<Reward>();
        public bool isFired = false;
        public Employee(string fullname, string position)
        {
            this.fullName = fullname;
            this.position = position;
        }
        public void AddTask(Task task)
        {
            if(task != null)
            {
                tasks.Add(task);
            }
            else
            {
                Console.WriteLine("Ввведена некорректная задача, операция не выполнена");
            }
        }
        public void CompleteTasks()
        {
            foreach (var t in tasks)
            {
                t.Complete();
            }
            tasks.Clear();
        }
        public void AddReward(Reward reward)
        {
            if(reward != null)
            {
                rewards.Add(reward);
            }
            else
            {
                Console.WriteLine("Введена некорректная награда, операция не выполнена");
            }

        }
        public void GetEveryReward()
        {
            foreach (var r in rewards)
            {
                r.Accept();
            }
            rewards.Clear();
        }

        public void SetFiredStatus()
        {
            isFired = true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var adress = new Address("Ижевск", "Пушкинская", "10");
            var company = new Company(adress, "ISTU Prilukov");
            var manager = new Manager("Клешев");
            var department = new Department("Отдел внедрения");
            var candidate = new Candidate("Гумметов", "сотрудник отдела внедрения");
            var task = new Task("Провести обучение");
            var reward = new Reward("За хорошую работу", "Премия 5000 рублей");
            company.AddDepartment(department);
            Console.WriteLine("AddDepartment:Количество отделов:{0}", company.departments.Count);
            department.SetManager(manager);
            Console.WriteLine("SetManager:Менеджер:{0}", department.manager.fullName);
            manager.AddCandidate(candidate);
            Console.WriteLine("AddCandidate:Количество кандидатов:{0}", manager.candidates.Count);
            manager.Hire(candidate, department);
            Console.WriteLine("Hire:Количество кандидатов:{0}", manager.candidates.Count);
            Console.WriteLine("Hire:Статус кандидата-принят:{0}", candidate.accepted);
            Console.WriteLine("Hire:Количество сотрудников в отделе:{0}", department.employees.Count);
            var employee = department.employees[0];
            department.Fire(department.employees[0]);
            Console.WriteLine("Fire:Количество сотрудников в отделе:{0}", department.employees.Count);
            Console.WriteLine("Fire:Статус сотрудника уволен:{0}", employee.isFired);
            department.RemoveManager();
            Console.WriteLine("RemoveManager:Менеджер:{0}", department.manager);
            company.RemoveDepartment(department);
            Console.WriteLine("RemoveDepartment:Количество отделов:{0}", company.departments.Count);




            /*var adress = new Address("Ижевск", "Пушкинская", "10");
            var company = new Company(adress,"ISTU Prilukov");
            var manager = new Manager("Клешев");
            var department = new Department("Отдел внедрения", manager);
            var candidate = new Candidate("Гумметов", "сотрудник отдела внедрения");
            var task = new Task("Провести обучение");
            var reward = new Reward("За хорошую работу", "Премия 5000 рублей");
            company.AddDepartment(department);
            Console.WriteLine("Количество отделов:{0}", company.departments.Count);
            Console.WriteLine("Менеджер:{0}", department.manager.fullName);
            manager.AddCandidate(candidate);
            Console.WriteLine("Количество кандидатов:{0}", manager.candidates.Count);
            manager.Cancel(candidate);
            Console.WriteLine("Количество кандидатов:{0}", manager.candidates.Count);
            Console.WriteLine("Статус кандидата отказано:{0}", candidate.canceled);
            manager.Hire(candidate,department);
            Console.WriteLine("Количество кандидатов:{0}", manager.candidates.Count);
            Console.WriteLine("Статус кандидата:{0}", candidate.accepted);
            Console.WriteLine("Количество сотрудников в отделе:{0}", department.employees.Count);
            var employee = department.employees[0];
            department.Fire(department.employees[0]);
            Console.WriteLine("Количество сотрудников в отделе:{0}", department.employees.Count);
            Console.WriteLine("Статус сотрудника уволен:{0}", employee.isFired);
            department.RemoveManager();
            Console.WriteLine("Менеджер:{0}", department.manager);
            company.RemoveDepartment(department);
            Console.WriteLine("Количество отделов:{0}", company.departments.Count);*/
            /*department.employees[0].AddTask(task);
            Console.WriteLine("Количество задач:{0}", department.employees[0].tasks.Count);
            department.employees[0].CompleteTasks();
            Console.WriteLine("Количество задач:{0}", department.employees[0].tasks.Count);
            Console.WriteLine("Статус задачи:{0}", task.isCompleted);
            manager.AddReward(reward);
            Console.WriteLine("Количество наград:{0}", manager.rewards.Count);
            manager.GiveReward(department.employees[0], reward);
            Console.WriteLine("Количество наград:{0}", manager.rewards.Count);
            Console.WriteLine("Количество наград сотрудника:{0}", department.employees[0].rewards.Count);
            department.employees[0].GetEveryReward();
            Console.WriteLine("Количество наград сотрудника:{0}", department.employees[0].rewards.Count);
            Console.WriteLine("Статус награды получена:{0}", reward.isAccepted);*/
        }
    }
}

