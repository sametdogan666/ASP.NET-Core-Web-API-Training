using InvocationApp.Aspects;

namespace Entities
{
    public interface IEmployee
    {
        void Add(int id, string firstName, string lastName);
        void Update(int id, string firstName, string lastName);
    }

    public class Employee : IEmployee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DefensiveProgrammingAspect(Priority = 1)]
        [InterceptorAspect(Priority = 2)]
        public virtual void Add(int id, string firstName, string lastName)
        {
            Console.WriteLine("Added");
        }

        [DefensiveProgrammingAspect(Priority = 1)]
        [InterceptorAspect(Priority = 2)]
        public virtual void Update(int id, string firstName, string lastName)
        {
            Console.WriteLine("Updated");
        }
    }
}