using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Application.Generators
{
    public class CodeGenerator
    {
        public static string GenerateCode()
        {
            Random rnd = new Random();
            int random = rnd.Next(100_000, 999_999);
            return random.ToString();
        }
    }
}
