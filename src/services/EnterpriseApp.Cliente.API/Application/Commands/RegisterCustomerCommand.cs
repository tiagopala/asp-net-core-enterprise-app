﻿using EnterpriseApp.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnterpriseApp.Cliente.API.Application.Commands
{
    public class RegisterCustomerCommand : Command
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Email { get; }
        public string Cpf { get; }

        public RegisterCustomerCommand(Guid id, string name, string email, string cpf)
        {
            AggregateId = id;
            Id = id;
            Name = name;
            Email = email;
            Cpf = cpf;
        }
    }
}
