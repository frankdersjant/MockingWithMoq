using ConsoleIntro.Interfaces;

namespace ConsoleIntro
{
    public class Dog : IAnimal
    {
        public string MakesSound()
        {
            return "Woof";
        }
    }
}
