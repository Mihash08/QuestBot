using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace questBot
{
    internal class StringNormalizer
    {
        static internal string Normalize(string str)
        {
            return (new string(str.Where(c => !char.IsPunctuation(c)).ToArray())).ToLower();
        } 
    }
}
