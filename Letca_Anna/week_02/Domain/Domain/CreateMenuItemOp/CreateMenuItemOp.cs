﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Infrastructure.Free;
using LanguageExt;
using Persistence.EfCore;
using static Domain.Domain.CreateMenuItemOp.CreateMenuItemResult;

namespace Domain.Domain.CreateMenuItemOp
{
    class CreateMenuItemOp : OpInterpreter<CreateMenuItemCmd, ICreateMenuItemResult, Unit> 
    {

        public override Task<ICreateMenuItemResult> Work(CreateMenuItemCmd Op, Unit state)
        {
            var (valid, validationResults) = Op.Validate();
            string validationMessage = "";
            validationResults.ForEach(x => validationMessage += x.ErrorMessage);

            if (!valid)
                return Task.FromResult((ICreateMenuItemResult)new MenuItemNotCreated(validationMessage));

            MenuItems newItem = new MenuItems() { Name = Op.Name, Price = Op.Price, MenuId = Op.Menu.Id };
            Op.Menu.MenuItems.Add(newItem);
            return Task.FromResult((ICreateMenuItemResult)new MenuItemCreated(newItem)) ;
        }
    }
}
