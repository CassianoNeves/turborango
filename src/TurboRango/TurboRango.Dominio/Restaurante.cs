﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboRango.Dominio
{
    public class Restaurante
    {
        public Restaurante( int capacidade, string nome, Localizacao localizacao, Contato contato, Categoria categoria )
        {
            this.Capacidade = capacidade;
            this.Nome = nome;
            this.Localizacao = localizacao;
            this.Categoria = categoria;
        }

        public Restaurante() { }

        public int? Capacidade { get; set; }
        public string Nome { get; set; }
        public Localizacao Localizacao { get; set; }
        public Contato Contato { get; set; }
        public Categoria Categoria { get; set; }
    }
}
