using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace util
{
    public class WeightedRandom
    {
        public List<choice> Escolhas;

        public WeightedRandom(List<choice> escolhas)
        {
            Escolhas = escolhas;
        }

        public choice Escolhe()
        {
            foreach (var item in Escolhas)
            {
                int testaChance = UnityEngine.Random.Range(0, 101);

                if (testaChance <= item.probabilidade)
                {
                    return item;
                }
            }

            return null;
        }
    }

    public class choice
    {
        public int probabilidade;
        public string nome;
    }
}

