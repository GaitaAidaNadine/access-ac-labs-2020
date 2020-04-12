﻿using CSharp.Choices.Attributes;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Domain.CreateMenuItemOp
{
    [AsChoice]
    public partial class CreateMenuItemResult
    {
        public interface ICreateMenuItemResult { }

        public class MenuItemCreated : ICreateMenuItemResult
        {
            public MenuItem MenuItem { get; }

            public MenuItemCreated(MenuItem menuItem)
            {
                MenuItem = menuItem;
            }
        }

        public class MenuItemNotCreated : ICreateMenuItemResult { }
    }
}
