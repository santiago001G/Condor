using AutoMapper;
using Condor.Core.Entities;
using Condor.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condor.Infraestructure.Mappings
{
    public class AutomapperProfile: Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Cliente, ClienteDto>();
            CreateMap<ClienteDto, Cliente>();
            CreateMap<ProductosClienteDTO, ProductosCliente>();
            CreateMap<ProductosCliente, ProductosClienteDTO>();
            CreateMap<AbonoDto, AbonoCliente>();
            CreateMap<AbonoCliente, AbonoDto>();
        }

    }
}
