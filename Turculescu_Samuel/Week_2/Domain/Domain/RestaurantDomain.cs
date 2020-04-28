﻿using System;
using System.Collections.Generic;
using System.Text;
using Domain.Domain.CreateMenuOp;
using Domain.Domain.CreateRestaurantOp;
using Domain.Domain.CreateClientOp;
using Domain.Models;
using Infra.Free;
using LanguageExt.ClassInstances;
using static IOExt;
using static Domain.Domain.CreateRestaurantOp.CreateRestaurantResult;
using static Domain.Domain.CreateClientOp.CreateClientResult;

namespace Domain.Domain
{
    public static class RestaurantDomain
    {
        public static IO<ICreateRestaurantResult> CreateRestaurant(string name, string address) =>
            NewIO<CreateRestaurantCmd, ICreateRestaurantResult>(new CreateRestaurantCmd(name, address));

        public static IO<CreateMenuResult.ICreateMenuResult> CreateMenu(Restaurant restaurant, string menuName)
            => NewIO<CreateMenuCmd, CreateMenuResult.ICreateMenuResult>(new CreateMenuCmd(restaurant, menuName));

        public static IO<ICreateClientResult> CreateClient(string firstName, string lastName, string email, string phone, string cardNumber, string idClient) =>
            NewIO<CreateClientCmd, ICreateClientResult>(new CreateClientCmd(firstName, lastName, email, phone, cardNumber, idClient));
    }
}
