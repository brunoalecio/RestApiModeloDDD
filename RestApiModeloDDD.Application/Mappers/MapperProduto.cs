using RestApiModeloDDD.Application.Dtos;
using RestApiModeloDDD.Application.Interfaces.Mappers;
using RestApiModeloDDD.Domain.Entities;

namespace RestApiModeloDDD.Application.Mappers
{
    public class MapperProduto : IMapperProduto
    {
        public Produto MapperDtoToEntity(ProdutoDto produtoDto)
        {
            Produto produto = new Produto(produtoDto.Nome, produtoDto.Valor);
            produto.Id = produtoDto.Id;
            return produto;
        }

        public ProdutoDto MapperEntityToDto(Produto produto)
        {
            ProdutoDto produtoDto = new ProdutoDto()
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Valor = produto.Valor
            };

            return produtoDto;
        }

        public IEnumerable<ProdutoDto> MapperListProdutosDto(IEnumerable<Produto> produtos)
        {
            var dto = produtos.Select(c => new ProdutoDto { Id = c.Id, Nome = c.Nome, Valor = c.Valor });

            return dto;
        }
    }
}