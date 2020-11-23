// <copyright file="ServiceModule.cs" company="BakuninCompany">
// Copyright (c) BakuninCompany. All rights reserved.
// </copyright>

namespace BLL.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DAL.Interfaces;
    using DAL.Repositories;
    using Ninject.Modules;

    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceModule"/> class.
    /// </summary>
    public class ServiceModule : NinjectModule
    {
        private string connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceModule"/> class.
        /// </summary>
        /// <param name="connection">Connection string.</param>
        public ServiceModule(string connection)
        {
            this.connectionString = connection;
        }

        /// <summary>
        /// Load.
        /// </summary>
        public override void Load()
        {
            this.Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(this.connectionString);
        }
    }
}
