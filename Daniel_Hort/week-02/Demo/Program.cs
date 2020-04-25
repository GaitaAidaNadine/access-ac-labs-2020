﻿/*1a. GetRestaurant<GetRestaurantCmd, GetRestaurantResult>(string: name)
1b. GetClient<GetClientCmd, GetClientResult>(string: clientId)
2.  GetMenus<GetMenusCmd, GetMenusResult>(Restaurant: restaurant)
3.  AddToCart<AddToCartCmd, AddToCartResult>(string: sessionId, MenuItem: menuItem, uint: qty) :
							AddToCartSuccessful | AddToCartNotSuccessful | AddToCartInvalidRequest
4.  ChangeQty<ChangeQtyCmd, ChangeQtyResult>(string: sessionId, int: menuItemId, uint newQty)
5.  PlaceOrder<OrderCmd, OrderResult>(Cart cart, Restaurant: restaurant, Client: client, uint tip = 0)
6.  GetOrderStatus<GetOrderStatusCmd, GetOrderStatusResult>(Cart cart);
ASPNET.CORE -> RestaurantDomain -> Result -> Match -> HttpResponseMessage
HTTP Request ------------------------------------------HTTP Response(200)*/

/* 1a. GetReastaurant<,>(Func<Restaurant, bool> expresion)
 * 1b. GetClient<,>(Func<Client, bool> expresion)
 * #1. Get<Model,Cmd,Result>(Func<Model,bool> expresion) // should be a list not an object / interpretable
 * 
 * the other ones need the cart so we'll see it
 * 
*/

using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Domain;
using Domain.Domain.CreateRestauratOp;
using Domain.Models;
using Infrastructure.Free;
using LanguageExt;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using static Domain.Domain.CreateMenuOp.CreateMenuResult;
using static Domain.Domain.CreateRestauratOp.CreateRestaurantResult;

namespace Demo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddOperations(typeof(Restaurant).Assembly);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var expr =
                from restaurantResult in RestaurantDomain.CreateRestaurant("McRonald")
                let restaurant = (restaurantResult as RestaurantCreated)?.Restaurant
                from menuRes1 in RestaurantDomain.CreateMenu(restaurant, "Burgers", MenuType.Meat)
                let menu1 = (menuRes1 as MenuCreated)?.Menu
                from menuItemRes1 in RestaurantDomain.CreateMenuItem(menu1, "Tasty", 13)
                from menuItemRes2 in RestaurantDomain.CreateMenuItem(menu1, "Cheese King", 5)
                from menuRes2 in RestaurantDomain.CreateMenu(restaurant, "Drinks", MenuType.Beverages)
                let menu2 = (menuRes2 as MenuCreated)?.Menu
                from menuItemRes3 in RestaurantDomain.CreateMenuItem(menu2, "Pepsi", 7)
                from menuItemRes4 in RestaurantDomain.CreateMenuItem(menu2, "Cola", 7)
                select restaurantResult;

            var interpreter = new LiveInterpreterAsync(serviceProvider);
            var result = await interpreter.Interpret(expr, Unit.Default);
            var finalResult = result.Match(OnRestaurantCreated, OnRestaurantNotCreated);

            var res = finalResult as Restaurant;

            Console.WriteLine("{0}'s Menus", res.Name);
            res.Menus.ForEach(a =>
            {
                Console.WriteLine("\t~{0} [{1}]~", a.Name, a.MenuType);
                a.Items.ForEach(b => Console.WriteLine("\t{0} / {1}", b.Name, b.Price));
            });

            find(res, a => a.MenuType == MenuType.Beverages);
        }

        private static void find(Restaurant r, Func<Menu, bool> ex)
        {
            var x = r.Menus.Find(ex).FirstOrDefault();
            Console.WriteLine($"\n\n{x.Name}");
        }

        private static object OnRestaurantNotCreated(RestaurantNotCreated arg)
        {
            return arg.Reason;
        }

        private static object OnRestaurantCreated(RestaurantCreated arg)
        {
            return arg.Restaurant;
        }
    }
}
