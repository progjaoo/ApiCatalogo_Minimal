﻿using System.Text.Json.Serialization;

namespace ApiCatalogo_MinimalAPI.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }

        public string? Nome { get; set; }    

        public string? Descricao { get; set; }

        public decimal Preco { get; set; }  

        public string? Imagem { get; set; }

        public DateTime DataCompra { get; set; }

        public int Estoque { get; set; }
        [JsonIgnore]

        public Categoria? Categorias { get; set; }
        public int CategoriaId { get; set; }    
    }
}
