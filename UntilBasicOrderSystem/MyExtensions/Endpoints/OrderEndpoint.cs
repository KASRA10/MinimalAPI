using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyExtensions.Endpoints.Contracts;
using MyExtensions.Services;

namespace MyExtensions.Endpoints
{
    public class OrderEndpoint : IEndpoint
    {
        //? REPR: Request
        public record PlaceOrderRequest(
            string CustomerNAme,
            string MobileNumber,
            string SendMethod,
            decimal TotalAmount
        );

        //? REPR: Response
        public record PlaceOrderResponse(int OrderId);

        //? Constructor Injection
        private readonly IOrderIdGenerator orderIdgenerator;

        public OrderEndpoint(IOrderIdGenerator orderIdgenerator)
        {
            this.orderIdgenerator = orderIdgenerator;
        }

        public void MapEndpoint(IEndpointRouteBuilder builder)
        {
            RouteGroupBuilder orderGroup = builder.MapGroup("orders");
            orderGroup.MapPost("place-order", PlaceOrderAsync).WithTags("Order System");
        }

        Task<IResult> PlaceOrderAsync(
            [FromBody] PlaceOrderRequest request,
            [FromServices] IOrderIdGenerator orderIdgenerator,
            [FromKeyedServices("Sms")] INotificationService notificationService
        )
        {
            //? Scenario
            //? Validate Data
            //? Save to DATABASE
            //? Send Notification (Message)
            if (string.IsNullOrEmpty(request.CustomerNAme))
            {
                return Task.FromResult(Results.BadRequest("Customer Name is a required field."));
            }

            if (string.IsNullOrEmpty(request.MobileNumber))
            {
                return Task.FromResult(Results.BadRequest("Mobile Number is a required field."));
            }
            //? Email, Dry

            //? Save (Ok) ==> Order Id
            //? Number, string, Guid

            // //? Requirement ORderID Generating
            // int OrderId = new Random().Next(1, 1000);

            int orderId = orderIdgenerator.New();

            PlaceOrderResponse response = new PlaceOrderResponse(orderId);

            //? Notification, Family Services
            notificationService.SendMessageAsync(
                $"Hi, {request.CustomerNAme}, Order id: {response.OrderId}",
                request.MobileNumber
            );

            // return Task.FromResult(Results.Ok("Order Placed"));
            return Task.FromResult((IResult)TypedResults.Ok(response));
        }

        //! or we can use variable and async:
        // async Task<IResult> PlaceOrderAsync()
        // {
        //     var result = await Task.FromResult(Results.Ok("Order PLaces"));
        //     return result;
        // }
    }
}
