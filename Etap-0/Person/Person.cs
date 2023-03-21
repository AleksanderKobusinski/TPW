using System;
namespace PersonNS

{
    public class Person
    {
        private string m_firstName;
        private string m_lastName;
        private DateTime m_birthDate;

        public Person(string firstName, string lastName, DateTime birthDate)
        {
            m_firstName = firstName;
            m_lastName = lastName;
            m_birthDate = birthDate;
        }

        public string PrintPerson
        {
            get { return m_firstName + " " + m_lastName + " " + m_birthDate.ToString("dd MM yyyy"); }
        }

        public bool ReachedMaturity()
        {
            return DateTime.Compare(DateTime.Now.AddYears(-18), this.m_birthDate) >0;
        }


        public static void Main()
        {
            Person newPerson = new("John", "Doe", new DateTime(2005,3,20));
            Console.WriteLine(newPerson.PrintPerson);
            
            string reachedMaturity = newPerson.m_firstName + " has reached maturity";
            string notReachedMaturity = newPerson.m_firstName + " has not reached maturity";

            if (newPerson.ReachedMaturity()) Console.WriteLine(reachedMaturity);
            else Console.WriteLine(notReachedMaturity);
        }
    }
}