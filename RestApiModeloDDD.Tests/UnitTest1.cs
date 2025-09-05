using RestApiModeloDDD.Domain.Entities;
using FluentAssertions;
using RestApiModeloDDD.Domain.Core.Interfaces.Repositories;
using RestApiModeloDDD.Domain.Services;
using Moq;

namespace RestApiModeloDDD.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void IsValido()
        {
            // Criamos uma nova instância da entidade com dados válidos.
            var cliente = new Cliente(
                nome: "Dorf",
                sobrenome: "Alves",
                email: "dorf@teste.com"
            )
            {
                Id = 1
            };

            // Act (Agir)
            // Chamamos o método que queremos testar.
            var ehValido = cliente.IsValido();

            // Assert (Verificar)
            // Verificamos se o resultado foi 'true'.
            // A sintaxe do FluentAssertions (ehValido.Should().BeTrue()) é bem legível.
            ehValido.Should().BeTrue();

        }

        [Fact]
        public void GetById_Should_Return_Cliente_When_Id_Exists()
        {
            // Arrange (Organizar)
            // 1. Criamos o dublê do repositório.
            var mockRepository = new Mock<IRepositoryCliente>();

            // 2. Criamos um cliente falso que será o "retorno" do nosso dublê.
            var clienteFalso = new Cliente("Bruno", "Teste", "bruno@teste.com") { Id = 1 };

            // 3. ESTA É A MÁGICA DO SETUP: Damos o roteiro para o dublê.
            // "Quando o método GetById for chamado com o ID 1, RETORNE o nosso clienteFalso."
            mockRepository.Setup(repo => repo.GetById(1)).Returns(clienteFalso);

            // 4. Criamos o serviço a ser testado, injetando o dublê.
            var serviceCliente = new ServiceCliente(mockRepository.Object);

            // Act (Agir)
            // Chamamos o método. Internamente, ele vai chamar o GetById do nosso dublê.
            var resultado = serviceCliente.GetById(1);

            // Assert (Verificar)
            // Verificamos se o serviço retornou exatamente o cliente que o nosso dublê foi instruído a retornar.
            resultado.Should().NotBeNull();
            resultado.Id.Should().Be(1);
            resultado.Nome.Should().Be("Bruno");

        }

    }
}