﻿namespace Exemplo.Domain.Model
{
    public class SedeModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateOnly DataInclusao { get; set; }
        public bool Ativo {  get; set; }
    }
}
