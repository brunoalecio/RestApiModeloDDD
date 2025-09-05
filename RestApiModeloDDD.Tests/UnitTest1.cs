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
            // Criamos uma nova inst�ncia da entidade com dados v�lidos.
            var cliente = new Cliente(
                nome: "Dorf",
                sobrenome: "Alves",
                email: "dorf@teste.com"
            )
            {
                Id = 1
            };

            // Act (Agir)
            // Chamamos o m�todo que queremos testar.
            var ehValido = cliente.IsValido();

            // Assert (Verificar)
            // Verificamos se o resultado foi 'true'.
            // A sintaxe do FluentAssertions (ehValido.Should().BeTrue()) � bem leg�vel.
            ehValido.Should().BeTrue();

        }

        [Fact]
        public void GetById_Should_Return_Cliente_When_Id_Exists()
        {
            // Arrange (Organizar)
            // 1. Criamos o dubl� do reposit�rio.
            var mockRepository = new Mock<IRepositoryCliente>();

            // 2. Criamos um cliente falso que ser� o "retorno" do nosso dubl�.
            var clienteFalso = new Cliente("Bruno", "Teste", "bruno@teste.com") { Id = 1 };

            // 3. ESTA � A M�GICA DO SETUP: Damos o roteiro para o dubl�.
            // "Quando o m�todo GetById for chamado com o ID 1, RETORNE o nosso clienteFalso."
            mockRepository.Setup(repo => repo.GetById(1)).Returns(clienteFalso);

            // 4. Criamos o servi�o a ser testado, injetando o dubl�.
            var serviceCliente = new ServiceCliente(mockRepository.Object);

            // Act (Agir)
            // Chamamos o m�todo. Internamente, ele vai chamar o GetById do nosso dubl�.
            var resultado = serviceCliente.GetById(1);

            // Assert (Verificar)
            // Verificamos se o servi�o retornou exatamente o cliente que o nosso dubl� foi instru�do a retornar.
            resultado.Should().NotBeNull();
            resultado.Id.Should().Be(1);
            resultado.Nome.Should().Be("Bruno");

        }

    }
}