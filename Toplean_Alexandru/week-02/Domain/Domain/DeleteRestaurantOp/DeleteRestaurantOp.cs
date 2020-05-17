﻿using Infrastructure.Free;
using LanguageExt;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using static Domain.Domain.DeleteRestaurantOp.DeleteRestaurantResult;
using Domain.Models;

namespace Domain.Domain.DeleteRestaurantOp
{
    internal class DeleteRestaurantOp : OpInterpreter<DeleteRestaurantCmd, IDeleteRestaurantResult, Unit>
    {
        public override Task<IDeleteRestaurantResult> Work(DeleteRestaurantCmd Op, Unit state)
        {
            if (Exists(Op.RestaurantName))
            {
                // Get the restaurant that will be deleted
                Restaurant restaurant = AllHardcodedValues.HarcodedVals.RestaurantList.FirstOrDefault(r => r.Name == Op.RestaurantName);
                // Deleting the restaurant
                AllHardcodedValues.HarcodedVals.RestaurantList.Remove(restaurant);
                return Task.FromResult<IDeleteRestaurantResult>(new RestaurantDeleted(true));
            }
            else
            {
                // No restaurant deleted. Restaurant does not exist
                return Task.FromResult<IDeleteRestaurantResult>(new RestaurantNotDeleted("The restaurant does not exist!"));
            }
        }

        public bool Exists(String name) => AllHardcodedValues.HarcodedVals.RestaurantList.Any(m => m.Name == name);
    }
}