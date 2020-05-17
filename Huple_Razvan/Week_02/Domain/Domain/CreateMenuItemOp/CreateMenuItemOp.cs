﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Infrastructure.Free;
using LanguageExt;
using static Domain.Domain.CreateMenuItemOp.CreateMenuItemResult;
namespace Domain.Domain.CreateMenuItemOp
{
    class CreateMenuItemOp : OpInterpreter<CreateMenuItemCmd, ICreateMenuItemResult, Unit>
    {
        public override Task<ICreateMenuItemResult> Work(CreateMenuItemCmd Op, Unit state)
        {
            MenuItem m = new MenuItem(Op.Menu, Op.Title, Op.Price, Op.Ingredients);
            if (Op.Menu.menuItems.Contains(m))
                return Task.FromResult((ICreateMenuItemResult)new MenuItemNotCreated("already exists"));
            Op.Menu.menuItems.Add(m);
            return Task.FromResult((ICreateMenuItemResult)new MenuItemCreated(m));
        }
    }
}
