namespace _03_antity_framework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AirplaneDBcontext dBcontext=new AirplaneDBcontext();
            dBcontext.Customers.Add(new Customer 
            {
                Name="Maxim",
                Birthdate=new DateTime(2000,12,15),
                Email=  "max@gmail.com"
            });
            dBcontext.SaveChanges();
            foreach (var item in dBcontext.Customers)
            {
                Console.WriteLine($"Name : {item.Name}");
            }
        }
    }
}