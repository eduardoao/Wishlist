﻿using System;
using Wishlist.Core.Interfaces.Repositorys;
using Wishlist.Core.Interfaces.Services;
using Wishlist.Core.Models;
using Wishlist.Core.Models.ValueObject;

namespace Wishlist.Core.Services
{
    public class ServiceClient : BaseService<Client>, IClientService
    {
        public readonly IRepositoryClient _repositoryClient;
      

        public ServiceClient(IRepositoryClient RepositoryCliente)
            : base(RepositoryCliente)
        {
            _repositoryClient = RepositoryCliente;
           
        }

        public override void Add(Client obj)
        {            

            var clientexist = _repositoryClient.GetByEmail(obj.Email.ToString());
            if (clientexist != null)
            {
                Errors.Add(new Error("001", "Cliente já existente na base de dados!"));
                return;
            }
            obj.SetIsEnable(true);
            base.Add(obj);
        }

        public override void Update(Client obj)
        {
            var clientexist = _repositoryClient.GetByEmail(obj.Email.ToString());
            if (clientexist == null)
            {
                Errors.Add(new Error("001", "Cliente não existe na base de dados!"));
                return;
            }
            base.Update(obj);
        }

    }
}
