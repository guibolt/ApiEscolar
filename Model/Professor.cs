using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Professor : Base

    {
        public double Salario { get; set; }
        public List<Materia> Materias { get; set; }
    }
}
