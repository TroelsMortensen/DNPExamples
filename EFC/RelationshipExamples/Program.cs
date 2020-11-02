using System;
using System.Threading.Tasks;
using RelationshipExamples.ManyToMany;

namespace RelationshipExamples
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await new MTMExamples().RunExample();
        }
    }
}