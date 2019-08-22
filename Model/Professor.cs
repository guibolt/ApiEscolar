using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Professor : Base

    {
        public double Salario { get; set; }
        public List<Materia> Materia { get; set; } = new List<Materia>();
    }
}
