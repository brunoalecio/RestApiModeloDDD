using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestApiModeloDDD.Application.Dtos;

namespace RestApiModeloDDD.Application.Features.Clientes.Commands
{
    public class UpdateClienteCommand
    {
        public ClienteDto ClienteDto { get; set; }
    }
}
